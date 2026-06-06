namespace SpaceBattle.Lib;

public static class IoC
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
    {
        if (key == "IoC.Register")
        {
            _strategies[(string)args[0]] = (IStrategy)args[1];
            return default!;
        }
        if (_strategies.TryGetValue(key, out var strategy)) return (T)strategy.Invoke(args);
        throw new Exception($"Зависимость {key} не найдена.");
    }
}
