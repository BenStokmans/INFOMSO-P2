using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public class WallAheadCondition : ICondition
{
    public bool Holds(Scene scene)
    {
        return true; //TODO eerst walls en 2d array voor scene maken
    }

    public void Parse(String condition) //word al gedaan in de parser miss gwn alleen return??
    {
        if (condition == "WallAhead") return;
        throw new ConditionException("invalid wallAhead condition");
    }
    
    
    public new string ToString() => "WallAhead";
}