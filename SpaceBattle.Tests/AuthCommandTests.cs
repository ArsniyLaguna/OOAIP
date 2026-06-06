using System;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class AuthCommandTests
{
    [Fact]
    public void Execute_WithValidCredentials_ShouldPassSuccessfully()
    {
        // Arrange
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("player_123");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 42))
            .Returns(true);

        var command = new AuthCommand(playerMock.Object, 42, authServiceMock.Object);

        // Act & Assert
        var exception = Record.Exception(() => command.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void Execute_WithInvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("hacker");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 42))
            .Returns(false);

        var command = new AuthCommand(playerMock.Object, 42, authServiceMock.Object);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => command.Execute());
    }

    [Fact]
    public void Constructor_NullPlayer_ShouldThrowArgumentNullException()
    {
        // Arrange
        var authServiceMock = new Mock<IAuthService>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AuthCommand(null!, 1, authServiceMock.Object));
    }

    [Fact]
    public void Constructor_NullService_ShouldThrowArgumentNullException()
    {
        // Arrange
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("player_1");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AuthCommand(playerMock.Object, 1, null!));
    }

    [Fact]
    public void Execute_WithMultiplePlayers_ShouldCheckAccessForEachPlayer()
    {
        // Arrange
        var player1Mock = new Mock<IPlayer>();
        player1Mock.SetupGet(p => p.Id).Returns("player_1");

        var player2Mock = new Mock<IPlayer>();
        player2Mock.SetupGet(p => p.Id).Returns("player_2");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock.Setup(s => s.CheckAccess(player1Mock.Object, 1)).Returns(true);
        authServiceMock.Setup(s => s.CheckAccess(player2Mock.Object, 1)).Returns(false);

        var command1 = new AuthCommand(player1Mock.Object, 1, authServiceMock.Object);
        var command2 = new AuthCommand(player2Mock.Object, 1, authServiceMock.Object);

        // Act
        command1.Execute(); // Должно пройти
        
        // Assert
        Assert.Throws<UnauthorizedAccessException>(() => command2.Execute());
    }

    [Fact]
    public void Execute_ShouldCallCheckAccessWithCorrectParameters()
    {
        // Arrange
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("admin");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(It.IsAny<IPlayer>(), It.IsAny<int>()))
            .Returns(true);

        var command = new AuthCommand(playerMock.Object, 99, authServiceMock.Object);

        // Act
        command.Execute();

        // Assert
        authServiceMock.Verify(s => s.CheckAccess(playerMock.Object, 99), Times.Once);
    }
}
