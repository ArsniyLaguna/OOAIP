namespace SpaceBattle.Lib;

public class FirePhotonCommand : ICommand
{
    private readonly Spaceship _spaceship;
    private readonly (int X, int Y) _direction;

    public FirePhotonCommand(
        Spaceship spaceship, 
        (int X, int Y) direction)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction;
    private readonly Vector _direction;
    private readonly IGameObjectRepository _repository;

    public FirePhotonCommand(Spaceship spaceship, Vector direction, IGameObjectRepository repository)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction ?? throw new ArgumentNullException(nameof(direction));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Execute()
    {
        var photon = _spaceship.FirePhoton(_direction);
        _repository.Add(photon);
    }
}
