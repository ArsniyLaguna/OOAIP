namespace SpaceBattle.Lib;

public class Spaceship : IMovable
{
    private int _nextPhotonId = 1;
    private Vector _velocity;

    public int Id { get; }
    public Vector Position { get; set; }
    public Vector Velocity => _velocity;

    public Spaceship(int id, Vector position)
    {
        Id = id;
        Position = position;
        _velocity = new Vector(0, 0);
    }

    public void SetVelocity(Vector velocity)
    {
    }

    public IGameObject FirePhoton((int X, int Y) direction)
        _velocity = velocity;
    }

    public void Update()
    {
        Position = new Vector(Position.X + _velocity.X, Position.Y + _velocity.Y);
    }

    public Photon FirePhoton(Vector direction)
    {
        return new Photon(_nextPhotonId++, Position, direction);
    }
}
