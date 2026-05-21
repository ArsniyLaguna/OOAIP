using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_ValidArgs_CallsReceiveWithCorrectCommand()
    {
        // Тест 1: Проверяет что при вызове Execute вызывается метод Receive с нужным параметром
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        ICommand sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);
        sendCommand.Execute();

        // Проверяем что метод Receive вызвался у mock-объекта ровно 1 раз и именно с нашей командой
        receiverMock.Verify(r => r.Receive(internalCommandMock.Object), Times.Once);
    }

    [Fact]
    public void SendCommand_ReceiverThrows_ThrowsException()
    {
        // Тест 2: Проверяет что если SendCommand.Execute выбрасывает исключение если приемник сбоит
        var receiverMock = new Mock<ICommandReceiver>();
        var internalCommandMock = new Mock<ICommand>();

        // Настраиваем mock так чтобы при попытке вызвать Receive он выкидывал ошибку
        receiverMock.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws<Exception>();

        ICommand sendCommand = new SendCommand(internalCommandMock.Object, receiverMock.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }
}