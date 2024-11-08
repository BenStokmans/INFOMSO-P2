using INFOMSO_P2.Conditions;
using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class RepeatUntilCommand : Command
{
    public ICondition Condition;
    public readonly List<Command> Commands = [];

    public RepeatUntilCommand() { }

    public RepeatUntilCommand(ICondition condition, List<Command> commands)
    {
        Condition = condition;
        Commands = commands;
    }

    public override void Execute(Scene scene)
    {
        while (!Condition.Holds(scene))
        {
            foreach(Command cmd in Commands) cmd.Execute(scene);
        }
    }

    protected virtual void ParseCondition(string condition)
    {
        string[] parts = condition.Split(' ');
        if (parts.Length != 2)
            throw new CommandException(Line, "Invalid repeat until command: no condition found");
        Condition = ConditionParser.Parse(parts[1]);
        if (Condition is null)
            throw new CommandException(Line, "Invalid condition in repeat until command: " + parts[1]);
    }

    public override void Parse(string command)
    {
        string[] lines = command.Split('\n');
        if (lines.Length < 2)
            throw new CommandException(Line, "Invalid repeat command: no commands found in block");

        lines = lines.Select(line => line.TrimEnd()).ToArray();

        ParseCondition(lines[0]);
        
        // remove first line and remove one tab from each line
        lines = lines[1..].Select(line => line[1..]).ToArray();
        string commandString = string.Join('\n', lines);

        int line = Line + 1;

        // separate blocks
        string[] blocks = CommandParser.GetBlocks(commandString);
        foreach (string block in blocks)
        {
            Command? cmd = CommandParser.ParseCommand(line, block);
            if (cmd is null)
                throw new CommandException(line, "Invalid command in repeat block");
            Commands.Add(cmd);
            line += block.Split('\n').Length - 1;
        }
    }
    
    public override string ToString() => $"RepeatUntil {Condition.ToString()}";
}