using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        // Мы явно указываем тип делегата Func<object[], object>
        // чтобы избежать проблем с приведением типов при регистрации
        IoC.Register("Actions.Start", new Func<object[], object>(args => 
        {
            // Проверка на наличие аргументов, чтобы избежать IndexOutOfRangeException
            if (args == null || args.Length == 0)
            {
                throw new ArgumentException("Для команды Actions.Start требуются аргументы (IDictionary).");
            }

            var order = (IDictionary<string, object>)args[0];
            
            // Возвращаем созданную команду
            return new ActionStartCommand(order);
        }));
    }
}
