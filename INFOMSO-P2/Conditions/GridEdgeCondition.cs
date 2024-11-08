using INFOMSO_P2.Game;
namespace INFOMSO_P2.Conditions;

public class GridEdgeCondition : ICondition
{
    
    //current implementation checks if on gridedge
    //maybe we want to check if ahead pos out bounds
    public bool Holds(Scene scene)
    {
        Vector2 pos = scene.GetCharacter().Position;
        return pos.X == 0 || pos.X == scene.Width - 1 || pos.Y == 0 || pos.Y == scene.Height - 1;
    }

    public new string ToString() => "GridEdge";
}