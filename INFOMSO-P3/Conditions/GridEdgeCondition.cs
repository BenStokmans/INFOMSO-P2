using INFOMSO_P3.Game;

namespace INFOMSO_P3.Conditions;

public class GridEdgeCondition : ICondition
{
    public bool Holds(Scene scene)
    {
        Vector2 pos = scene.GetCharacter().Position;
        pos += scene.GetCharacter().Direction;
        return pos.X < 0 || pos.X >= scene.Width || pos.Y < 0 || pos.Y >= scene.Height;
    }

    public new string ToString() => "GridEdge";
}