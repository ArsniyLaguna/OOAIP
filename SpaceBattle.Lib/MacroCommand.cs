namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private readonly List<ICommand> _commands;
    public MacroCommand(IEnumerable<ICommand> commands) => _commands = commands.ToList();
    public void Execute() => _commands.ForEach(c => c.Execute());
}
