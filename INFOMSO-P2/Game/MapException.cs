namespace INFOMSO_P2.Game;

public abstract class MapException(int x, int y) : Exception
{
    public int X = x;
    public int Y = y;

    public abstract override string ToString();
}