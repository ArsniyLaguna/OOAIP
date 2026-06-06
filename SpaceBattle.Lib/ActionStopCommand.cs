using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class ActionStopCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public ActionStopCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var injectableCommand = (ICommandInjectable)_order["TargetCommand"];
        
        injectableCommand.Inject(new EmptyCommand());
    }
}