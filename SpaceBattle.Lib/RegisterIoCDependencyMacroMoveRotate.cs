namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        // Регистрация макрокоманды перемещения на базе Specs.Move
        IoC.Resolve<object>(
            "IoC.Register", 
            "Macro.Move", 
            new CreateMacroCommandStrategy("Specs.Move")
        );

        // Регистрация макрокоманды вращения на базе  Specs.Rotate
        IoC.Resolve<object>(
            "IoC.Register", 
            "Macro.Rotate", 
            new CreateMacroCommandStrategy("Specs.Rotate")
        );
    }
}