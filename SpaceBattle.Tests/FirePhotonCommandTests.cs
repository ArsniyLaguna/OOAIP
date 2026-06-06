using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class FirePhotonCommandTests
{

    [Fact]
    public void FirePhotonCommand_Execute_ShouldCreatePhoton_AddToRepository_AndEnqueueMovement()
    {
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);
        var gameQueue = new Queue<ICommand>();
        var moveCommandMock = new Mock<ICommand>();

        var command = new FirePhotonCommand(
            spaceship, 
            direction, 
            repository, 
            gameQueue, 
            photon => moveCommandMock.Object
        );

        command.Execute();

        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
        
        Assert.Single(gameQueue);
        Assert.Equal(moveCommandMock.Object, gameQueue.Peek());
    }

    [Fact]
    public void FirePhotonCommand_Constructor_NullArguments_ShouldThrowArgumentNullException()
    {
        var repository = new GameObjectRepository();
        var gameQueue = new Queue<ICommand>();
        var spaceship = new Spaceship(1, (0, 0));
        Func<IGameObject, ICommand> dummyFactory = obj => new Mock<ICommand>().Object;

        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(null!, (1, 0), repository, gameQueue, dummyFactory));
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(spaceship, (1, 0), null!, gameQueue, dummyFactory));
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(spaceship, (1, 0), repository, null!, dummyFactory));
        Assert.Throws<ArgumentNullException>(() => new FirePhotonCommand(spaceship, (1, 0), repository, gameQueue, null!));
    }


    [Fact]
    public void Spaceship_Properties_And_Update_ShouldBeCovered()
    {
        var spaceship = new Spaceship(10, (5, 5));

        spaceship.Position = (20, 30);
        spaceship.Update();

        Assert.Equal(10, spaceship.Id);
        Assert.Equal((20, 30), spaceship.Position);
    }

[Fact]
    public void Photon_Properties_And_Update_ShouldBeCovered()
    {
        var initialPos = (1, 2);
        var velocity = (0, 1);
        var photon = new Photon(100, initialPos, velocity);

        photon.Update();

        Assert.Equal(100, photon.Id);
        Assert.Equal((1, 3), photon.Position);
    }
}
