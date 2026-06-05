namespace SpaceBattle.Lib;

public interface IAuthService
{
    bool CheckAccess(string playerToken, int gameObjectId);
}
