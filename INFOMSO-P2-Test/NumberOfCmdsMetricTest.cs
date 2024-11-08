using INFOMSO_P2.Commands;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2_Test;

public class NumberOfCmdsMetricTest
{
    [Test]
    public void NumberOfCmdsMetricNoNestingTest()
    {
        var program = new Program([
            new MoveCommand(1),
            new MoveCommand(1),
            new MoveCommand(1)
        ]);

        var calculator = new NumberOfCmdsMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Number of commands: 3"));
    }

    [Test]
    public void NumberOfCmdsMetricWithNestingTest()
    {
        var program = new Program([
            new RepeatCommand(2, [
                new MoveCommand(1),
                new MoveCommand(1)
            ]),
            new MoveCommand(1)
        ]);

        var calculator = new NumberOfCmdsMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Number of commands: 4"));
    }

}