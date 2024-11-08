using INFOMSO_P2.Game;

namespace INFOMSO_P2.Exercises;

public abstract class Exercise(string source, IMapParser mapParser)
{
    public MapElement[,] Map { get; set; } = mapParser.Parse(source);

    public abstract bool IsCompleted(Scene scene);
}