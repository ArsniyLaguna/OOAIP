using System;
using System.Linq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{
    [Fact]
    public void Execute_ShouldCreatePhotonAndAddToRepository()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(10, 20));
        var direction = new Vector(1, 0);
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
        var spaceship = new Spaceship(1, new Vector(0, 0));
        var command1 = new FirePhotonCommand(spaceship, new Vector(1, 0), repository);
        var command2 = new FirePhotonCommand(spaceship, new Vector(0, 1), repository);

        command1.Execute();
        command2.Execute();

        var photons = repository.GetAll().OfType<Photon>().ToList();
        Assert.Equal(2, photons.Count);
    }

    [Fact]
    public void Constructor_NullSpaceship_ShouldThrowArgumentNullException()
    {
        var repository = new GameObjectRepository();

        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(null!, new Vector(1, 0), repository));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        var spaceship = new Spaceship(1, new Vector(0, 0));

        Assert.Throws<ArgumentNullException>(() =>
            new FirePhotonCommand(spaceship, new Vector(1, 0), null!));
    }

    [Fact]
    public void Photon_Update_ShouldMovePhoton()
    {
        // Фотон со скоростью 5, двигающийся по вектору (1, 1)
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 1), 5);
        
        photon.Update();

        // Ожидаем позицию: (0 + 1*5, 0 + 1*5) = (5, 5)
        Assert.Equal(new Vector(5, 5), photon.Position);
    }

    [Fact]
    public void Spaceship_FirePhoton_ShouldReturnPhoton()
    {
        var spaceship = new Spaceship(1, new Vector(10, 20));

        var photon = spaceship.FirePhoton(new Vector(1, 0));

        Assert.NotNull(photon);
        Assert.IsType<Photon>(photon);
    }
}
