namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    void Add(IGameObject gameObject);

    IGameObject? Get(int id);

    void Remove(int id);

    IEnumerable<IGameObject> GetAll();
}
