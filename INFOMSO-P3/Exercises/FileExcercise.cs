using INFOMSO_P3.Game;

namespace INFOMSO_P3.Exercises;

public class FileExcercise(string source) : Exercise(source, new FileMapParser())
{
    public override bool IsCompleted(Scene scene)
    {
        Character character = scene.GetCharacter();
        return
            scene.GetMapElement(character.Position)
            == MapElement.EndState || (scene.GetMapElement(character.Position) == MapElement.Start && character.Path.Count > 1);
    }
}