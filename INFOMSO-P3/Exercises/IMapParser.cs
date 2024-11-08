namespace INFOMSO_P3.Exercises;

public interface IMapParser
{
    public MapElement[,] Parse(string source);
}