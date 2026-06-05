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
        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess("valid_token", 42))
            .Returns(true);

        var command = new AuthCommand("valid_token", 42, authServiceMock.Object);

        // Act & Assert
        var exception = Record.Exception(() => command.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void Execute_WithInvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        var authServiceMock = new Mock<IAuthService>();
        authServiceMock
            .Setup(s => s.CheckAccess("hacker_token", 42))
            .Returns(false);

        var command = new AuthCommand("hacker_token", 42, authServiceMock.Object);

        Assert.Throws<UnauthorizedAccessException>(() => command.Execute());
    }

    [Fact]
    public void Constructor_NullToken_ShouldThrowArgumentNullException()
    {
        var authServiceMock = new Mock<IAuthService>();

        Assert.Throws<ArgumentNullException>(() => new AuthCommand(null!, 1, authServiceMock.Object));
    }

    [Fact]
    public void Constructor_NullService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AuthCommand("token", 1, null!));
    }
}
