namespace INFOMSO_P3.Game;

public class BlockedException(int x, int y) : Exception($"Blocked: ({x}, {y})");