using INFOMSO_P2.Commands;

namespace INFOMSO_P2.Metrics;

public class RepeatMetricCalculator : IMetricsCalculator
{
    public string CalculateMetrics(Commands.Program program)
    {
        var repeatCommands = 0;

        foreach (ICommand cmd in program.Commands)
        {
            if (cmd is not RepeatCommand repeatCmd) continue;
            repeatCommands += CalculateNumberOfRepeatCommands(repeatCmd);
        }

        return $"Number of repeat commands: {repeatCommands}";
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