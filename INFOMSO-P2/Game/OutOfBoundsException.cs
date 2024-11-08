namespace INFOMSO_P2.Game;

public class OutOfBoundsException(int x, int y) : Exception($"OutOfBounds: ({x}, {y})");