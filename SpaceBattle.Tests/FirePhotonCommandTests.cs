using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void Execute_ShouldCreatePhotonAndAddToRepository()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(10, 20));
        var direction = new Vector(1, 0);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        // Act
        command.Execute();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
    }

    [Fact]
    public void Execute_MultipleShots_ShouldCreateMultiplePhotons()
    {
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
    public void Execute_PhotonShouldHaveSamePositionAsSpaceship()
    {
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(null, (1, 0), repository));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            new FirePhotonCommand(null, new Vector(1, 0), repository));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        // Arrange
        var spaceship = new Spaceship(1, new Vector(0, 0));

        // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(spaceship, (1, 0), null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            new FirePhotonCommand(spaceship, new Vector(1, 0), null));
    }

    [Fact]
    public void Execute_PhotonShouldHaveCorrectDirection()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(0, 0));
        var direction = new Vector(1, -1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        // Act
        command.Execute();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().First();
        Assert.Equal(direction, photon.GetDirection());
    }

    [Fact]
    public void Execute_PhotonShouldHaveUniqueIds()
    {
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
    public void Photon_MultipleUpdates_ShouldMovePhotonMultipleTimes()
    {
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
    public void Photon_NegativeDirection_ShouldMoveInNegativeDirection()
    {
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
    public void Spaceship_FirePhoton_ShouldReturnPhoton()
    {
        // Arrange
        var spaceship = new Spaceship(1, new Vector(10, 20));

        // Act
        var photon = spaceship.FirePhoton(new Vector(1, 0));

        // Assert
        Assert.NotNull(photon);
        Assert.IsType<Photon>(photon);
    }

    [Fact]
    public void Spaceship_FireMultiplePhotons_ShouldHaveDifferentIds()
    {
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
