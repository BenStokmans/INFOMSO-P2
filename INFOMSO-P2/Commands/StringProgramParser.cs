namespace INFOMSO_P2.Commands;

public class StringProgramParser : IProgramParser
{
    public Program Parse(string source)
    {
        // replace 4 spaces with tab
        source = source.Replace("    ", "\t");

        var commands = new List<ICommand>();
        string[] blocks = CommandParser.GetBlocks(source);

        foreach (string block in blocks)
        {
            ICommand? cmd = CommandParser.ParseCommand(block);
            if (cmd is null)
                throw new CommandException("Invalid command in program");
            commands.Add(cmd);
        }

        // parse file
        return new Program(commands);
    }

    public string SourceCode(string source) => File.ReadAllText(source);

    public string UserPrompt() => "Type out source code for program: ";
}