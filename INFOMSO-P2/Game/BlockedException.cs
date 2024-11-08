namespace INFOMSO_P2.Game;

public class BlockedException(int x, int y) : Exception($"Blocked: ({x}, {y})");