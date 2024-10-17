using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class TurnCommand : EntityCommand
{
    private int _degrees;

    public TurnCommand() => _degrees = 0;
    public TurnCommand(int degrees) => _degrees = degrees;

    public override void Parse(string command)
    {
        // turn right or left
        _degrees = command.Split(' ')[1] switch
        {
            "right" => 90,
            "left" => 270,
            _ => throw new CommandException("Invalid direction")
        };
    }

    protected override void Execute(Entity entity) =>
        entity.Direction = _degrees == 90 ? entity.Direction.Rotate90() : entity.Direction.Rotate270();
}