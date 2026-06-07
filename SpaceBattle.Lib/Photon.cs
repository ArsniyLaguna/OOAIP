public class Photon : IMovable
{
    public int Id { get; }
    public (int X, int Y) Position { get; set; }
    public (int X, int Y) Velocity { get; }

    public Photon(int id, (int X, int Y) position, (int X, int Y) direction, int speed = 1)
    {
        Id = id;
        Position = position;
        Velocity = (direction.X * speed, direction.Y * speed);
namespace SpaceBattle.Lib;

public class Photon : IMovable
{
    private Vector _velocity;

    public int Id { get; }
    public Vector Position { get; set; }
    public Vector Velocity => _velocity;

    public Photon(int id, Vector position, Vector direction, int speed = 1)
    {
        if (speed <= 0)
            throw new ArgumentException("Speed must be greater than 0", nameof(speed));
        
        Id = id;
        Position = position;
        _velocity = new Vector(direction.X * speed, direction.Y * speed);
    }

    public void Update()
    {
        Position = (Position.X + Velocity.X, Position.Y + Velocity.Y);
    }
        Position = new Vector(Position.X + _velocity.X, Position.Y + _velocity.Y);
    }

    public Vector GetDirection() => _velocity;
}
