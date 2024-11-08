using INFOMSO_P2.Commands;
using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

var availableExercises = new Dictionary<string, Exercise>
{
    { "shape", new ShapeExercise() },
    { "pathfinding", new PathfindingExercise() },
};

Console.WriteLine("Select exercise: ");
for (var i = 0; i < availableExercises.Count; i++)
    Console.WriteLine($"{i + 1}. {availableExercises.ElementAt(i).Key}");

int exerciseIndex = -1;
while (exerciseIndex < 0 || exerciseIndex >= availableExercises.Count)
{
    Console.Write("Select exercise: ");
    if (!int.TryParse(Console.ReadLine(), out exerciseIndex))
    {
        exerciseIndex = -1;
        Console.WriteLine("Invalid input");
    }
    else
        exerciseIndex--;
}

Exercise selectedExercise = availableExercises.ElementAt(exerciseIndex).Value;

var scene = new Scene(selectedExercise.Map);

scene.Entities.Add(new Character());

var programParsers = new Dictionary<string, IProgramParser>
{
    { "file", new FileProgramParser() },
    { "hardcoded", new HardCodedProgramParser() }
};

Console.WriteLine("Select program type: ");
for (var i = 0; i < programParsers.Count; i++)
    Console.WriteLine($"{i + 1}. {programParsers.ElementAt(i).Key}");

int programParserIndex = -1;
while (programParserIndex < 0 || programParserIndex >= programParsers.Count)
{
    Console.Write("Select program type: ");
    if (!int.TryParse(Console.ReadLine(), out programParserIndex))
    {
        programParserIndex = -1;
        Console.WriteLine("Invalid input");
    }
    else
        programParserIndex--;
}

IProgramParser selectedProgramParser = programParsers.ElementAt(programParserIndex).Value;

Console.Write(selectedProgramParser.UserPrompt());
string? programSource = Console.ReadLine();

INFOMSO_P2.Commands.Program program = selectedProgramParser.Parse(programSource);
program.Run(scene);

Console.WriteLine($"End state {scene.GetCharacter()}");

List<IMetricsCalculator> metrics =
[
    new DepthMetricCalculator(),
    new RepeatMetricCalculator(),
    new NumberOfCmdsMetricCalculator(),
];

Console.WriteLine("Metrics:");
foreach (IMetricsCalculator metric in metrics)
    Console.WriteLine(metric.CalculateMetrics(program));