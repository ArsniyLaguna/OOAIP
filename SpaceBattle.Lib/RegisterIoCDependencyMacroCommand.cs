namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    private class MacroStrategy : IStrategy
    {
        public object Invoke(params object[] args)
        {
            var commands = (IEnumerable<ICommand>)args[0];
            return new MacroCommand(commands);
        }
    }

    public void Execute()
    {
        IoC.Resolve<object>("IoC.Register", "Commands.Macro", new MacroStrategy());
    }
}