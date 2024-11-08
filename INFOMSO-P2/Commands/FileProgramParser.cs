namespace INFOMSO_P2.Commands;

public class FileProgramParser : StringProgramParser
{
    public new Program Parse(string source)
    {
        string file = File.ReadAllText(source);
        return base.Parse(file);
    }

    public override string SourceCode(string source) => File.ReadAllText(source);
}