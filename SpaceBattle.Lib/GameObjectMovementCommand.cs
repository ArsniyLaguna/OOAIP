namespace SpaceBattle.Lib;

public class GameObjectMovementCommand : ICommand
{
    private readonly IGameObject _gameObject;

    {
        _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
    }

    public void Execute()
    {
        _gameObject.Update();
    }
}
