using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class TurnCommand : EntityCommand
{
    public int Degrees;

    public TurnCommand() => Degrees = 0;
    public TurnCommand(int degrees) => Degrees = degrees;

    public override void Parse(string command)
    {
        var parts = command.Split(' ');
        if (parts.Length != 2)
            throw new CommandException(Line, "Invalid turn command: not in the form 'Turn <direction>'");

        var direction = parts[1].ToLower();
        // turn right or left
        Degrees = direction switch
        {
            "right" => 270,
            "left" => 90,
            _ => throw new CommandException(Line, "Invalid direction in turn command: " + direction)
        };
    }

    protected override void Execute(Entity entity, Scene scene) =>
        entity.Direction = Degrees == 90 ? entity.Direction.Rotate90() : entity.Direction.Rotate270();
    
    public override string ToString() => $"TurnCommand {Degrees}";
}