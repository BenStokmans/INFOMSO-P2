using INFOMSO_P3.Exercises;

namespace INFOMSO_P3.Game;

public class Character : Entity
{
    public readonly List<Vector2> Path = [];

    public override void Move(int distance, Scene scene)
    {
        Vector2 newPos = Position + Direction * distance;
        // check if there is a wall on the path to the new position
        for (var i = 1; i <= distance; i++)
        {
            Vector2 pos = Position + Direction * i;
            if (scene.GetMapElement(pos) == MapElement.Blocked)
                throw new BlockedException(pos.X, pos.Y);
        }

        for (var i = 1; i <= distance; i++)
        {
            Path.Add(Position + Direction * i);
        }
        Position = newPos;
    }

    public override string ToString()
    {
        string directionString = Direction switch
        {
            { X: 1, Y: 0 } => "east",
            { X: -1, Y: 0 } => "west",
            { X: 0, Y: -1 } => "north",
            { X: 0, Y: 1 } => "south",
            _ => throw new ArgumentOutOfRangeException()
        };

        return $"{Position} facing {directionString}";
    }
}