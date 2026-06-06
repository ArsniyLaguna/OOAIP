namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        // Регистрируем через Func, который возвращает MacroCommand
        IoC.Register("Commands.Macro", (args) =>
        {
            var commands = (IEnumerable<ICommand>)args[0];
            return new MacroCommand(commands);
        });
    }
}
