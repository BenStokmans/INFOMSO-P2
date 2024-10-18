namespace INFOMSO_P2.Metrics;

public interface IMetricsCalculator
{
    public string CalculateMetrics(Commands.Program program);
}