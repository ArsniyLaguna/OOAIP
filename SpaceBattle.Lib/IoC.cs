using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib
{ // Уровень 1: Namespace
    public static class IoC
    { // Уровень 2: Класс
        private static readonly Dictionary<string, Func<object[], object>> _registry = new();

        public static void Register(string key, Func<object[], object> strategy)
        { // Уровень 3: Методы
            _registry[key] = strategy;
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            if (_registry.TryGetValue(key, out var strategy))
            {
                return (T)strategy(args);
            }
            throw new Exception($"Зависимость '{key}' не найдена.");
        }

        public static void Reset() => _registry.Clear();
    } // Закрытие уровня 2 (Класс)
} // Закрытие уровня 1 (Namespace)
