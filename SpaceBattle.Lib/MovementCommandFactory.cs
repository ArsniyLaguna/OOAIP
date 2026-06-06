namespace SpaceBattle.Lib;

public class MovementCommandFactory
{
    private readonly IGameObjectRepository _repository;

    public MovementCommandFactory(IGameObjectRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<ICommand> CreateMovementCommandsForAll()
    {
        var commands = new List<ICommand>();
        
        foreach (var gameObject in _repository.GetAll())
        {
            commands.Add(new GameObjectMovementCommand(gameObject));
        }

        return commands;
    }

    public ICommand CreateMovementCommand(int gameObjectId)
    {
        var gameObject = _repository.Get(gameObjectId);
        if (gameObject == null)
            throw new ArgumentException($"Game object with id {gameObjectId} not found", nameof(gameObjectId));

        return new GameObjectMovementCommand(gameObject);
    }
}
