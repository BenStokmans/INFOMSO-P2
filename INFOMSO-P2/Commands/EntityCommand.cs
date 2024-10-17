using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public abstract class EntityCommand : ICommand
{
    public abstract void Parse(string command);

    public void Execute(Scene scene)
    {
        // the default entity is the character
        Character? character = scene.GetCharacter();
        if (character != null)
            Execute(character);
    }

    protected abstract void Execute(Entity entity);
}