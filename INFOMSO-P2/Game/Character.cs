namespace INFOMSO_P2.Game;

public class Character : Entity
{
    public override void Move(int distance) => Position += Direction * distance;

    public override string ToString()
    {
        string? directionString = Direction switch
        {
            { X: 1, Y: 0 } => "east",
            { X: -1, Y: 0 } => "west",
            { X: 0, Y: 1 } => "south",
            { X: 0, Y: -1 } => "north",
        };
        return $"{Position} facing {directionString}";
    }
}