using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class Program(List<ICommand> commands)
{
    public List<ICommand> Commands = commands;

    public void Run(Scene scene)
    {
        foreach (ICommand cmd in Commands)
            cmd.Execute(scene);
    }
}