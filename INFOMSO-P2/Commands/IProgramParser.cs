namespace INFOMSO_P2.Commands;

public interface IProgramParser
{
    public Program Parse(string source);
    public string UserPrompt();
}