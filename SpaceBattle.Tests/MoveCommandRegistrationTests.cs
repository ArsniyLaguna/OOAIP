using Xunit;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MoveCommandRegistrationTests
{
    [Fact]
    public void Execute_ShouldRegisterMoveCommandInIoC_AndResolveCorrectly()
    {
        // Arrange
        var gameObject = new object();
        var movableMock = new Mock<IMovable>();

        IoC.Register("Adapters.IMovingObject", args => movableMock.Object);

        var registerCommand = new RegisterIoCDependencyMoveCommand();

        // Act
        registerCommand.Execute();
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Move", gameObject);

        // Assert
        Assert.NotNull(resolvedCommand);
        Assert.IsType<MoveCommand>(resolvedCommand);
    }
}
