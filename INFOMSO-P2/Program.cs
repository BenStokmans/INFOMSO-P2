using INFOMSO_P2.Commands;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

var scene = new Scene();

scene.Entities.Add(new Character());

var parsers = new Dictionary<string, IProgramParser>
{
    { "file", new FileProgramParser() },
    { "hardcoded", new HardCodedProgramParser() }
};

Console.WriteLine("Select program type: ");
for (var i = 0; i < parsers.Count; i++)
    Console.WriteLine($"{i + 1}. {parsers.ElementAt(i).Key}");

int parserIndex = -1;
while (parserIndex < 0 || parserIndex >= parsers.Count)
{
    Console.Write("Select program type: ");
    if (!int.TryParse(Console.ReadLine(), out parserIndex))
    {
        parserIndex = -1;
        Console.WriteLine("Invalid input");
    }
    else
        parserIndex--;
}

IProgramParser selectedParser = parsers.ElementAt(parserIndex).Value;

Console.Write(selectedParser.UserPrompt());
string? source = Console.ReadLine();

INFOMSO_P2.Commands.Program program = selectedParser.Parse(source);
program.Run(scene);

Console.WriteLine($"End state {scene.GetCharacter()}");

List<IMetricsCalculator> metrics = new()
{
    new DepthMetricCalculator(),
    new RepeatMetricCalculator()
};

Console.WriteLine("Metrics:");
foreach (IMetricsCalculator metric in metrics)
    Console.WriteLine(metric.CalculateMetrics(program));