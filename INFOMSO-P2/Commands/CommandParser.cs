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

    public static string[] GetBlocks(string command)
    {
        string[] lines = command.Split('\n');
        var blocks = new List<string>();

        var currentBlock = "";
        foreach (string line in lines)
        {
            if (line.StartsWith('\t'))
                currentBlock += line + "\n";
            else
            {
                blocks.Add(currentBlock);
                currentBlock = line + "\n";
            }
        }
        blocks.Add(currentBlock);
        blocks.RemoveAll(string.IsNullOrEmpty);

        return blocks.ToArray();
    }
}