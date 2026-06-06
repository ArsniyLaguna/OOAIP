namespace SpaceBattle.Lib;

public interface IMovable : IGameObject
{
    (int X, int Y) Velocity { get; }
    void Update();
}
