namespace INFOMSO_P2.Commands;

public class FileProgramParser : IProgramParser
{
    public Program Parse(string source)
    {
        string file = File.ReadAllText(source);

        // replace 4 spaces with tab
        file = file.Replace("    ", "\t");

        var commands = new List<ICommand>();
        string[] blocks = CommandParser.GetBlocks(file);

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

    public string UserPrompt() => "Enter the path to the file to parse: ";

}