using System;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Register("Commands.Move", args =>
        {
            var obj = args[0];
            var movableObject = IoC.Resolve<IMovable>("Adapters.IMovingObject", obj);
            return new MoveCommand(movableObject);
        });
    }
}
