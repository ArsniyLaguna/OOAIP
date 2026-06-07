using Xunit;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class TestAuthContext : IAuthContext
{
    public string Token { get; set; } = string.Empty;
    public string GameId { get; set; } = string.Empty;
    public string PlayerId { get; set; } = string.Empty;
}

public class AuthTests
{
    [Fact]
    public void AuthCommand_WithValidToken_ExecutesWithoutException()
    {
        IoC.Reset();

        new RegisterIoCDependencyAuth().Execute();

        var context = new TestAuthContext
        {
            Token = "valid_token",
            GameId = "game_1",
            PlayerId = "player_1"
        };

        var authCmd = IoC.Resolve<ICommand>("Commands.Auth", context);
        var exception = Record.Exception(() => authCmd.Execute());

        Assert.Null(exception);
    }

    [Fact]
    public void AuthCommand_WithEmptyToken_ThrowsException()
    {
        IoC.Reset();

        new RegisterIoCDependencyAuth().Execute();

        var context = new TestAuthContext
        {
            Token = "",
            GameId = "game_1",
            PlayerId = "player_1"
        };

        var authCmd = IoC.Resolve<ICommand>("Commands.Auth", context);

        Assert.Throws<Exception>(() => authCmd.Execute());
    }
}
