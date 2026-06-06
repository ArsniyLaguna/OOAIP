using System;
using System.Collections.Generic;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void Execute_ShouldCreatePhotonAndEnqueueMovementCommand()
    {
        var shooterMock = new Mock<IShootable>();
        shooterMock.SetupGet(s => s.Position).Returns((10, 20));
        shooterMock.SetupGet(s => s.Direction).Returns((1, 0));

        var gameQueue = new Queue<ICommand>();

        var photonMock = new Mock<IGameObject>();
        var moveCommandMock = new Mock<ICommand>();

        IoC.Register("Game.CreatePhoton", args => photonMock.Object);
        IoC.Register("Commands.Move", args => moveCommandMock.Object);

        var command = new FirePhotonCommand(shooterMock.Object, gameQueue);

        command.Execute();

        Assert.Single(gameQueue);
        Assert.Equal(moveCommandMock.Object, gameQueue.Peek());
    }

    [Fact]
    public void Constructor_NullShooter_ShouldThrowArgumentNullException()
    {
        var gameQueue = new Queue<ICommand>();

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(null!, gameQueue));
    }

    [Fact]
    public void Constructor_NullQueue_ShouldThrowArgumentNullException()
    {
        var shooterMock = new Mock<IShootable>();

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(shooterMock.Object, null!));
    }

    [Fact]
    public void Spaceship_ShouldImplementIShootableAndKeepProperties()
    {
        var position = (10, 20);
        var direction = (1, 0);

        var spaceship = new Spaceship(1, position, direction);

        Assert.Equal(position, spaceship.Position);
        Assert.Equal(direction, spaceship.Direction);
    }

    [Fact]
    public void Spaceship_Update_ShouldNotThrowException()
    {
        var spaceship = new Spaceship(1, (0, 0), (1, 0));

        spaceship.Update();
    }
}
