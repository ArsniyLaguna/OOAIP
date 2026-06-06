using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib
{
public static class IoC
{
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    // Статический конструктор вызывается автоматически при первом использовании класса
    static IoC()
    {
        _strategies["IoC.Register"] = (args) =>
        {
            var key = (string)args[0];
            var strategy = (Func<object[], object>)args[1];
            _strategies[key] = strategy;
            return null!; // Возвращаем null, так как регистрация — это действие
        };
    }

    public static T Resolve<T>(string key, params object[] args)
    {
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
