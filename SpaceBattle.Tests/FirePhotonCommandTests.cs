using System;
using System.Linq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void FirePhotonCommand_Execute_ShouldAddPhotonToRepository()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(50, 75));
        var direction = new Vector(1, 1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        // Act
        command.Execute();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().First();
        Assert.Equal(new Vector(50, 75), photon.Position);
    }

    [Fact]
    public void FirePhotonCommand_Constructor_ShouldThrowOnNull()
    {
        var repo = new GameObjectRepository();
        var ship = new Spaceship(1, new Vector(0, 0));
        
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(null!, new Vector(1, 0), repo));
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(ship, new Vector(1, 0), null!));
    }

    [Fact]
    public void Photon_Update_ShouldMovePositionByVelocity()
    {
        // Arrange: скорость (1, 1), скорость = 5 вектор (5, 5)
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 1), speed: 5);

        // Act
        photon.Update();

        // Assert
        Assert.Equal(new Vector(5, 5), photon.Position);
    }

    [Fact]
    public void Spaceship_FirePhoton_ShouldGenerateUniqueIds()
    {
        // Arrange
        var spaceship = new Spaceship(1, new Vector(0, 0));

        // Act
        var p1 = spaceship.FirePhoton(new Vector(1, 0));
        var p2 = spaceship.FirePhoton(new Vector(0, 1));

        // Assert
        Assert.NotEqual(p1.Id, p2.Id);
    }

    [Fact]
    public void Photon_Constructor_InvalidSpeed_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new Photon(1, new Vector(0, 0), new Vector(1, 0), speed: 0));
    }
}
