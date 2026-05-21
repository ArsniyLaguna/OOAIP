using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public static class IoC
{
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    public static void Register(string key, Func<object[], object> strategy)
    {
        if (string.IsNullOrEmpty(key)) throw new ArgumentException("Ключ не может быть пустым.", nameof(key));
        _strategies[key] = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public static T Resolve<T>(string key, params object[] args)
    {
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return (T)strategy(args);
        }
        throw new KeyNotFoundException($"Зависимость '{key}' не зарегистрирована в IoC-контейнере.");
    }
}
