namespace INFOMSO_P2.Exercises;

public class HardCodedMapParser : IMapParser
{
    public MapElement[,] Parse(string source)
    {
        return source.ToLower() switch
        {
            "pathfinding" => CreatePathfindingMap(),
            "shape" => CreateShapeMap(),
            _ => throw new ArgumentOutOfRangeException(nameof(source), "Invalid map index")
        };
    }

    private static MapElement[,] CreatePathfindingMap()
    {
        var pathfindingMap = new MapElement[5, 5];

        // diagonal path from (0,0) to (4,4)
        for (var x = 0; x < 5; x++)
        for (var y = 0; y < 5; y++)
        {
            pathfindingMap[x, y] = MapElement.Blocked;
        }

        for (var x = 0; x < 5; x++)
        for (var y = 0; y < 5; y++)
        {
            if (x != y) continue;

            pathfindingMap[x, y] = MapElement.Open;
            if (x != 4)
            {
                pathfindingMap[x + 1, y] = MapElement.Open;
            }
        }

        pathfindingMap[4, 4] = MapElement.EndState;


        return pathfindingMap;
    }

    private static MapElement[,] CreateShapeMap()
    {
        var shapeMap = new MapElement[5, 5];
        // all corners are blocked
        shapeMap[0, 0] = MapElement.Blocked;
        shapeMap[0, 4] = MapElement.Blocked;
        shapeMap[4, 0] = MapElement.Blocked;
        shapeMap[4, 4] = MapElement.Blocked;

        // center cross is blocked
        shapeMap[1, 2] = MapElement.Blocked;
        shapeMap[2, 1] = MapElement.Blocked;
        shapeMap[2, 2] = MapElement.Blocked;
        shapeMap[2, 3] = MapElement.Blocked;
        shapeMap[3, 2] = MapElement.Blocked;

        // start position is (1,0)
        shapeMap[1, 0] = MapElement.Start;

        return shapeMap;
    }
}