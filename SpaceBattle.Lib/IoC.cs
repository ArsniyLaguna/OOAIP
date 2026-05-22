namespace SpaceBattle.Lib;

public static class IoC
{
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    public static T Resolve<T>(string key, params object[] args)
    {
        if (key == "IoC.Register")
        {
            var targetKey = (string)args[0];
            var strategy = (Func<object[], object>)args[1];
            _strategies[targetKey] = strategy;
            return default!;
        }

        if (_strategies.TryGetValue(key, out var currentStrategy))
        {
            return (T)currentStrategy(args);
        }

        throw new Exception($"Зависимость '{key}' не найдена.");
    }
}