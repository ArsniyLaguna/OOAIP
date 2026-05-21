using System;

namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
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
