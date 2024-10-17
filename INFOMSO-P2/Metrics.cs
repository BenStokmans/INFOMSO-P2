namespace INFOMSO_P2;

public struct Metrics(int maximumNestingDepth, int numberOfRepeatCommands)
{
    public int MaximumNestingDepth { get; set; } = maximumNestingDepth;
    public int NumberOfRepeatCommands { get; set; } = numberOfRepeatCommands;

    public override string ToString()
    {
        return $"Maximum nesting depth: {MaximumNestingDepth}, Number of repeat commands: {NumberOfRepeatCommands}";
    }
}