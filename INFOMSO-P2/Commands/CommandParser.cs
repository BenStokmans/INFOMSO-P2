namespace INFOMSO_P2.Commands;

public static class CommandParser
{
    public static Command ParseCommand(int line, string command)
    {
        string commandName = command.Split(' ')[0];

        Command? cmd = commandName switch
        {
            "Move" => new MoveCommand(),
            "Turn" => new TurnCommand(),
            "Repeat" => new RepeatCommand(),
            "RepeatUntil" => new RepeatUntilCommand(),
            _ => null
        };

        if (cmd == null)
            throw new CommandException(line, $"Invalid command name: {commandName}");

        cmd.Parse(line, command.TrimEnd());
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
        blocks.RemoveAll(block => block is "\n" or "");

        return blocks.ToArray();
    }
}