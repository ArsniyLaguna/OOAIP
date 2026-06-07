using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public interface IMovable : IGameObject
{
    Vector Velocity { get; }
    void Update();
}
