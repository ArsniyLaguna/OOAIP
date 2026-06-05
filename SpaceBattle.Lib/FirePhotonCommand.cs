namespace SpaceBattle.Lib;

public class FirePhotonCommand : ICommand
{
    private readonly Spaceship _spaceship;
    private readonly (int X, int Y) _direction;
    private readonly IGameObjectRepository _repository;

    /// <param name="spaceship">Корабль, который стреляет</param>
    /// <param name="direction">Направление выстрела</param>
    /// <param name="repository">Репозиторий для добавления торпеды</param>
    public FirePhotonCommand(Spaceship spaceship, (int X, int Y) direction, IGameObjectRepository repository)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction;
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Execute()
    {
        var photon = _spaceship.FirePhoton(_direction);

        _repository.Add(photon);
    }
}
