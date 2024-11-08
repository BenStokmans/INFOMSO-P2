namespace INFOMSO_P3.Commands;

public class StringProgramParser : IProgramParser
{
    public Program Parse(string source)
    {
        // replace 4 spaces with tab
        source = source.Replace("    ", "\t");

        var commands = new List<Command>();
        string[] blocks = CommandParser.GetBlocks(source);

        var line = 1;
        foreach (string block in blocks)
        {
            Command? cmd = CommandParser.ParseCommand(line, block);
            if (cmd is null)
                throw new CommandException(line, "Invalid command in program");
            commands.Add(cmd);
            line += block.Split('\n').Length - 1;
        }

        // parse file
        return new Program(commands);
    }

    public virtual string SourceCode(string source) => source;
}