namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _obj;

    public MoveCommand(IMovingObject obj)
    {
        _obj = obj;
    private readonly IMovable _movable;

    public MoveCommand(IMovable movable)
    {
        _movable = movable;
    }

    public void Execute()
    {
        try
        {
            _movable.Position += _movable.Velocity;
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при выполнении команды движения.", ex);
        }
    }
}
