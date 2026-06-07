using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCMacroTests
{
    [Fact]
    public void RegisterIoCDependencyMacroCommand_RegistersAndResolvesCorrectly_Criterion1()
    {
        var registerCommand = new RegisterIoCDependencyMacroCommand();
        
        registerCommand.Execute();

        var commandMock1 = new Mock<ICommand>();
        var commandMock2 = new Mock<ICommand>();
        var commandArray = new ICommand[] { commandMock1.Object, commandMock2.Object };

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Macro", new object[] { commandArray });

        Assert.NotNull(resolvedCommand);
        Assert.IsType<MacroCommand>(resolvedCommand);

        resolvedCommand.Execute();
        commandMock1.Verify(c => c.Execute(), Times.Once);
        commandMock2.Verify(c => c.Execute(), Times.Once);
    }
}
