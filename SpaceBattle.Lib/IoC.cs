using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public static class IoC
{
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    public static void Register(string key, Func<object[], object> strategy)
    {
        _strategies[key] = strategy;
    }

    public static T Resolve<T>(string key, params object[] args)
    {
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return (T)strategy(args);
        }
        throw new Exception($"Зависимость {key} не найдена.");
    }
}
