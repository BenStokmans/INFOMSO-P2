using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class GridEdgeCondition : ICondition
{
    
    //current implementation checks if on gridedge
    //maybe we want to check if ahead pos out bounds
    public bool Holds(Scene scene)
    {
        Vector2 outerCorner = new Vector2(scene.Map.GetLength(0), scene.Map.GetLength(1));
        Vector2 pos = scene.GetCharacter().Position;
        return (pos.X == 0 || pos.X == outerCorner.X || pos.Y == 0 || pos.Y == outerCorner.Y);
    }
}