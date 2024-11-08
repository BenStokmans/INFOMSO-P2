namespace INFOMSO_P3.Game;

public class OutOfBoundsException(int x, int y) : Exception($"OutOfBounds: ({x}, {y})");