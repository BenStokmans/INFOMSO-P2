using INFOMSO_P3.Game;

namespace INFOMSO_P3.Conditions;

public class ExecutionCounterCondition(int executionCount) : ICondition
{
    private int _currentExecutionCount;

    public bool Holds(Scene scene)
    {
        return executionCount == _currentExecutionCount++;
    }
    
    public new string ToString() => "ExecutionCounterCondition";
}