using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Register("Actions.Start", (args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            var targetCommand = order.ContainsKey("Command") ? (ICommand)order["Command"] : new EmptyCommand();
            var queue = (Queue<ICommand>)order["Queue"];

            var injectableCommand = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        
            injectableCommand.Inject(targetCommand);

            queue.Enqueue(injectableCommand);
            
            return injectableCommand;
        });
    }
}