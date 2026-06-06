namespace SpaceBattle.Lib;

public class Spaceship : IGameObject
{
    private int _nextPhotonId = 1;
    private (int X, int Y) _velocity;

    public int Id { get; }
    public (int X, int Y) Position { get; set; }

    public Spaceship(int id, (int X, int Y) position)
    {
        Id = id;
        Position = position;
        _velocity = (0, 0);
    }

    public void SetVelocity((int X, int Y) velocity)
    {
        _velocity = velocity;
    }

    public void Update()
    {
        Position = (Position.X + _velocity.X, Position.Y + _velocity.Y);
    }

    public Photon FirePhoton((int X, int Y) direction)
    {
        return new Photon(_nextPhotonId++, Position, direction);
    }
}
