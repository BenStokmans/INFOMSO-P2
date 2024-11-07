using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class RepeatUntilCommand : ICommand
{
    public ICondition Condition;
    public readonly List<ICommand> Commands = [];

    public RepeatUntilCommand() { }

    public RepeatUntilCommand(ICondition condition, List<ICommand> commands)
    {
        Condition = condition;
        Commands = commands;
    }

    public void Execute(Scene scene)
    {
        while (!Condition.Holds(scene))
        {
            foreach(ICommand cmd in Commands) cmd.Execute(scene);
        }
    }

    public void Parse(String command)
    {
        string[] lines = command.Split('\n');
        if (lines.Length < 2)
            throw new CommandException("Invalid repeat until command");

        lines = lines.Select(line => line.TrimEnd()).ToArray();

        string[] parts = lines[0].Split(' ');
        Condition = ConditionParser.Parse(parts[1]); //TODO check potential throw exception
        
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
    
    public new string ToString() => $"RepeatUntil {Condition.ToString()}";
}