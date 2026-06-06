namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            IoC.Register("Commands.Move", (args) =>
            {
                var obj = (IMovingObject)args[0];
                return new MoveCommand(obj);
            });
        }
    }
}
