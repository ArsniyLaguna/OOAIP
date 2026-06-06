using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class CoverageBoostTests
{

    [Fact]
    public void Game_Tick_WhenCommandThrowsException_ShouldIgnoreAndContinue()
    {
        // Arrange
        var repositoryMock = new Mock<IGameObjectRepository>();
        var factoryMock = new Mock<MovementCommandFactory>(repositoryMock.Object);
        factoryMock.Setup(f => f.CreateMovementCommandsForAll()).Returns(new List<ICommand>());

        var game = new Game(repositoryMock.Object, factoryMock.Object);
        var badCommandMock = new Mock<ICommand>();
        
        // Симулируем выброс исключения внутри команды, чтобы зайти в блок catch(Exception) в Game.Tick
        badCommandMock.Setup(c => c.Execute()).Throws(new Exception("Тестовое подавление ошибки"));
        game.InjectCommand(badCommandMock.Object);

        // Act
        var exception = Record.Exception(() => game.Tick());

        // Assert
        Assert.Null(exception); // Игра не должна упасть, блок catch должен сработать
    }

    [Fact]
    public void Game_Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        var factoryMock = new Mock<MovementCommandFactory>(new Mock<IGameObjectRepository>().Object);
        Assert.Throws<ArgumentNullException>(() => new Game(null!, factoryMock.Object));
    }

    [Fact]
    public void Game_Constructor_NullFactory_ShouldThrowArgumentNullException()
    {
        var repositoryMock = new Mock<IGameObjectRepository>();
        Assert.Throws<ArgumentNullException>(() => new Game(repositoryMock.Object, null!));
    }


    [Fact]
    public void FirePhotonCommand_Execute_ShouldCreatePhoton_AddToRepository_AndEnqueueMovement()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, (10, 20));
        var direction = (1, 0);
        var gameQueue = new Queue<ICommand>();
        var moveCommandMock = new Mock<ICommand>();

        // Фабричный метод-лямбда для изоляции от конкретных классов команд
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
        
        // Проверяем запуск: команда движения должна оказаться в очереди
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
    public void GameObjectRepository_Add_And_Get_ShouldWorkCorrectly()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var gameObjectMock = new Mock<IGameObject>();
        gameObjectMock.SetupGet(g => g.Id).Returns(42);

        // Act
        repository.Add(gameObjectMock.Object);
        var result = repository.Get(42);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(42, result.Id);
    }

    [Fact]
    public void GameObjectRepository_Add_Null_ShouldThrowArgumentNullException()
    {
        var repository = new GameObjectRepository();
        Assert.Throws<ArgumentNullException>(() => repository.Add(null!));
    }

    [Fact]
    public void GameObjectRepository_Get_NonExistingId_ShouldReturnNull()
    {
        var repository = new GameObjectRepository();
        Assert.Null(repository.Get(999));
    }

    [Fact]
    public void GameObjectRepository_Remove_ShouldDeleteObject()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var gameObjectMock = new Mock<IGameObject>();
        gameObjectMock.SetupGet(g => g.Id).Returns(5);
        repository.Add(gameObjectMock.Object);

        // Act
        repository.Remove(5);

        // Assert
        Assert.Null(repository.Get(5));
    }


    [Fact]
    public void UserSession_Properties_ShouldRetainConstructorValues()
    {
        // Arrange
        var token = "secure_token_123";
        var playerId = 777;
        var expireTime = DateTime.UtcNow.AddHours(2);

        // Act
        var session = new UserSession(token, playerId, expireTime);

        // Assert
        Assert.Equal(token, session.Token);
        Assert.Equal(playerId, session.PlayerId);
        Assert.Equal(expireTime, session.ExpireTime);
    }
}
