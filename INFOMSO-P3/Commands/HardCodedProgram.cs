namespace INFOMSO_P3.Commands;

public class HardCodedProgramParser : IProgramParser
{
    private readonly Dictionary<string, string> _programs = new()
    {
        { "Basic-Shape", "Move 2\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 2\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 2\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 2\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1" },
        { "Advanced-Shape", "Repeat 4 times\r\n\tRepeatUntil WallAhead\r\n\t\tMove 1\r\n\tTurn right\r\n\tMove 1\r\n\tTurn left\r\n\tMove 1\r\n\tTurn right\r\n" },
        { "Expert-Shape", "RepeatUntil ReachedGoal\r\n\tRepeatUntil WallAhead\r\n\t\tMove 1\r\n\tTurn right\r\n\tMove 1\r\n\tTurn left\r\n\tMove 1\r\n\tTurn right\r\n" },
        { "Basic-Pathfinding", "Move 1\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 1\r\nTurn left\r\nMove 1\r\nTurn right\r\nMove 1\r\nTurn left\r\n" },
        { "Advanced-Pathfinding", "Repeat 4 times\r\n\tMove 1\r\n\tTurn right\r\n\tMove 1\r\n\tTurn left\r\n" },
        { "Expert-Pathfinding", "RepeatUntil ReachedGoal\r\n\tMove 1\r\n\tTurn right\r\n\tMove 1\r\n\tTurn left\r\n" }
    };

    public Program Parse(string source)
    {
        string name = source.Trim();
        if (int.TryParse(source, out int index))
        {
            if (index < 1 || index > _programs.Count)
                throw new ArgumentOutOfRangeException("Invalid program index: " + index);
            name = _programs.ElementAt(index - 1).Key;
        }

        if (!_programs.TryGetValue(name, out string? value))
            throw new ArgumentOutOfRangeException("Invalid program name: " + name);

        var parser = new StringProgramParser();
        return parser.Parse(value);
    }

    public string SourceCode(string source)
    {
        string name = source.Trim();
        if (int.TryParse(source, out int index))
        {
            if (index < 1 || index > _programs.Count)
                throw new ArgumentOutOfRangeException("Invalid program index: " + index);
            name = _programs.ElementAt(index - 1).Key;
        }

        if (!_programs.TryGetValue(name, out string? value))
            throw new ArgumentOutOfRangeException("Invalid program name: " + name);

        return value;
    }
}