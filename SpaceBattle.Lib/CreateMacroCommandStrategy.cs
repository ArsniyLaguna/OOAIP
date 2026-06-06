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
        // 1. Получаем список имен команд, из которых состоит макрос
        var commandNames = IoC.Resolve<IEnumerable<string>>(_commandSpec);

        // 2. Создаем экземпляры всех этих команд, передавая аргументы (например, игровой объект)
        var commands = commandNames.Select(name => IoC.Resolve<ICommand>(name, args));

        // 3. Возвращаем готовую макро-команду
        return new MacroCommand(commands);
    }
}
