public class GameObjectMovementCommand : ICommand
{
    private readonly IMovable _gameObject;
    
    public GameObjectMovementCommand(IMovable gameObject)
    private readonly IMovable _movable;
    
    public GameObjectMovementCommand(IMovable movable)
    {
        _movable = movable ?? throw new ArgumentNullException(nameof(movable));
    }

    public void Execute()
    {
        _movable.Update();
    }
}
