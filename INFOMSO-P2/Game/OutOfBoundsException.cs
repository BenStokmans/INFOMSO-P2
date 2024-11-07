namespace INFOMSO_P2.Game;

public class OutOfBoundsException(int x, int y) : MapException(x, y)
{
    public override string ToString() => $"OutOfBounds: ({X}, {Y})";
}