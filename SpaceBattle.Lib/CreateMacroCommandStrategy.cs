namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy : IStrategy
{
    private readonly string _commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        _commandSpec = commandSpec;
    }

    public object Invoke(params object[] args)
    {
        var commandNames = IoC.Resolve<IEnumerable<string>>(_commandSpec);

        var commands = commandNames.Select(name => IoC.Resolve<ICommand>(name, args));

        return new MacroCommand(commands);
    }
}