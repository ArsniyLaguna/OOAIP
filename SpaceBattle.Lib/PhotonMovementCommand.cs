namespace SpaceBattle.Lib;

public class PhotonMovementCommand : ICommand
{
    private readonly Photon _photon;

    /// <summary>
    /// Создать команду движения фотона
    /// </summary>
    /// <param name="photon">Фотон для перемещения</param>
    public PhotonMovementCommand(Photon photon)
    {
        _photon = photon ?? throw new ArgumentNullException(nameof(photon));
    }

    public void Execute()
    {
        _photon.Update();
    }
}
