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
        // Arrange
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();
        var sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        // Act
        sendCommand.Execute();

        // Assert
        receiverMock.Verify(r => r.Receive(internalCommandMock.Object), Times.Once);
    }

    [Fact]
    public void SendCommand_ReceiverThrows_ThrowsException()
    {
        // Arrange
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();
        
        receiverMock.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws(new Exception());
        var sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => sendCommand.Execute());
    }

    [Fact]
    public void RegisterSendCommandDependency_Execute_ResolvesCorrectly()
    {
        // Arrange
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        // 1. Вызываем команду регистрации зависимости
        ICommand registerCmd = new RegisterIoCDependencySendCommand();
        registerCmd.Execute();

        // 2. Разрешаем зависимость Commands.Send через IoC
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Send", internalCommandMock.Object, receiverMock.Object);

        // 3. Проверяем критерии приемки
        Assert.NotNull(resolvedCommand);
        Assert.IsType<SendCommand>(resolvedCommand);
    }
}
