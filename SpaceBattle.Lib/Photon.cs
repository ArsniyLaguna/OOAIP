namespace SpaceBattle.Lib;

public class Photon : IGameObject
{
    private readonly (int X, int Y) _direction;
    private readonly int _speed;

    public int Id { get; }
    public (int X, int Y) Position { get; set; }

    public Photon(int id, (int X, int Y) position, (int X, int Y) direction, int speed = 1)
    {
        if (speed <= 0)
            throw new ArgumentException("Speed must be greater than 0", nameof(speed));
        
        Id = id;
        Position = position;
        _direction = direction;
        _speed = speed;
    }

    public void Update()
    {
        Position = (Position.X + _direction.X * _speed, Position.Y + _direction.Y * _speed);
    }

    public (int X, int Y) GetDirection() => _direction;
}
