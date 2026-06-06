namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            IoC.Register("Commands.Move", (args) => new MoveCommand((IMovingObject)args[0]));
        }
    }
}
