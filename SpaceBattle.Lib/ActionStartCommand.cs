using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class ActionStartCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public ActionStartCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var targetCommand = _order.ContainsKey("Command") ? (ICommand)_order["Command"] : new EmptyCommand();
        var queue = (Queue<ICommand>)_order["Queue"];

        var injectableCommand = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        injectableCommand.Inject(targetCommand);

        queue.Enqueue(injectableCommand);
    }
}
