using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Conditions;

public class WallAheadCondition : ICondition
{
    public bool Holds(Scene scene)
    {
        return
            scene.GetMapElement(scene.GetCharacter().GetAheadPosition()) == MapElement.Blocked;
    }
    
    public new string ToString() => "WallAhead";
}