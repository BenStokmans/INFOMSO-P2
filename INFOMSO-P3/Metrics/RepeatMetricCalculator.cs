using INFOMSO_P3.Commands;

namespace INFOMSO_P3.Metrics;

public class RepeatMetricCalculator : IMetricsCalculator
{
    public string CalculateMetrics(Program program)
    {
        var repeatCommands = 0;

        foreach (Command cmd in program.Commands)
        {
            if (cmd is not RepeatUntilCommand repeatCmd) continue;
            repeatCommands += CalculateNumberOfRepeatCommands(repeatCmd);
        }

        return $"Number of repeat commands: {repeatCommands}";
    }

    private static int CalculateNumberOfRepeatCommands(RepeatUntilCommand repeatCommand)
    {
        var count = 1;
        foreach (Command cmd in repeatCommand.Commands)
        {
            if (cmd is not RepeatUntilCommand repeatCmd) continue;
            count += CalculateNumberOfRepeatCommands(repeatCmd);
        }
        return count;
    }
}