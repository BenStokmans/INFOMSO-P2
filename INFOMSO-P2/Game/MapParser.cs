namespace INFOMSO_P2.Game;


//assumes valid map
//TODO: map verification
public static class MapParser
{
    public static MapElement?[,] Parse(string source)
    {
        string[] lines = source.Split('\n');
        foreach (string line in lines)
        {
            line.Trim();
        }

        int width = lines[0].Length;
        MapElement?[,] map = new MapElement?[lines.Length, width];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = lines[y][x] switch
                {
                    '+' => MapElement.Blocked,
                    'O' => MapElement.Open,
                    'o' => MapElement.Open,
                    'S' => MapElement.Start,
                    's' => MapElement.Start,
                    'X' => MapElement.EndState,
                    'x' => MapElement.EndState,
                    _ => null
                };
            }
        }

        return map;
    }
}