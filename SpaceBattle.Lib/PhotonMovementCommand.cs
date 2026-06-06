namespace SpaceBattle.Lib;

public class PhotonMovementCommand : ICommand
{
    private readonly Photon _photon;

    public PhotonMovementCommand(Photon photon)
    {
        _photon = photon ?? throw new ArgumentNullException(nameof(photon));
    }

    public void Execute()
    {
        _photon.Update();
    }
}
