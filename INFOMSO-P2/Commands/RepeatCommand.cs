using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class RepeatCommand : ICommand
{
    public int Times;
    public readonly List<ICommand> Commands = [];

    public RepeatCommand() {}
    public RepeatCommand(int times, List<ICommand> commands)
    {
        Times = times;
        Commands = commands;
    }

    public void Parse(string command)
    {
        string[] lines = command.Split('\n');
        if (lines.Length < 2)
            throw new CommandException("Invalid repeat command");

        string[] parts = lines[0].Split(' ');
        if (parts is not ["Repeat", _, "times"] || !int.TryParse(parts[1], out Times))
            throw new CommandException("Invalid repeat command");

        // remove first line and remove one tab from each line
        lines = lines[1..].Select(line => line[1..]).ToArray();
        string commandString = string.Join('\n', lines);

        // separate blocks
        string[] blocks = CommandParser.GetBlocks(commandString);
        foreach (string block in blocks)
        {
            ICommand? cmd = CommandParser.ParseCommand(block);
            if (cmd is null)
                throw new CommandException("Invalid command in repeat block");
            Commands.Add(cmd);
        }
    }

    public void Execute(Scene scene)
    {
        for (var i = 0; i < Times; i++)
            foreach (ICommand cmd in Commands)
                cmd.Execute(scene);
    }
}