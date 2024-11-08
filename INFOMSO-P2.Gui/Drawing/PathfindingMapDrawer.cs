using System;
using Avalonia;
using Avalonia.Media;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Gui.Drawing;

public class PathfindingMapDrawer : DefaultMapDrawer
{
    public override void DrawMap(DrawingContext context, Scene scene, Size cellSize)
    {
        base.DrawMap(context, scene, cellSize);

        const double goalSizePercentage = 0.6; // percentage of cell size
        double minSize = Math.Min(cellSize.Width, cellSize.Height);
        Vector2 goalPosition = scene.GoalPosition!.Value;

        var goalBrush = new SolidColorBrush(Colors.Green);
        double goalSize = minSize * goalSizePercentage;
        var goalRect = new Rect(goalPosition.X * cellSize.Width + (cellSize.Width - goalSize) / 2,
            goalPosition.Y * cellSize.Height + (cellSize.Height - goalSize) / 2, goalSize, goalSize);
        DrawCross(context, goalRect.Center, goalSize, new Pen(goalBrush, 4));
    }

    private static void DrawCross(DrawingContext context, Point center, double size, Pen pen)
    {
        double halfSize = size / 2;
        var p1 = new Point(center.X - halfSize, center.Y - halfSize);
        var p2 = new Point(center.X + halfSize, center.Y + halfSize);
        var p3 = new Point(center.X - halfSize, center.Y + halfSize);
        var p4 = new Point(center.X + halfSize, center.Y - halfSize);
        context.DrawLine(pen, p1, p2);
        context.DrawLine(pen, p3, p4);
    }
}