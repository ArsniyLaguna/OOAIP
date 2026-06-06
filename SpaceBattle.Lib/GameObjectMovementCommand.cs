public class GameObjectMovementCommand : ICommand
{
    private readonly IMovable _gameObject;
    
    public GameObjectMovementCommand(IMovable gameObject)
    {
        _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
    }

    public void Execute()
    {
        _gameObject.Update();
    }
}
