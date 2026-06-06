using System.Linq;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class GameWithAuthTests
{
    [Fact]
    public void GameTick_WithAuthCommand_ShouldValidatePlayerAccess()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);
        var game = new Game(repository, factory);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("player_1");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 1))
            .Returns(true);

        var spaceship = new Spaceship(1, (10, 20));
        repository.Add(spaceship);

        // Авторизация + стрельба
        var authCommand = new AuthCommand(playerMock.Object, 1, authServiceMock.Object);
        var fireCommand = new FirePhotonCommand(spaceship, (1, 0), repository);

        game.InjectCommand(authCommand);
        game.InjectCommand(fireCommand);

        // Act
        game.Tick();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        Assert.Equal((11, 20), photon.Position);
    }

    [Fact]
    public void GameTick_WithFailedAuth_ShouldContinueProcessing()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);
        var game = new Game(repository, factory);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("hacker");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 1))
            .Returns(false);

        var spaceship = new Spaceship(1, (0, 0));
        repository.Add(spaceship);

        // Ошибка авторизации + стрельба
        var authCommand = new AuthCommand(playerMock.Object, 1, authServiceMock.Object);
        var fireCommand = new FirePhotonCommand(spaceship, (1, 0), repository);

        game.InjectCommand(authCommand); // Это выкинет exception, но game продолжит работу
        game.InjectCommand(fireCommand);

        // Act
        game.Tick();

        // Assert
        // Второе командо все равно исполнится из-за try-catch в Game.Tick()
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
    }

    [Fact]
    public void GameTick_MultiplePlayersAuth_ShouldCheckEachPlayer()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);
        var game = new Game(repository, factory);

        var player1Mock = new Mock<IPlayer>();
        player1Mock.SetupGet(p => p.Id).Returns("admin");

        var player2Mock = new Mock<IPlayer>();
        player2Mock.SetupGet(p => p.Id).Returns("user");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock.Setup(s => s.CheckAccess(player1Mock.Object, 1)).Returns(true);
        authServiceMock.Setup(s => s.CheckAccess(player2Mock.Object, 1)).Returns(true);

        var spaceship = new Spaceship(1, (0, 0));
        repository.Add(spaceship);

        // Оба игрока авторизуются
        var authCommand1 = new AuthCommand(player1Mock.Object, 1, authServiceMock.Object);
        var authCommand2 = new AuthCommand(player2Mock.Object, 1, authServiceMock.Object);
        var fireCommand = new FirePhotonCommand(spaceship, (1, 0), repository);

        game.InjectCommand(authCommand1);
        game.InjectCommand(authCommand2);
        game.InjectCommand(fireCommand);

        // Act
        game.Tick();

        // Assert
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        authServiceMock.Verify(s => s.CheckAccess(It.IsAny<IPlayer>(), 1), Times.Exactly(2));
    }
}
