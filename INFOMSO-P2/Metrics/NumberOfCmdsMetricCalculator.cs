using INFOMSO_P2.Commands;

namespace INFOMSO_P2.Metrics;

public class NumberOfCmdsMetricCalculator : IMetricsCalculator
{
    public string CalculateMetrics(Commands.Program program)
    {
        var numberOfCommands = 0;

        foreach (ICommand cmd in program.Commands)
        {
            numberOfCommands++;
            if (cmd is not RepeatUntilCommand repeatCmd) continue;
            numberOfCommands += CountNumberOfCommands(repeatCmd) - 1;
        }

        return $"Number of commands: {numberOfCommands}";
    }

    private static int CountNumberOfCommands(RepeatUntilCommand repeatCommand)
    {
        var count = 1;
        foreach (ICommand cmd in repeatCommand.Commands)
        {
            count++;
            if (cmd is not RepeatUntilCommand repeatCmd) continue;

            count += CountNumberOfCommands(repeatCmd) - 1;
        }
        return count;
    }
}