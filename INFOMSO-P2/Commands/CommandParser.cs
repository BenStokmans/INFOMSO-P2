namespace INFOMSO_P2.Commands;

public static class CommandParser
{
    public static ICommand? ParseCommand(string command)
    {
        string commandName = command.Split(' ')[0];

        ICommand? cmd = commandName switch
        {
            "Move" => new MoveCommand(),
            "Turn" => new TurnCommand(),
            "Repeat" => new RepeatCommand(),
            _ => null
        };

        if (cmd == null)
            throw new CommandException("Invalid command");

        cmd.Parse(command.TrimEnd('\n'));
        return cmd;
    }
}