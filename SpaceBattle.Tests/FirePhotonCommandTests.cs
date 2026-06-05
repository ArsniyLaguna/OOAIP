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
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);
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
        var spaceship = new Spaceship(1, (0, 0));
        var command1 = new FirePhotonCommand(spaceship, (1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, (0, 1), repository);

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
        var spaceshipPosition = (50, 75);
        var spaceship = new Spaceship(1, spaceshipPosition);
        var direction = (1, 1);
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
            new FirePhotonCommand(null, (1, 0), repository));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        // Arrange
        var spaceship = new Spaceship(1, (0, 0));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(spaceship, (1, 0), null));
    }

    [Fact]
    public void Execute_PhotonShouldHaveCorrectDirection()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (0, 0));
        var direction = (1, -1);
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
        var spaceship = new Spaceship(1, (0, 0));
        var command1 = new FirePhotonCommand(spaceship, (1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, (0, 1), repository);

        // Act
        command1.Execute();
        command2.Execute();

        // Assert
        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.NotEqual(photons[0].Id, photons[1].Id);
    }
}
