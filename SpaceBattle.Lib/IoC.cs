using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib
{
    private static readonly Dictionary<string, Func<object[], object>> _registry = new();

    public static void Register(string key, Func<object[], object> strategy)
    {
        _registry[key] = strategy;
    }

    public static T Resolve<T>(string key, params object[] args)
    {
        if (_registry.TryGetValue(key, out var strategy))
        {
            return (T)strategy(args);
        }
        throw new Exception($"Зависимость '{key}' не найдена.");
    private static readonly Dictionary<string, IStrategy> _strategies = new();
    public static T Resolve<T>(string key, params object[] args)
    public static class IoC
    {
        private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

        public static T Resolve<T>(string key, params object[] args)
        {
            if (_strategies.TryGetValue(key, out var strategy))
            {
                return (T)strategy(args);
            }
            throw new Exception($"Зависимость '{key}' не найдена.");
        }

        public static void Register(string key, Func<object[], object> strategy)
        {
            _strategies[key] = strategy;
        }

        public static void Reset() => _strategies.Clear();
    }
}
