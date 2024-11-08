namespace INFOMSO_P3.Game;

public readonly struct Vector2(int x, int y)
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
    public Vector2 Rotate270() => new(-Y, X);


#pragma warning disable CA2211
    public static Vector2 Zero = new(0, 0);
    public static Vector2 Down = new(0, -1);
    public static Vector2 Right = new(1, 0);
#pragma warning restore CA2211

    private bool Equals(Vector2 other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";
}