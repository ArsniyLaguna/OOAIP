using Moq;
using SpaceBattle.Lib;
using Xunit;
using System;

namespace SpaceBattle.Tests;

public class GameObjectMovementCommandTests
{
    [Fact]
    public void Execute_ShouldCallUpdateOnGameObject()
    {
        // Используем IMovable, так как команда работает именно с ним (у него есть Update)
        var gameObjectMock = new Mock<IMovable>();
        var command = new GameObjectMovementCommand(gameObjectMock.Object);

        command.Execute();

        gameObjectMock.Verify(g => g.Update(), Times.Once);
    }

    [Fact]
    public void Execute_MultipleExecutions_ShouldCallUpdateMultipleTimes()
    {
        var gameObjectMock = new Mock<IMovable>();
        var command = new GameObjectMovementCommand(gameObjectMock.Object);

        command.Execute();
        command.Execute();
        command.Execute();

        gameObjectMock.Verify(g => g.Update(), Times.Exactly(3));
    }

    [Fact]
    public void Constructor_NullGameObject_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GameObjectMovementCommand(null!));
    }

    [Fact]
    public void Execute_WithSpaceship_ShouldUpdatePosition()
    {
        var spaceship = new Spaceship(1, new Vector(10, 20));
        spaceship.SetVelocity(new Vector(1, 1));
        var command = new GameObjectMovementCommand(spaceship);

        command.Execute();

        Assert.Equal(new Vector(11, 21), spaceship.Position);
    }

    [Fact]
    public void Execute_WithPhoton_ShouldUpdatePosition()
    {
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 1), 1);
        var command = new GameObjectMovementCommand(photon);
        var initialPosition = photon.Position;

        command.Execute();

        Assert.NotEqual(initialPosition, photon.Position);
        Assert.Equal(new Vector(1, 1), photon.Position);
    }
}
