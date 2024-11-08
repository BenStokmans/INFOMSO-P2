using INFOMSO_P3.Exercises;

namespace INFOMSO_P3.Game;

public class Scene
{
    public readonly List<Entity> Entities = [];
    public readonly MapElement[,] Map;
    public int Width => Map.GetLength(0);
    public int Height => Map.GetLength(1);

    public Vector2? GoalPosition;

    public Scene(MapElement[,] map)
    {
        Map = map;
        var character = new Character();

        for (var x = 0; x < map.GetLength(0); x++)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] is MapElement.EndState)
                    GoalPosition = new Vector2(x, y);
                if (map[x, y] is not MapElement.Start) continue;

                character.Position = new Vector2(x, y);
                GoalPosition = new Vector2(x, y);
            }
        }
        if (map[character.Position.X, character.Position.Y] is MapElement.Blocked)
            throw new ArgumentException("Character is placed on a blocked tile.");

        character.Path.Add(character.Position);
        Entities.Add(character);
    }

    public Character GetCharacter() => (Entities.First(e => e is Character) as Character)!;

    public MapElement GetMapElement(Vector2 v)
    {
        if (v.X < 0 || v.X >= Width || v.Y < 0 || v.Y >= Height)
            throw new OutOfBoundsException(v.X, v.Y);

        return Map[v.X, v.Y];
    }
    
}