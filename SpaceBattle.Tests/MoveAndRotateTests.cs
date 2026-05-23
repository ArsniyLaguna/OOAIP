using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MoveAndRotateTests
{
    [Fact]
    public void RegisterMoveCommand_ResolvesCorrectly()
    {
        var movingObjectMock = new Mock<IMovingObject>();
        var dummyGameObject = new object();

        IoC.Register("Adapters.IMovingObject", (args) => movingObjectMock.Object);

        ICommand registerCmd = new RegisterIoCDependencyMoveCommand();
        registerCmd.Execute();
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Move", dummyGameObject);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<MoveCommand>(resolvedCommand);
    }
}
