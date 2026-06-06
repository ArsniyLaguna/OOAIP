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
    public void Execute_ShouldCreatePhoton_AddToRepository_AndEnqueueMovement()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);
        var gameQueue = new Queue<ICommand>();
        
        var moveCommandMock = new Mock<ICommand>();

        // Передаем лямбду в качестве фабрики команд движения
        var command = new FirePhotonCommand(
            spaceship, 
            direction, 
            repository, 
            gameQueue, 
            photon => moveCommandMock.Object
        );

        // Act
        command.Execute();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        Assert.Equal(spaceship.Position, photon.Position);
        
        // Проверяем, что движение торпеды улетело в игровую очередь
        Assert.Single(gameQueue);
        Assert.Equal(moveCommandMock.Object, gameQueue.Peek());
    }

    [Fact]
    public void Constructor_NullSpaceship_ShouldThrowArgumentNullException()
    {
        var repository = new GameObjectRepository();
        var gameQueue = new Queue<ICommand>();
        Func<IGameObject, ICommand> dummyFactory = obj => new Mock<ICommand>().Object;

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(null!, (1, 0), repository, gameQueue, dummyFactory));
    }

    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        var spaceship = new Spaceship(1, (0, 0));
        var gameQueue = new Queue<ICommand>();
        Func<IGameObject, ICommand> dummyFactory = obj => new Mock<ICommand>().Object;

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(spaceship, (1, 0), null!, gameQueue, dummyFactory));
    }

    [Fact]
    public void Constructor_NullQueue_ShouldThrowArgumentNullException()
    {
        var spaceship = new Spaceship(1, (0, 0));
        var repository = new GameObjectRepository();
        Func<IGameObject, ICommand> dummyFactory = obj => new Mock<ICommand>().Object;

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(spaceship, (1, 0), repository, null!, dummyFactory));
    }

    [Fact]
    public void Constructor_NullFactory_ShouldThrowArgumentNullException()
    {
        var spaceship = new Spaceship(1, (0, 0));
        var repository = new GameObjectRepository();
        var gameQueue = new Queue<ICommand>();

        Assert.Throws<ArgumentNullException>(() => 
            new FirePhotonCommand(spaceship, (1, 0), repository, gameQueue, null!));
    }
}
