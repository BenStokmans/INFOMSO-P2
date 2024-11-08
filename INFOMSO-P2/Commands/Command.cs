using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public abstract class Command : ICommand
{
    public int Line { get; private set; }

    public void Parse(int line, string command)
    {
        Line = line;
        Parse(command);
    }

    public abstract void Parse(string command);
    public abstract void Execute(Scene scene);
    public abstract override string ToString();
}