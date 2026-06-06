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
    }

    public void Update()
    {
        Position = (Position.X + Velocity.X, Position.Y + Velocity.Y);
    }
}
