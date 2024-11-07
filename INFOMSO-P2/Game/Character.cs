namespace INFOMSO_P2.Game;

public class Character : Entity
{
    public List<Vector2> Path = [];

    public override void Move(int distance, Scene scene)
    {
        var newPos = Position + Direction * distance;
        if (newPos.X < 0 || newPos.X >= scene.Width || newPos.Y < 0 || newPos.Y >= scene.Height)
            throw new OutOfBoundsException(newPos.X, newPos.Y);
        if (scene.GetMapElement(newPos) == MapElement.Blocked)
            throw new BlockedException(newPos.X, newPos.Y);

        Position = newPos;
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