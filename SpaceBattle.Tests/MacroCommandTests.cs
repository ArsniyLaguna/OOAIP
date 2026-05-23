using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MacroCommandTests
{
    [Fact]
    public void MacroCommand_ShouldExecuteAllCommandsInSequence_Criterion1()
    {
        var commandMock1 = new Mock<ICommand>();
        var commandMock2 = new Mock<ICommand>();
        var commandMock3 = new Mock<ICommand>();

        var commands = new ICommand[] { commandMock1.Object, commandMock2.Object, commandMock3.Object };
        var macro = new MacroCommand(commands);

        macro.Execute();

        commandMock1.Verify(c => c.Execute(), Times.Once);
        commandMock2.Verify(c => c.Execute(), Times.Once);
        commandMock3.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void MacroCommand_ShouldStopAndThrow_WhenCommandFails_Criterion2()
    {
        var commandMock1 = new Mock<ICommand>();
        var commandMock2 = new Mock<ICommand>();
        var commandMock3 = new Mock<ICommand>();

        commandMock1.Setup(c => c.Execute());
        commandMock2.Setup(c => c.Execute()).Throws<InvalidOperationException>();

        var commands = new ICommand[] { commandMock1.Object, commandMock2.Object, commandMock3.Object };
        var macro = new MacroCommand(commands);

        Assert.Throws<InvalidOperationException>(() => macro.Execute());
        commandMock1.Verify(c => c.Execute(), Times.Once);
        commandMock2.Verify(c => c.Execute(), Times.Once);
        commandMock3.Verify(c => c.Execute(), Times.Never);
    }
}
