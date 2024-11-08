using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;

namespace INFOMSO_P2.Conditions;

public class ExecutionCounterCondition(int executionCount) : ICondition
{
    public readonly int ExecutionCount = executionCount;
    private int _currentExecutionCount;

    public bool Holds(Scene scene)
    {
        return ExecutionCount == _currentExecutionCount++;
    }
    
    public new string ToString() => "ExecutionCounterCondition";
}