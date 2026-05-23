namespace SpaceBattle.Lib;

public static class IoC
{
    private readonly static Dictionary<string, Func<object[], object>> _strategies = new();

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