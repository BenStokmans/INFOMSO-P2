namespace INFOMSO_P2.Game;

public class Scene
{
    public readonly List<Entity> Entities = [];
    public MapElement[,] Map;
    public Character? GetCharacter() => Entities.FirstOrDefault(e => e is Character) as Character;
    public MapElement GetMapElement(Vector2 v) => Map[v.X, v.Y];
    
}