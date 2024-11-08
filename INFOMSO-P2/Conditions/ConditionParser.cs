namespace INFOMSO_P2.Conditions;

public static class ConditionParser
{
    public static ICondition? Parse(string condition)
    {
        return condition switch
        {
            "GridEdge" => new GridEdgeCondition(),
            "WallAhead" => new WallAheadCondition(),
            "ReachedGoal" => new ReachedGoalCondition(),
            _ => null
        };
    }
}