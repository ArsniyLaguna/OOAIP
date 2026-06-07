using Moq;
using SpaceBattle.Lib;
using Xunit;
using System;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_ValidArgs_CallsReceiveWithCorrectCommand()
    {
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();
        var sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        sendCommand.Execute();

        receiverMock.Verify(r => r.Receive(internalCommandMock.Object), Times.Once);
    }

    [Fact]
    public void SendCommand_ReceiverThrows_ThrowsException()
    {
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        receiverMock.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws(new Exception());
        var sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }

    [Fact]
    public void RegisterSendCommandDependency_Execute_ResolvesCorrectly()
    {
        IoC.Reset();

        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        ICommand registerCmd = new RegisterIoCDependencySendCommand();
        registerCmd.Execute();

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Send", internalCommandMock.Object, receiverMock.Object);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<SendCommand>(resolvedCommand);
    }
}
