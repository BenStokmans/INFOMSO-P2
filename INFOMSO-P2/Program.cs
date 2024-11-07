using INFOMSO_P2.Commands;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

var mapParsers = new Dictionary<string, IMapParser>
{
    { "file", new FileMapParser() },
};

Console.WriteLine("Select map type: ");
for (var i = 0; i < mapParsers.Count; i++)
    Console.WriteLine($"{i + 1}. {mapParsers.ElementAt(i).Key}");

int mapParserIndex = -1;
while (mapParserIndex < 0 || mapParserIndex >= mapParsers.Count)
{
    Console.Write("Select map type: ");
    if (!int.TryParse(Console.ReadLine(), out mapParserIndex))
    {
        mapParserIndex = -1;
        Console.WriteLine("Invalid input");
    }
    else
        mapParserIndex--;
}

IMapParser selectedMapParser = mapParsers.ElementAt(mapParserIndex).Value;

Console.Write(selectedMapParser.UserPrompt());
string? mapSource = Console.ReadLine();

var map = selectedMapParser.Parse(mapSource);

var scene = new Scene(map);

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
    new RepeatMetricCalculator()
];

Console.WriteLine("Metrics:");
foreach (IMetricsCalculator metric in metrics)
    Console.WriteLine(metric.CalculateMetrics(program));