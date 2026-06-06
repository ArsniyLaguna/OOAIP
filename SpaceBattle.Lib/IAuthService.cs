namespace SpaceBattle.Lib;

public interface IAuthService
{
    bool CheckAccess(IPlayer player, int gameObjectId);
}
