namespace INFOMSO_P2.Game;

public class Character : Entity
{
    public List<Vector2> Path;

    public override void Move(int distance)
    {
        Position += Direction * distance;
        Path.Add(Position);
    }

    public override string ToString()
    {
        string? directionString = Direction switch
        {
            { X: 1, Y: 0 } => "east",
            { X: -1, Y: 0 } => "west",
            { X: 0, Y: -1 } => "south",
            { X: 0, Y: 1 } => "north",
            _ => throw new ArgumentOutOfRangeException()
        };
        return $"{Position} facing {directionString}";
    }
}