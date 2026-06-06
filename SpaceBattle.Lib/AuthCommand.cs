namespace SpaceBattle.Lib;

public class AuthCommand : ICommand
{
    private readonly IPlayer _player;
    private readonly int _gameObjectId;
    private readonly IAuthService _authService;

    public AuthCommand(IPlayer player, int gameObjectId, IAuthService authService)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _gameObjectId = gameObjectId;
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public void Execute()
    {
        if (!_authService.CheckAccess(_player, _gameObjectId))
        {
            throw new UnauthorizedAccessException($"Игрок с ID '{_player.Id}' не имеет прав на объект с ID {_gameObjectId}.");
        }
    }
}
