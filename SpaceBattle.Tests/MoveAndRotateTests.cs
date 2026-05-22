using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MoveAndRotateTests
{
    [Fact]
    public void RegisterMoveCommand_ResolvesCorrectly()
    {
        // Настраиваем заглушку для адаптера в IoC
        var movingObjectMock = new Mock<IMovingObject>();
        var dummyGameObject = new object();

        IoC.Register("Adapters.IMovingObject", (args) => movingObjectMock.Object);

        // Регистрируем Commands.Move
        ICommand registerCmd = new RegisterIoCDependencyMoveCommand();
        registerCmd.Execute();

        // Проверяем разрешение
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Move", dummyGameObject);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<MoveCommand>(resolvedCommand);
    }
}
