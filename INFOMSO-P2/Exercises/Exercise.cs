using INFOMSO_P2.Game;

namespace INFOMSO_P2.Exercises;

public abstract class Exercise(string source, IMapParser mapParser)
{
    public MapElement[,] Map { get; } = mapParser.Parse(source);

    public abstract bool IsCompleted(Scene scene);
}