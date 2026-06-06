using SpaceBattle.Lib;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;

namespace SpaceBattle.Tests;

public class MacroStrategyTests
{
    [Fact]
    public void CreateMacroCommandStrategy_SuccessfulResolution_Criterion2()
    {
        var commandMock1 = new Mock<ICommand>();
        var commandMock2 = new Mock<ICommand>();

        // Используем прямой Register (без использования Moq для стратегий регистрации)
        IoC.Register("Specs.Macro.Test", args => new List<string> { "SubCommand1", "SubCommand2" });
        IoC.Register("SubCommand1", args => commandMock1.Object);
        IoC.Register("SubCommand2", args => commandMock2.Object);

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
        // Регистрируем список, который ссылается на несуществующую команду
        IoC.Register("Specs.Macro.TestError", args => new List<string> { "NonExistentCommand" });

        var macroStrategy = new CreateMacroCommandStrategy("Specs.Macro.TestError");

        // Ожидаем исключение, так как "NonExistentCommand" не зарегистрирована в IoC
        Assert.Throws<Exception>(() => macroStrategy.Invoke(new object[] { "игровой_объект" }));
    }
}
