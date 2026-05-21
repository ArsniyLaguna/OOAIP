using System;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        SpaceBattle.Lib.IoC.Register("Commands.Move", args =>
        {
            var obj = args[0];
            var movableObject = SpaceBattle.Lib.IoC.Resolve<IMovable>("Adapters.IMovingObject", obj);
            return new MoveCommand(movableObject);
        });
    }
}
