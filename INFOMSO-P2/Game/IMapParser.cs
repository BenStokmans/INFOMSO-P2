namespace INFOMSO_P2.Game;

public interface IMapParser
{
    public MapElement[,] Parse(string source);
    public string UserPrompt();
}