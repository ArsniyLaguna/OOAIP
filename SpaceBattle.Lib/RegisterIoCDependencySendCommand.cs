namespace SpaceBattle.Lib;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        IoC.Register("Commands.Send", (args) =>
        {
            var command = (ICommand)args[0];
            var receiver = (ICommandReceiver)args[1];
            return new SendCommand(command, receiver);
        });
    }
}