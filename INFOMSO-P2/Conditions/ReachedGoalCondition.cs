using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;

namespace INFOMSO_P2.Conditions;

public class ReachedGoalCondition : ICondition
{
    public bool Holds(Scene scene)
    {
        Character? character = scene.GetCharacter();
        return 
            scene.GetMapElement(character.Position)
                == MapElement.EndState || (scene.GetMapElement(character.Position) == MapElement.Start && character.Path.Count > 1);
    }
    
    public new string ToString() => "ReachedGoal";
}