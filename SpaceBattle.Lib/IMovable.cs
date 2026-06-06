using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public interface IMovable : IGameObject
{
    Vector Position { get; set; }
    Vector Velocity { get; }
    void Update();
}
