namespace INFOMSO_P3.Commands;

public interface IProgramParser
{
    public Program Parse(string source);
    public string SourceCode(string source);
}