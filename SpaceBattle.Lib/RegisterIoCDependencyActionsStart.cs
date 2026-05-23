namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Register("Actions.Start", (args) =>
        {
            var order = (IDictionary<string, object>)args[0];
            return new EmptyCommand();
        });
    }
}