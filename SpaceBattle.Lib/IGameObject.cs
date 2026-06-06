namespace SpaceBattle.Lib;

public interface IGameObject
{
    int Id { get; }
}

public interface IMovable : IGameObject
{
    (int X, int Y) Position { get; set; }
    (int X, int Y) Velocity { get; }
    void Update();
}
