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
        // 1. Получаем список имен команд
        var commandNames = IoC.Resolve<IEnumerable<string>>(_commandSpec, args);
        
        // 2. Создаем команды
        var commands = commandNames.Select(name => IoC.Resolve<ICommand>(name, args));
        
        // 3. Возвращаем макрос
        return new MacroCommand(commands);
    }
}
