using INFOMSO_P3.Game;

namespace INFOMSO_P3.Commands;

public class Program(List<Command> commands)
{
    public readonly List<Command> Commands = commands;

    public void Run(Scene scene)
    {
        foreach (Command cmd in Commands)
            cmd.Execute(scene);
    }
}