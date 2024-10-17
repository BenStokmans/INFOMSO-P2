using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class MoveCommand : EntityCommand
{
    private int _distance;

    public MoveCommand() => _distance = 0;
    public MoveCommand(int distance) => _distance = distance;

    public override void Parse(string command)
    {
        string[] parts = command.Split(' ');
        if (parts is ["Move", _] && int.TryParse(parts[1], out _distance))
            return;
        throw new CommandException("Invalid move command");
    }

    protected override void Execute(Entity entity) => entity.Move(_distance);
}