namespace INFOMSO_P2.Game;

public class Scene
{
    public readonly List<Entity> Entities = [];
    public Character? GetCharacter() => Entities.FirstOrDefault(e => e is Character) as Character;
}