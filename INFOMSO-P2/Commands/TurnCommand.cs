using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class TurnCommand : EntityCommand
{
    public int Degrees;

    public TurnCommand() => Degrees = 0;
    public TurnCommand(int degrees) => Degrees = degrees;

    public override void Parse(string command)
    {
        // turn right or left
        Degrees = command.Split(' ')[1] switch
        {
            "right" => 270,
            "left" => 90,
            _ => throw new CommandException("Invalid direction")
        };
    }

    protected override void Execute(Entity entity, Scene scene) =>
        entity.Direction = Degrees == 90 ? entity.Direction.Rotate90() : entity.Direction.Rotate270();
    
    public override string ToString() => $"TurnCommand {Degrees}";
}