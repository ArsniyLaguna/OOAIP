namespace SpaceBattle.Lib;
public class Photon : IGameObject
{
    private readonly int _speed;
    private (int X, int Y) _direction;

    public int Id { get; }
    public (int X, int Y) Position { get; set; }

    /// <summary>
    /// Создать фотонную торпеду
    /// </summary>
    /// <param name="id">Идентификатор торпеды</param>
    /// <param name="position">Начальная позиция</param>
    /// <param name="direction">Направление движения</param>
    /// <param name="speed">Скорость движения</param>
    public Photon(int id, (int X, int Y) position, (int X, int Y) direction, int speed = 1)
    {
        if (speed <= 0)
            throw new ArgumentException("Speed must be positive", nameof(speed));

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
    public int GetSpeed() => _speed;
}
