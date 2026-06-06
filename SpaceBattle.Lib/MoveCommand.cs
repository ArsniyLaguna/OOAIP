namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovable _obj;

    public MoveCommand(IMovable obj)
    {
        _obj = obj;
    }

    public void Execute()
    {
        _obj.Position += _obj.Velocity;
    }
}
