﻿namespace INFOMSO_P3.Game;

public abstract class Entity
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 Direction = Vector2.Right;

    public abstract void Move(int distance, Scene scene);
    public Vector2 GetAheadPosition() => Position + Direction;
}