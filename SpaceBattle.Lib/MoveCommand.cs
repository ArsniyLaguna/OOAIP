namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _obj;

    public MoveCommand(IMovingObject obj)
    {
        _obj = obj;
    }

    public void Execute()
    {
    }
}
