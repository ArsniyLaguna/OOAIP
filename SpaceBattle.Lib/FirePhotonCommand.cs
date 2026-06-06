namespace SpaceBattle.Lib;

public class FirePhotonCommand : ICommand
{
    private readonly Spaceship _spaceship;
    private readonly (int X, int Y) _direction;
    private readonly IGameObjectRepository _repository;

    public FirePhotonCommand(Spaceship spaceship, (int X, int Y) direction, IGameObjectRepository repository)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction;
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Execute()
    {
        var direction = new Vector(_direction.X, _direction.Y);
        var photon = _spaceship.FirePhoton(direction);
        _repository.Add(photon);
    }
}
