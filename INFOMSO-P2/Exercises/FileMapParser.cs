namespace INFOMSO_P2.Exercises;


public class FileMapParser : IMapParser
{
    public MapElement[,] Parse(string source)
    {
        string[] lines = source.Split('\n');
        lines = lines.Select(line => line.Trim()).ToArray();

        int width = lines[0].Length;
        var map = new MapElement[width, lines.Length];
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < width; x++)
            {
                map[x, y] = lines[y][x] switch
                {
                    '+' => MapElement.Blocked,
                    'O' => MapElement.Open,
                    'o' => MapElement.Open,
                    's' => MapElement.Start,
                    'S' => MapElement.Start,
                    'X' => MapElement.EndState,
                    'x' => MapElement.EndState,
                    _ => MapElement.Open
                };
            }
        }

        return map;
    }
}