namespace INFOMSO_P2.Commands;

public class FileProgramParser : IProgramParser
{
    public Program Parse(string source)
    {
        string file = File.ReadAllText(source);
        var commands = new List<ICommand>();
        string[] lines = file.Split("\n");

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
        blocks.RemoveAll(string.IsNullOrEmpty);

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