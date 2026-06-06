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
        // Получаем список имен команд и передаем args, если спецификации нужно знать контекст
        var commandNames = IoC.Resolve<IEnumerable<string>>(_commandSpec, args);
        
        // Создаем экземпляры команд
        var commands = commandNames.Select(name => IoC.Resolve<ICommand>(name, args));
        
        // Возвращаем готовую макро-команду
        return new MacroCommand(commands);
    }
}
