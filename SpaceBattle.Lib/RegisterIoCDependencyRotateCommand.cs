namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<object>(
            "IoC.Register",
            "Commands.Rotate",
            new Func<object[], object>(args =>
            {
                var uObject = args[0];
                var rotatableAdapter = IoC.Resolve<IRotatable>("Adapters.IRotatable", uObject);
                return new RotateCommand(rotatableAdapter);
            })
        );
    }
}