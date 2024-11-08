using INFOMSO_P3.Game;

namespace INFOMSO_P3.Exercises;

public class PathfindingExercise() : Exercise("pathfinding", new HardCodedMapParser())
{
    public override bool IsCompleted(Scene scene)
    {
        // check if character has reached the end state
        return scene.GetMapElement(scene.GetCharacter().Position) == MapElement.EndState;
    }
}