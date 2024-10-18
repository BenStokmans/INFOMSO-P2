using INFOMSO_P2.Commands;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2_Test;

public class DepthMetricTest
{
    [Test]
    public void DepthMetricTestNested()
    {
        var program = new Program([
            new RepeatCommand(4, [
                new RepeatCommand(3, [
                    new RepeatCommand(2, [
                        new MoveCommand(1),
                        new MoveCommand(1)
                    ]),
                    new MoveCommand(1)
                ]),
                new MoveCommand(1)
            ])
        ]);

        var calculator = new DepthMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Maximum nesting depth: 3"));
    }

    [Test]
    public void DepthMetricTestNotNested()
    {
        var program = new Program([
            new MoveCommand(1),
            new MoveCommand(1),
            new MoveCommand(1)
        ]);

        var calculator = new DepthMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Maximum nesting depth: 0"));
    }
}