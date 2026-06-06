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
            
            //возвращаем созданную команду старта
            return new ActionStartCommand(order);
        });
    }
}
