namespace INFOMSO_P2.Game;

public struct Vector2(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator *(Vector2 a, int b) => new(a.X * b, a.Y * b);
    public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
    public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vector2 a, Vector2 b) => a.X != b.X || a.Y != b.Y;

    public Vector2 Rotate90() => new(Y, -X);
    public Vector2 Rotate180() => new(-X, -Y);
    public Vector2 Rotate270() => new(-Y, X);


    public static Vector2 Zero = new(0, 0);
    public static Vector2 One = new(1, 1);
    public static Vector2 Up = new(0, 1);
    public static Vector2 Down = new(0, -1);
    public static Vector2 Left = new(-1, 0);
    public static Vector2 Right = new(1, 0);

    private static readonly Dictionary<string, Vector2>  NamedVectors = new()
    {
        ["zero"] = Zero,
        ["one"] = One,
        ["up"] = Up,
        ["down"] = Down,
        ["left"] = Left,
        ["right"] = Right
    };

    public static bool TryParse(string value, out Vector2 result)
    {
        if (NamedVectors.TryGetValue(value.ToLower(), out result))
            return true;
        string[] parts = value.Split(',');
        if (parts.Length != 2 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
            return false;
        result = new Vector2(x, y);
        return true;
    }

    public override string ToString() => $"({X}, {Y})";
}