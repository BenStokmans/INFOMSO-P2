using System.Text;

namespace INFOMSO_P2.Commands;

public class HardCodedProgramParser : IProgramParser
{
    private readonly Dictionary<string, Program> _programs = new()
    {
        { "Rectangle", new Program([
            new MoveCommand(10),
            new TurnCommand(90),
            new MoveCommand(10),
            new TurnCommand(90),
            new MoveCommand(10),
            new TurnCommand(90),
            new MoveCommand(10),
            new TurnCommand(90)
        ]) },
        { "Straight Line", new Program([
            new MoveCommand(10),
            new MoveCommand(10),
            new MoveCommand(10),
            new MoveCommand(10)
        ]) },
    };

    public Program Parse(string source)
    {
        if (!int.TryParse(source, out int index))
            throw new CommandException("Invalid program index");

        if (index < 1 || index > _programs.Count)
            throw new CommandException("Invalid program index");

        return _programs.ElementAt(index - 1).Value;
    }

    public string UserPrompt()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Available built-in programs:");
        for (var i = 0; i < _programs.Count; i++)
            sb.AppendLine($"{i + 1}. {_programs.ElementAt(i).Key}");

        sb.Append("Select a program: ");
        return sb.ToString();
    }
}