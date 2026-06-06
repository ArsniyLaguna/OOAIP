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
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("player_123");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 42))
            .Returns(true);

        var command = new AuthCommand(playerMock.Object, 42, authServiceMock.Object);

        var exception = Record.Exception(() => command.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void Execute_WithInvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("hacker");

        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess(playerMock.Object, 42))
            .Returns(false);

        var command = new AuthCommand(playerMock.Object, 42, authServiceMock.Object);

        Assert.Throws<UnauthorizedAccessException>(() => command.Execute());
    }

    [Fact]
    public void Constructor_NullPlayer_ShouldThrowArgumentNullException()
    {
        var authServiceMock = new Mock<IAuthService>();

#pragma warning disable CS8625 
        Assert.Throws<ArgumentNullException>(() => new AuthCommand(null, 1, authServiceMock.Object));
#pragma warning restore CS8625
    }

    [Fact]
    public void Constructor_NullService_ShouldThrowArgumentNullException()
    {
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupGet(p => p.Id).Returns("player_1");

#pragma warning disable CS8625
        Assert.Throws<ArgumentNullException>(() => new AuthCommand(playerMock.Object, 1, null));
#pragma warning restore CS8625 
    }
}
