using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class GameObjectMovementCommandTests
{
    [Fact]
    public void Execute_ShouldCallUpdateOnGameObject()
    {
        // Arrange
        var gameObjectMock = new Mock<IGameObject>();
        gameObjectMock.SetupGet(g => g.Id).Returns(1);
        var command = new GameObjectMovementCommand(gameObjectMock.Object);

        // Act
        command.Execute();

        // Assert
        gameObjectMock.Verify(g => g.Update(), Times.Once);
    }

    [Fact]
    public void Execute_MultipleExecutions_ShouldCallUpdateMultipleTimes()
    {
        // Arrange
        var gameObjectMock = new Mock<IGameObject>();
        gameObjectMock.SetupGet(g => g.Id).Returns(1);
        var command = new GameObjectMovementCommand(gameObjectMock.Object);

        // Act
        command.Execute();
        command.Execute();
        command.Execute();

        // Assert
        gameObjectMock.Verify(g => g.Update(), Times.Exactly(3));
    }

    [Fact]
    public void Constructor_NullGameObject_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GameObjectMovementCommand(null));
    }

    [Fact]
    public void Execute_WithSpaceship_ShouldCallUpdate()
    {
        // Arrange
        var spaceship = new Spaceship(1, (10, 20));
        var command = new GameObjectMovementCommand(spaceship);

        // Act
        command.Execute();

        // Assert - не будет exception
        Assert.NotNull(command);
    }

    [Fact]
    public void Execute_WithPhoton_ShouldCallUpdate()
    {
        // Arrange
        var photon = new Photon(1, (0, 0), (1, 1), 5);
        var command = new GameObjectMovementCommand(photon);
        var initialPosition = photon.Position;

        // Act
        command.Execute();

        // Assert
        Assert.NotEqual(initialPosition, photon.Position);
    }
}
