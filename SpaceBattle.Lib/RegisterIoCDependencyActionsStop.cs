using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Register("Actions.Stop", (args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            var injectableCommand = (ICommandInjectable)order["TargetCommand"];

            return new ActionCommand(() =>
            {
                injectableCommand.Inject(new EmptyCommand());
            });
        });
    }
}

public class ActionCommand : ICommand
{
    private readonly Action _action;
    public ActionCommand(Action action) => _action = action;
    public void Execute() => _action();
}