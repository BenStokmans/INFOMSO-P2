using INFOMSO_P2.Commands;

namespace INFOMSO_P2;

public static class MetricsCalculator
{
    public static Metrics CalculateMetrics(Commands.Program program)
    {
        var maxDepth = 0;
        var repeatCommands = 0;

        foreach (ICommand cmd in program.Commands)
        {
            if (cmd is not RepeatCommand repeatCmd) continue;
            int depth = CalculateMaxDepth(repeatCmd);
            if (depth > maxDepth)
                maxDepth = depth;
            repeatCommands += CalculateNumberOfRepeatCommands(repeatCmd);
        }

        return new Metrics(maxDepth, repeatCommands);
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

    private static int CalculateNumberOfRepeatCommands(RepeatCommand repeatCommand)
    {
        var count = 1;
        foreach (ICommand cmd in repeatCommand.Commands)
        {
            if (cmd is not RepeatCommand repeatCmd) continue;
            count += CalculateNumberOfRepeatCommands(repeatCmd);
        }
        return count;
    }
}