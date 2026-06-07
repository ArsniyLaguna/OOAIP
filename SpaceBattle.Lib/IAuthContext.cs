namespace SpaceBattle.Lib;

public interface IAuthContext
{
    string Token { get; }
    string GameId { get; }
    string PlayerId { get; }
}
