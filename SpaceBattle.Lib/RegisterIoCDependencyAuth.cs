namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAuth : ICommand
{
    public void Execute()
    {
        IoC.Register("Commands.Auth", (args) =>
        {
            return new AuthCommand((IAuthContext)args[0]);
        });

        IoC.Register("Auth.ValidateToken", (args) =>
        {
            var token = (string)args[0];
            return (object)!string.IsNullOrEmpty(token);
        });
    }
}
