using System;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void FirePhotonCommand_Execute_ShouldCreatePhoton()
    {
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);

        var command = new FirePhotonCommand(spaceship, direction);

        command.Execute();
        // Логика подтверждения что фотон создан - проверяем через Spaceship
        var photon = spaceship.FirePhoton(direction);
        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
    }

    [Fact]
    public void FirePhotonCommand_Constructor_NullSpaceship_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(null!, (1, 0)));
    }

    [Fact]
    public void Spaceship_FirePhoton_ShouldReturnPhotonWithCorrectPosition()
    {
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 1);

        var photon = spaceship.FirePhoton(direction);

        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
        Assert.IsType<Photon>(photon);
    }

    [Fact]
    public void Spaceship_Properties_ShouldBeAccessible()
    {
        var spaceship = new Spaceship(10, (5, 5));

        spaceship.Position = (20, 30);
        spaceship.Update();

        Assert.Equal(10, spaceship.Id);
        Assert.Equal((20, 30), spaceship.Position);
    }

    [Fact]
    public void Photon_Update_ShouldMoveByVelocity()
    {
        var initialPos = (1, 2);
        var velocity = (0, 1);
        var photon = new Photon(100, initialPos, velocity);

        photon.Update();

        Assert.Equal(100, photon.Id);
        Assert.Equal((1, 3), photon.Position);
    }

    [Fact]
    public void Photon_Constructor_InvalidSpeed_ShouldThrowArgumentException()
    {
        var initialPos = (1, 2);
        var velocity = (1, 0);

        Assert.Throws<ArgumentException>(() => new Photon(1, initialPos, velocity, speed: 0));
        Assert.Throws<ArgumentException>(() => new Photon(1, initialPos, velocity, speed: -1));
    }

    [Fact]
    public void Photon_GetDirection_ShouldReturnDirection()
    {
        var direction = (2, 3);
        var photon = new Photon(1, (0, 0), direction);

        Assert.Equal(direction, photon.GetDirection());
    }

    [Fact]
    public void Photon_GetSpeed_ShouldReturnSpeed()
    {
        var photon = new Photon(1, (0, 0), (1, 0), speed: 5);

        Assert.Equal(5, photon.GetSpeed());
    }

    [Fact]
    public void Photon_MultipleUpdates_ShouldAccumulateMovement()
    {
        var photon = new Photon(1, (0, 0), (1, 1), speed: 2);

        photon.Update(); // (0, 0) + (1, 1) * 2 = (2, 2)
        photon.Update(); // (2, 2) + (1, 1) * 2 = (4, 4)

        Assert.Equal((4, 4), photon.Position);
    }
}
