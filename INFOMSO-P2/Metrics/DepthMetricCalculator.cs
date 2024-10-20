using INFOMSO_P2.Commands;

namespace INFOMSO_P2.Metrics;

public class DepthMetricCalculator : IMetricsCalculator
{
    public string CalculateMetrics(Commands.Program program)
    {
        var maxDepth = 0;

        foreach (ICommand cmd in program.Commands)
        {
            if (cmd is not RepeatCommand repeatCmd) continue;
            int depth = CalculateMaxDepth(repeatCmd);
            if (depth > maxDepth)
                maxDepth = depth;
        }

        return $"Maximum nesting depth: {maxDepth}";
    }

    private static int CalculateMaxDepth(RepeatCommand repeatCommand)
    {
        var maxDepth = 0;
        foreach (ICommand cmd in repeatCommand.Commands)
        {
            if (cmd is not RepeatCommand repeatCmd) continue;
            int depth = CalculateMaxDepth(repeatCmd);
            if (depth > maxDepth)
                maxDepth = depth;
        }
        return maxDepth + 1;
    }
}