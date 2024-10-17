using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class RepeatCommand : ICommand
{
    private int _times;
    public readonly List<ICommand> Commands = [];

    public void Parse(string command)
    {
        string[] parts = command.Split(' ');
        if (parts is not ["Repeat", _, "times"] || !int.TryParse(parts[1], out _times))
            throw new CommandException("Invalid repeat command");

        // split new lines
        string[] lines = command.Split('\n');
        foreach (string line in lines[1..])
        {
            // if we find a line that doesn't start with a tab, we stop parsing as it's the end of the repeat block
            if (!line.StartsWith('\t'))
                break;

            ICommand? cmd = CommandParser.ParseCommand(line[1..]);
            if (cmd is null)
                throw new CommandException("Invalid command in repeat block");
            Commands.Add(cmd);
        }
    }

    public void Execute(Scene scene)
    {
        for (var i = 0; i < _times; i++)
            foreach (ICommand cmd in Commands)
                cmd.Execute(scene);
    }
}