using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void Execute_ShouldCreatePhotonAndAddToRepository()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        command.Execute();

        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
    }

    [Fact]
    public void Execute_MultipleShots_ShouldCreateMultiplePhotons()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (0, 0));
        var command1 = new FirePhotonCommand(spaceship, (1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, (0, 1), repository);

        command1.Execute();
        command2.Execute();

        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.Equal(2, photons.Count);
    }

    [Fact]
    public void Execute_PhotonShouldHaveSamePositionAsSpaceship()
    {
        var repository = new GameObjectRepository();
        var spaceshipPosition = (50, 75);
        var spaceship = new Spaceship(1, spaceshipPosition);
        var direction = (1, 1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        command.Execute();

        var photon = repository.GetAll().OfType<Photon>().First();
        Assert.Equal(spaceshipPosition, photon.Position);
    }

    [Fact]
    public void Constructor_NullSpaceship_ShouldThrowArgumentNullException()
    {
        var repository = new GameObjectRepository();

        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(null, (1, 0), repository));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        var spaceship = new Spaceship(1, (0, 0));

        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(spaceship, (1, 0), null));
    }

    [Fact]
    public void Execute_PhotonShouldHaveCorrectDirection()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (0, 0));
        var direction = (1, -1);
        var command = new FirePhotonCommand(spaceship, direction, repository);

        command.Execute();

        var photon = repository.GetAll().OfType<Photon>().First();
        Assert.Equal(direction, photon.GetDirection());
    }

    [Fact]
    public void Execute_PhotonShouldHaveUniqueIds()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (0, 0));
        var command1 = new FirePhotonCommand(spaceship, (1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, (0, 1), repository);

        command1.Execute();
        command2.Execute();

        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.NotEqual(photons[0].Id, photons[1].Id);
    }
}
