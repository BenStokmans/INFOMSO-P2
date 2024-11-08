using INFOMSO_P3.Game;

namespace INFOMSO_P3.Commands;

public abstract class EntityCommand : Command
{
    public override void Execute(Scene scene)
    {
        // the default entity is the character
        Character character = scene.GetCharacter();
        Execute(character, scene);
    }

    protected abstract void Execute(Entity entity, Scene scene);

    public abstract override string ToString();
}