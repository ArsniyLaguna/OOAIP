using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    // тест 14

    [Fact]
    public void SendCommand_ValidArgs_CallsReceiveWithCorrectCommand()
    {
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        ICommand sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);
        sendCommand.Execute();

        receiverMock.Verify(r => r.Receive(internalCommandMock.Object), Times.Once);
    }

    [Fact]
    public void SendCommand_ReceiverThrows_ThrowsException()
    {
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        receiverMock.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws<Exception>();

        ICommand sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }

    // тест 15

    [Fact]
    public void RegisterSendCommandDependency_Execute_ResolvesCorrectly()
    {
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        // 1 Вызываем команду регистрации зависимости
        ICommand registerCmd = new RegisterIoCDependencySendCommand();
        registerCmd.Execute();

        // 2Разрешаем зависимость Commands.Send через IoC
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Send", internalCommandMock.Object, receiverMock.Object);

        // 3 Проверяем критерии приемки
        Assert.NotNull(resolvedCommand);
        Assert.IsType<SendCommand>(resolvedCommand);
    }
}
