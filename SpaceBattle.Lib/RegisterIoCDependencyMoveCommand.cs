namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Register("Commands.Move", (args) =>
        {
            var obj = args[0];
            var movingObject = IoC.Resolve<IMovingObject>("Adapters.IMovingObject", obj);
            return new MoveCommand(movingObject);
        });
    }
}