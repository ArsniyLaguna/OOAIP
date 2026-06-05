using System;

namespace SpaceBattle.Lib;

public class AuthCommand : ICommand
{
    private readonly string _playerToken;
    private readonly int _gameObjectId;
    private readonly IAuthService _authService;

    public AuthCommand(string playerToken, int gameObjectId, IAuthService authService)
    {
        _playerToken = playerToken ?? throw new ArgumentNullException(nameof(playerToken));
        _gameObjectId = gameObjectId;
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public void Execute()
    {
        if (!_authService.CheckAccess(_playerToken, _gameObjectId))
        {
            throw new UnauthorizedAccessException($"Игрок с токеном '{_playerToken}' не имеет прав на объект с ID {_gameObjectId}.");
        }
    }
}
