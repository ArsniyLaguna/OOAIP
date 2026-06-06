using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib
{
public static class IoC
{
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    static IoC()
    {
        // Упрощаем регистрацию: передаем просто ключ и делегат
        _strategies["IoC.Register"] = (args) =>
        {
            var key = (string)args[0];
            // Здесь мы ожидаем делегат. Если передается мок, он сломается.
            var strategy = (Func<object[], object>)args[1];
            _strategies[key] = strategy;
            return null!; 
        };
    }

    public static T Resolve<T>(string key, params object[] args)
    {
        // Специальная обработка для IoC.Register, чтобы не искать в словаре, 
        // если стратегия еще не добавлена
        if (key == "IoC.Register")
        {
            return (T)_strategies["IoC.Register"](args);
        }

        if (_strategies.TryGetValue(key, out var strategy))
        {
            return (T)strategy(args);
        }
        throw new Exception($"Зависимость {key} не зарегистрирована.");
    }

    public static void Register(string key, Func<object[], object> strategy)
    {
        _strategies[key] = strategy;
    }
}
}
