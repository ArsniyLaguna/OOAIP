namespace SpaceBattle.Lib;

public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        IoC.Register("Commands.CommandInjectable", (args) =>
        {
            return new CommandInjectableCommand();
        });
    }
}
