namespace SpaceBattle.Lib;

public class AuthCommand : ICommand
{
    private readonly IAuthContext _context;

    public AuthCommand(IAuthContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Execute()
    {
        bool isValid = (bool)IoC.Resolve<bool>("Auth.ValidateToken",
            _context.Token, _context.GameId, _context.PlayerId);

        if (!isValid)
        {
            throw new Exception("Authorization failed: Invalid token or access denied.");
        }
    }
}
