using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;
using INFOMSO_P2.Gui.ViewModels;

namespace INFOMSO_P2.Gui;

public class ProgramCanvas : Control
{

    public Scene? Scene => (DataContext as MainWindowViewModel)?.Scene;
    public Exercise? Exercise => (DataContext as MainWindowViewModel)?.Exercise;

    public static readonly StyledProperty<Color> BackgroundColorProperty =
        AvaloniaProperty.Register<ProgramCanvas, Color>(nameof(BackgroundColor), defaultValue: Colors.White);

    public Color BackgroundColor
    {
        get => GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        var rect = new Rect(Bounds.Size);
        context.FillRectangle(new SolidColorBrush(BackgroundColor), rect);

        if (Scene is null) return;

        // draw grid
        var gridBrush = new SolidColorBrush(Colors.LightGray);
        var gridPen = new Pen(gridBrush, 4);

        // cell size
        var cellSize = new Size(rect.Width / Scene.Width, rect.Height / Scene.Height);

        // draw cells
        for (var x = 0; x < Scene.Width; x++)
        {
            for (var y = 0; y < Scene.Height; y++)
            {
                MapElement mapElement = Scene.GetMapElement(new Vector2(x, y));
                var cellRect = new Rect(x * cellSize.Width, y * cellSize.Height, cellSize.Width, cellSize.Height);
                SolidColorBrush cellBrush = GetCellBrush(Exercise, x, y);
                context.FillRectangle(cellBrush, cellRect);
            }
        }

        // draw path
        Character? character = Scene.GetCharacter();

        var path = character.Path;
        path.Add(character.Position);

        var pathPen = new Pen(new SolidColorBrush(Colors.Red), 4);
        for (var i = 0; i < character.Path.Count - 1; i++)
        {
            Vector2 p1 = character.Path[i];
            Vector2 p2 = character.Path[i + 1];
            var p1Point = new Point(p1.X * cellSize.Width + cellSize.Width / 2, p1.Y * cellSize.Height + cellSize.Height / 2);
            var p2Point = new Point(p2.X * cellSize.Width + cellSize.Width / 2, p2.Y * cellSize.Height + cellSize.Height / 2);
            context.DrawLine(pathPen, p1Point, p2Point);
        }

        const double characterSizePercentage = 0.6; // percentage of cell size
        double minSize = Math.Min(cellSize.Width, cellSize.Height);
        double characterSize = minSize * characterSizePercentage;

        if (Exercise is PathfindingExercise)
        {
            Vector2 goalPosition = Scene.GoalPosition!.Value;
            var goalBrush = new SolidColorBrush(Colors.Green);
            double goalSize = minSize * characterSizePercentage;
            var goalRect = new Rect(goalPosition.X * cellSize.Width + (cellSize.Width - goalSize) / 2,
                goalPosition.Y * cellSize.Height + (cellSize.Height - goalSize) / 2, goalSize, goalSize);
            DrawCross(context, goalRect.Center, goalSize, new Pen(goalBrush, 4));
        }

        // draw character as blue circle
        var characterBrush = new SolidColorBrush(Colors.Blue);
        var characterRect = new Rect(character.Position.X * cellSize.Width + (cellSize.Width - characterSize) / 2,
            character.Position.Y * cellSize.Height + (cellSize.Height - characterSize) / 2, characterSize, characterSize);
        context.DrawEllipse(characterBrush, new Pen(characterBrush, 4), characterRect.Center, characterSize / 2, characterSize / 2);

        // draw character direction arrow
        var directionPen = new Pen(new SolidColorBrush(Colors.Black), 4);
        Point directionStart = characterRect.Center;
        const double arrowSizePercentage = 0.4;
        var directionEnd = new Point(directionStart.X + character.Direction.X * cellSize.Width * arrowSizePercentage,
            directionStart.Y + character.Direction.Y * cellSize.Height * arrowSizePercentage);
        DrawArrow(context, directionStart, directionEnd, directionPen);


        // draw grid lines
        for (var i = 0; i <= Scene.Width; i++)
        {
            double x = i * cellSize.Width;
            var p1 = new Point(x, 0);
            var p2 = new Point(x, rect.Height);
            context.DrawLine(gridPen, p1, p2);
        }

        for (var i = 0; i <= Scene.Height; i++)
        {
            double y = i * cellSize.Height;
            var p1 = new Point(0, y);
            var p2 = new Point(rect.Width, y);
            context.DrawLine(gridPen, p1, p2);
        }
    }

    private SolidColorBrush GetCellBrush(Exercise exercise, int x, int y)
    {
        if (exercise is PathfindingExercise)
        {
            return exercise.Map[x, y] switch
            {
                MapElement.Open => new SolidColorBrush(Colors.White),
                MapElement.Blocked => new SolidColorBrush(Colors.Coral),
                _ => new SolidColorBrush(Colors.White)
            };
        }

        return exercise.Map[x, y] switch
        {
            MapElement.Blocked => new SolidColorBrush(Colors.White),
            _ => new SolidColorBrush(Colors.Coral)
        };
    }

    private void DrawCross(DrawingContext context, Point center, double size, Pen pen)
    {
        double halfSize = size / 2;
        var p1 = new Point(center.X - halfSize, center.Y - halfSize);
        var p2 = new Point(center.X + halfSize, center.Y + halfSize);
        var p3 = new Point(center.X - halfSize, center.Y + halfSize);
        var p4 = new Point(center.X + halfSize, center.Y - halfSize);
        context.DrawLine(pen, p1, p2);
        context.DrawLine(pen, p3, p4);
    }

    private void DrawArrow(DrawingContext context, Point start, Point end, Pen pen)
    {
        var arrowSize = 10;
        double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
        var arrowEnd1 = new Point(end.X - arrowSize * Math.Cos(angle - Math.PI / 6), end.Y - arrowSize * Math.Sin(angle - Math.PI / 6));
        var arrowEnd2 = new Point(end.X - arrowSize * Math.Cos(angle + Math.PI / 6), end.Y - arrowSize * Math.Sin(angle + Math.PI / 6));
        context.DrawLine(pen, start, end);
        context.DrawLine(pen, end, arrowEnd1);
        context.DrawLine(pen, end, arrowEnd2);
    }
}