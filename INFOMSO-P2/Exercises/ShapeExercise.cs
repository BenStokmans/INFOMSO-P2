using INFOMSO_P3.Game;

namespace INFOMSO_P3.Exercises;

public class ShapeExercise() : Exercise("shape", new HardCodedMapParser())
{
    public override bool IsCompleted(Scene scene)
    {
        Character character = scene.GetCharacter();
        var path = character.Path;

        // check if every open tile is visited
        for (var i = 0; i < Map.GetLength(0); i++)
        for (var j = 0; j < Map.GetLength(1); j++)
            if (Map[i, j] == MapElement.Open && !path.Contains(new Vector2(i, j)))
                return false;

        return true;
    }
}