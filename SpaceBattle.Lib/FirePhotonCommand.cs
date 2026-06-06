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
    }

    public void Execute()
    {
        var photon = _spaceship.FirePhoton(_direction);
    }
}
