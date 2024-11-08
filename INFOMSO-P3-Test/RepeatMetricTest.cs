using INFOMSO_P3.Commands;
using INFOMSO_P3.Metrics;

namespace INFOMSO_P3_Test;

public class RepeatMetricTest
{
    [Test]
    public void RepeatMetricTestNested()
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

        var calculator = new RepeatMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Number of repeat commands: 3"));
    }

    [Test]
    public void RepeatMetricTestNotNested()
    {
        var program = new Program([
            new RepeatCommand(4, [
                new MoveCommand(1)
            ]),

            new RepeatCommand(3, [
                new MoveCommand(1)
            ]),

            new RepeatCommand(2, [
                new MoveCommand(1)
            ])

        ]);

        var calculator = new RepeatMetricCalculator();
        string result = calculator.CalculateMetrics(program);

        Assert.That(result, Is.EqualTo("Number of repeat commands: 3"));
    }
}