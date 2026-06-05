namespace SpaceBattle.Lib;

/// <summary>
/// Космический корабль
/// </summary>
public class Spaceship : IGameObject
{
    private int _nextPhotonId = 1;

    public int Id { get; }
    public (int X, int Y) Position { get; set; }

    public Spaceship(int id, (int X, int Y) position)
    {
        Id = id;
        Position = position;
    }

    public void Update()
    {
        // Корабль может выполнять другие действия
    }

    /// <summary>
    /// Создать фотонную торпеду
    /// </summary>
    public Photon FirePhoton((int X, int Y) direction)
    {
        return new Photon(_nextPhotonId++, Position, direction);
    }
}
