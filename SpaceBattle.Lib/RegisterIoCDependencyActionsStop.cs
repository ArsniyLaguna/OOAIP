namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Register("Actions.Stop", (args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            return new EmptyCommand();
        });
    }
}
