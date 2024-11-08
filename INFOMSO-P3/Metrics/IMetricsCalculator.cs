namespace INFOMSO_P3.Metrics;

public interface IMetricsCalculator
{
    public string CalculateMetrics(Commands.Program program);
}