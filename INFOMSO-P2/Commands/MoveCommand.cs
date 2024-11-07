using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class MoveCommand : EntityCommand
{
    public int Distance;

    public MoveCommand() => Distance = 0;
    public MoveCommand(int distance) => Distance = distance;

    public override void Parse(string command)
    {
        string[] parts = command.Split(' ');
        if (parts is ["Move", _] && int.TryParse(parts[1], out Distance))
            return;
        throw new CommandException("Invalid move command");
    }

    protected override void Execute(Entity entity, Scene scene) => entity.Move(Distance, scene);
    
    public override string ToString() => $"Move {Distance}";
}