using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MacroStrategyTests
{
    [Fact]
    public void CreateMacroCommandStrategy_SuccessfulResolution_Criterion2()
    {
        var commandMock1 = new Mock<ICommand>();
        var commandMock2 = new Mock<ICommand>();

        var strategyCmd1 = new Mock<IStrategy>();
        strategyCmd1.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(commandMock1.Object);

        var strategyCmd2 = new Mock<IStrategy>();
        strategyCmd2.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(commandMock2.Object);

        var cmdNames = new List<string> { "SubCommand1", "SubCommand2" };
        var strategySpec = new Mock<IStrategy>();
        strategySpec.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(cmdNames);

        IoC.Resolve<object>("IoC.Register", "Specs.Macro.Test", strategySpec.Object);
        IoC.Resolve<object>("IoC.Register", "SubCommand1", strategyCmd1.Object);
        IoC.Resolve<object>("IoC.Register", "SubCommand2", strategyCmd2.Object);

        var macroStrategy = new CreateMacroCommandStrategy("Specs.Macro.Test");

        var resolvedMacro = (ICommand)macroStrategy.Invoke(new object[] { "игровой_объект" });

        Assert.NotNull(resolvedMacro);
        resolvedMacro.Execute();

        commandMock1.Verify(c => c.Execute(), Times.Once);
        commandMock2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void CreateMacroCommandStrategy_MissingDependency_ThrowsException_Criterion3()
    {
        var cmdNames = new List<string> { "NonExistentCommand" };
        var strategySpec = new Mock<IStrategy>();
        strategySpec.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(cmdNames);

        IoC.Resolve<object>("IoC.Register", "Specs.Macro.TestError", strategySpec.Object);

        var macroStrategy = new CreateMacroCommandStrategy("Specs.Macro.TestError");
        Assert.Throws<Exception>(() => macroStrategy.Invoke(new object[] { "игровой_объект" }));
    }
}
