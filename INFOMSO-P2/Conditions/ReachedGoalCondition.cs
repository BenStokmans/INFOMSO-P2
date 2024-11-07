using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class ReachedGoalCondition : ICondition
{
    public bool Holds(Scene scene)
    {
        return 
            scene.GetMapElement(scene.GetCharacter().Position) 
                == MapElement.EndState;
    }
    
    public new string ToString() => "ReachedGoal";
}