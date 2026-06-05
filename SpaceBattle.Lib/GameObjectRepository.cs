namespace SpaceBattle.Lib;

public class GameObjectRepository : IGameObjectRepository
{
    private readonly Dictionary<int, IGameObject> _objects = new();

    public void Add(IGameObject gameObject)
    {
        if (gameObject == null)
            throw new ArgumentNullException(nameof(gameObject));

        _objects[gameObject.Id] = gameObject;
    }

    public IGameObject? Get(int id)
    {
        _objects.TryGetValue(id, out var obj);
        return obj;
    }

    public void Remove(int id)
    {
        _objects.Remove(id);
    }

    public IEnumerable<IGameObject> GetAll()
    {
        return _objects.Values.ToList();
    }
}
