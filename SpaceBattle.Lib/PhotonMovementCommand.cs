namespace SpaceBattle.Lib;

public class PhotonMovementCommand : ICommand
{
    private readonly IMovable _movable;

    public PhotonMovementCommand(Photon photon)
    public PhotonMovementCommand(IMovable movable)
    {
        _movable = movable ?? throw new ArgumentNullException(nameof(movable));
    }

    public void Execute()
    {
        _movable.Update();
    }
}
