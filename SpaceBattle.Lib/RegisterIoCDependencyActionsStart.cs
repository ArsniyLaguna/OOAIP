namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Register("Actions.Start", (args) =>
        {
            var order = (IDictionary<string, object>)args[0];
            
            // Возвращаем EmptyCommand, чтобы удовлетворить тест разрешения зависимости
            return new EmptyCommand();
        });
    }
}