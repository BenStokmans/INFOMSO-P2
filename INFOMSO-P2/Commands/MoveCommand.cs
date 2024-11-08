using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class MoveCommand : EntityCommand
{
    public int Distance;

    public MoveCommand() => Distance = 0;
    public MoveCommand(int distance) => Distance = distance;

    protected override void Parse(string command)
    {
        string[] parts = command.Split(' ');
        if (parts is ["Move", _] && int.TryParse(parts[1], out Distance))
            return;
        throw new CommandException(Line, "Invalid move command: not in the form 'Move <distance>'");
    }

    protected override void Execute(Entity entity, Scene scene)
    {
        try
        {
            entity.Move(Distance, scene);
        }
        catch (Exception e)
        {
            throw new CommandException(Line, e.Message);
        }
    }
    
    public override string ToString() => $"Move {Distance}";
}