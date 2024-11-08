using INFOMSO_P2.Commands;
using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2_Test;

public class ProgramTest
{

    [Test]
    public void ProgramFileParsingTest()
    {
        const string program = "Repeat 4 times\n\tRepeat 3 times\n\t\tRepeat 2 times\n\t\t\tMove 1\n\t\t\tMove 1\n\t\tMove 1\n\tMove 1";
        // write program to file
        File.WriteAllText("program.txt", program);

        var parser = new FileProgramParser();
        Program parsedProgram = parser.Parse("program.txt");

        Assert.Multiple(() =>
        {
            Assert.That(parsedProgram.Commands.Count, Is.EqualTo(1));
            Assert.That(parsedProgram.Commands[0], Is.TypeOf<RepeatCommand>());
            Assert.That(((RepeatCommand)parsedProgram.Commands[0]).Times, Is.EqualTo(4));
            Assert.That(((RepeatCommand)parsedProgram.Commands[0]).Commands, Has.Count.EqualTo(2));
            Assert.That(((RepeatCommand)parsedProgram.Commands[0]).Commands[0], Is.TypeOf<RepeatCommand>());

        });

        var depthCalculator = new DepthMetricCalculator();
        string depthResult = depthCalculator.CalculateMetrics(parsedProgram);

        Assert.That(depthResult, Is.EqualTo("Maximum nesting depth: 3"));

        var repeatCalculator = new RepeatMetricCalculator();
        string repeatResult = repeatCalculator.CalculateMetrics(parsedProgram);

        Assert.That(repeatResult, Is.EqualTo("Number of repeat commands: 3"));

        File.Delete("program.txt");
    }

    [Test]
    public void ProgramFileParsingTestInvalid()
    {
        // attempt to load non-existing file
        var parser = new FileProgramParser();
        Assert.Throws<FileNotFoundException>(() => parser.Parse("non-existing.txt"));

        // write invalid program to file
        const string program = "Repeat 4\n\tRepeat 3 times\n\t\tRepeat 2 times\n\t\t\tMove 1\n\t\t\tMove 1\n\t\tMove 1\n\tMove 1";
        // write invalid program to file
        File.WriteAllText("program.txt", program);
        Assert.Throws<CommandException>(() => parser.Parse("program.txt"));


        File.Delete("program.txt");
    }

    [Test]
    public void ProgramRunTest()
    {
        var map = new MapElement[65, 1];
        for (var x = 0; x < 65; x++)
        {
            map[x, 0] = x < 64 ? MapElement.Open : MapElement.EndState;
        }


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

        var scene = new Scene(map);
        scene.Entities.Add(new Character());
        program.Run(scene);

        Assert.That(scene.GetCharacter().Position, Is.EqualTo(new Vector2(64, 0)));
    }
}