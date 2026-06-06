using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCMoveRotateTests
{
    [Fact]
    public void RegisterIoCDependencyMacroMoveRotate_RegistersAndResolvesBothCorrectly()
    {
        var moveSubCmdMock = new Mock<ICommand>();
        var rotateSubCmdMock = new Mock<ICommand>();

        var strategyMoveSub = new Mock<IStrategy>();
        strategyMoveSub.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(moveSubCmdMock.Object);

        var strategyRotateSub = new Mock<IStrategy>();
        strategyRotateSub.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(rotateSubCmdMock.Object);

        var strategySpecsMove = new Mock<IStrategy>();
        strategySpecsMove.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(new List<string> { "MoveSubCommand" });

        var strategySpecsRotate = new Mock<IStrategy>();
        strategySpecsRotate.Setup(s => s.Invoke(It.IsAny<object[]>())).Returns(new List<string> { "RotateSubCommand" });

        IoC.Resolve<object>("IoC.Register", "Specs.Move", strategySpecsMove.Object);
        IoC.Resolve<object>("IoC.Register", "Specs.Rotate", strategySpecsRotate.Object);
        IoC.Resolve<object>("IoC.Register", "MoveSubCommand", strategyMoveSub.Object);
        IoC.Resolve<object>("IoC.Register", "RotateSubCommand", strategyRotateSub.Object);
        var registrationCommand = new RegisterIoCDependencyMacroMoveRotate();
        registrationCommand.Execute();

        var resolvedMacroMove = IoC.Resolve<ICommand>("Macro.Move", new object[] { "игровой_объект" });
        var resolvedMacroRotate = IoC.Resolve<ICommand>("Macro.Rotate", new object[] { "игровой_объект" });

        Assert.NotNull(resolvedMacroMove);
        Assert.NotNull(resolvedMacroRotate);

        resolvedMacroMove.Execute();
        resolvedMacroRotate.Execute();

        moveSubCmdMock.Verify(c => c.Execute(), Times.Once);
        rotateSubCmdMock.Verify(c => c.Execute(), Times.Once);
    }
}
