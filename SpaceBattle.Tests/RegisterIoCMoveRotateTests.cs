 namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        // Регистрация макрокоманды перемещения на базе Specs.Move
        IoC.Register("Macro.Move", (args) =>
        {
            var strategy = new CreateMacroCommandStrategy("Specs.Move");
            return strategy.Invoke(args);
        });

        // Регистрация макрокоманды вращения на базе Specs.Rotate
        IoC.Register("Macro.Rotate", (args) =>
        {
            var strategy = new CreateMacroCommandStrategy("Specs.Rotate");
            return strategy.Invoke(args);
        });
    }
}
