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
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(10, 20));
        var direction = new Vector(1, 0);
        var command = new FirePhotonCommand(spaceship, direction, repository);

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
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(0, 0));
        var command1 = new FirePhotonCommand(spaceship, new Vector(1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, new Vector(0, 1), repository);

        // Act
        command1.Execute();
        command2.Execute();

        // Assert
        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.Equal(2, photons.Count);
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
        // Arrange
        var repository = new GameObjectRepository();
        var spaceshipPosition = new Vector(50, 75);
        var spaceship = new Spaceship(1, spaceshipPosition);
        var direction = new Vector(1, 1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        // Act
        command.Execute();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().First();
        Assert.Equal(spaceshipPosition, photon.Position);
    }

    [Fact]
    public void Constructor_NullSpaceship_ShouldThrowArgumentNullException()
    {
        // Arrange
        var repository = new GameObjectRepository();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(null, new Vector(1, 0), repository));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        // Arrange
        var spaceship = new Spaceship(1, new Vector(0, 0));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(spaceship, new Vector(1, 0), null));
    }

    [Fact]
    public void Spaceship_Properties_ShouldBeAccessible()
    {
        var spaceship = new Spaceship(10, (5, 5));
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(0, 0));
        var direction = new Vector(1, -1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

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
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(0, 0));
        var command1 = new FirePhotonCommand(spaceship, new Vector(1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, new Vector(0, 1), repository);

        // Act
        command1.Execute();
        command2.Execute();

        // Assert
        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.NotEqual(photons[0].Id, photons[1].Id);
    }

    [Fact]
    public void Photon_Update_ShouldMovePhoton()
    {
        // Arrange
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 1), 5);
        var initialPosition = photon.Position;

        // Act
        photon.Update();

        // Assert
        Assert.Equal(new Vector(5, 5), photon.Position);
        Assert.NotEqual(initialPosition, photon.Position);
    }

    [Fact]
    public void Photon_Constructor_InvalidSpeed_ShouldThrowArgumentException()
    {
        var initialPos = (1, 2);
        var velocity = (1, 0);

        Assert.Throws<ArgumentException>(() => new Photon(1, initialPos, velocity, speed: 0));
        Assert.Throws<ArgumentException>(() => new Photon(1, initialPos, velocity, speed: -1));
        // Arrange
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 0), 2);

        // Act
        photon.Update();
        photon.Update();
        photon.Update();

        // Assert
        Assert.Equal(new Vector(6, 0), photon.Position);
    }

    [Fact]
    public void Photon_GetDirection_ShouldReturnDirection()
    {
        var direction = (2, 3);
        var photon = new Photon(1, (0, 0), direction);

        Assert.Equal(direction, photon.GetDirection());
        // Arrange
        var photon = new Photon(1, new Vector(10, 10), new Vector(-1, -1), 2);

        // Act
        photon.Update();

        // Assert
        Assert.Equal(new Vector(8, 8), photon.Position);
    }

    [Fact]
    public void Photon_InvalidSpeed_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Photon(1, new Vector(0, 0), new Vector(1, 0), 0));
    }

    [Fact]
    public void Photon_GetSpeed_ShouldReturnSpeed()
    {
        var photon = new Photon(1, (0, 0), (1, 0), speed: 5);
        // Arrange
        var spaceship = new Spaceship(1, new Vector(10, 20));

        // Act
        var photon = spaceship.FirePhoton(new Vector(1, 0));

        Assert.Equal(5, photon.GetSpeed());
    }

    [Fact]
    public void Photon_MultipleUpdates_ShouldAccumulateMovement()
    {
        var photon = new Photon(1, (0, 0), (1, 1), speed: 2);

        photon.Update(); // (0, 0) + (1, 1) * 2 = (2, 2)
        photon.Update(); // (2, 2) + (1, 1) * 2 = (4, 4)

        Assert.Equal((4, 4), photon.Position);
        // Arrange
        var spaceship = new Spaceship(1, new Vector(0, 0));

        // Act
        var photon1 = spaceship.FirePhoton(new Vector(1, 0));
        var photon2 = spaceship.FirePhoton(new Vector(0, 1));

        // Assert
        Assert.NotEqual(photon1.Id, photon2.Id);
    }

    [Fact]
    public void Spaceship_Update_ShouldNotThrowException()
    {
        // Arrange
        var spaceship = new Spaceship(1, new Vector(0, 0));

        // Act & Assert
        spaceship.Update();
    }
}
