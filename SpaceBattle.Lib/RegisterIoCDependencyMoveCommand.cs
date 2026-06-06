namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            IoC.Register("Commands.Move", (args) =>
            {
                var obj = args[0];
                var movable = IoC.Resolve<IMovable>("Adapters.IMovable", obj);
                return new MoveCommand(movable);
            });
        }
    }
}
