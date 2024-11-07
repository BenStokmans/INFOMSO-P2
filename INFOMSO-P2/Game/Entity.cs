namespace INFOMSO_P2.Game;

public abstract class Entity
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 Direction = Vector2.Right;

    public abstract void Move(int distance);
    public Vector2 GetAheadPosition() => Position + Direction;
}