namespace INFOMSO_P2.Commands;

public class FileProgramParser : StringProgramParser
{
    public new Program Parse(string source)
    {
        string file = File.ReadAllText(source);
        return base.Parse(file);
    }

    public string SourceCode(string source) => File.ReadAllText(source);

    public string UserPrompt() => "Enter the path to the file to parse: ";
}