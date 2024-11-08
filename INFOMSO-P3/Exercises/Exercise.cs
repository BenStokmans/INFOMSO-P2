using INFOMSO_P3.Game;

namespace INFOMSO_P3.Exercises;

public abstract class Exercise(string source, IMapParser mapParser)
{
    public MapElement[,] Map { get; } = mapParser.Parse(source);

    public abstract bool IsCompleted(Scene scene);
}