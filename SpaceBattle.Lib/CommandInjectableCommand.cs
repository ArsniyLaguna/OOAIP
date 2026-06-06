namespace SpaceBattle.Lib;

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _innerCommand;

    public void Inject(ICommand command)
    {
        _innerCommand = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        if (_innerCommand == null)
        {
            throw new Exception("данная команда не инициализировна");
        }
        _innerCommand.Execute();
    }
}
