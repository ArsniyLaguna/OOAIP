namespace SpaceBattle.Lib;

public class GameObjectMovementCommand : ICommand
{
    private readonly IGameObject _gameObject;

    /// <summary>
    /// Создать команду движения игрового объекта
    /// </summary>
    /// <param name="gameObject">Игровой объект для перемещения</param>
    public GameObjectMovementCommand(IGameObject gameObject)
    {
        _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
    }

    public void Execute()
    {
        _gameObject.Update();
    }
}
