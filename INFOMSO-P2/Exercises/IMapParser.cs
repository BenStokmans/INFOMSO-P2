namespace INFOMSO_P2.Exercises;

public interface IMapParser
{
    public MapElement[,] Parse(string source);
}