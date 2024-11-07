namespace INFOMSO_P2.Game;

public class BlockedException(int x, int y) : MapException(x, y)
{
    public override string ToString() => $"Blocked: ({X}, {Y})";
}