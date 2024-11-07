using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using INFOMSO_P2.Game;
using INFOMSO_P2.Gui.ViewModels;

namespace INFOMSO_P2.Gui;

public class ProgramCanvas : Control
{

    public Scene? Scene => (DataContext as MainWindowViewModel)?.Scene;

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
                var mapElement = Scene.GetMapElement(new Vector2(x, y));
                var cellRect = new Rect(x * cellSize.Width, y * cellSize.Height, cellSize.Width, cellSize.Height);
                var cellBrush = mapElement switch
                {
                    MapElement.Open => new SolidColorBrush(Colors.White),
                    MapElement.Blocked => new SolidColorBrush(Colors.Coral),
                    MapElement.EndState => new SolidColorBrush(Colors.Green),
                    _ => throw new ArgumentOutOfRangeException()
                };
                context.FillRectangle(cellBrush, cellRect);
            }
        }

        // draw path
        var character = Scene.GetCharacter();

        var path = character.Path;
        path.Add(character.Position);

        var pathPen = new Pen(new SolidColorBrush(Colors.Red), 4);
        for (var i = 0; i < character.Path.Count - 1; i++)
        {
            var p1 = character.Path[i];
            var p2 = character.Path[i + 1];
            var p1Point = new Point(p1.X * cellSize.Width + cellSize.Width / 2, p1.Y * cellSize.Height + cellSize.Height / 2);
            var p2Point = new Point(p2.X * cellSize.Width + cellSize.Width / 2, p2.Y * cellSize.Height + cellSize.Height / 2);
            context.DrawLine(pathPen, p1Point, p2Point);
        }

        // draw character as blue circle
        var characterBrush = new SolidColorBrush(Colors.Blue);
        const double characterSizePercentage = 0.6; // percentage of cell size
        var minSize = Math.Min(cellSize.Width, cellSize.Height);
        var characterSize = minSize * characterSizePercentage;
        var characterRect = new Rect(character.Position.X * cellSize.Width + (cellSize.Width - characterSize) / 2,
            character.Position.Y * cellSize.Height + (cellSize.Height - characterSize) / 2, characterSize, characterSize);
        context.DrawEllipse(characterBrush, new Pen(characterBrush, 4), characterRect.Center, characterSize / 2, characterSize / 2);

        // draw character direction arrow
        var directionPen = new Pen(new SolidColorBrush(Colors.Black), 4);
        var directionStart = characterRect.Center;
        const double arrowSizePercentage = 0.4;
        var directionEnd = new Point(directionStart.X + character.Direction.X * cellSize.Width * arrowSizePercentage,
            directionStart.Y + character.Direction.Y * cellSize.Height * arrowSizePercentage);
        DrawArrow(context, directionStart, directionEnd, directionPen);


        // draw grid lines
        for (var i = 0; i <= Scene.Width; i++)
        {
            var x = i * cellSize.Width;
            var p1 = new Point(x, 0);
            var p2 = new Point(x, rect.Height);
            context.DrawLine(gridPen, p1, p2);
        }

        for (var i = 0; i <= Scene.Height; i++)
        {
            var y = i * cellSize.Height;
            var p1 = new Point(0, y);
            var p2 = new Point(rect.Width, y);
            context.DrawLine(gridPen, p1, p2);
        }
    }

    private void DrawArrow(DrawingContext context, Point start, Point end, Pen pen)
    {
        var arrowSize = 10;
        var angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
        var arrowEnd1 = new Point(end.X - arrowSize * Math.Cos(angle - Math.PI / 6), end.Y - arrowSize * Math.Sin(angle - Math.PI / 6));
        var arrowEnd2 = new Point(end.X - arrowSize * Math.Cos(angle + Math.PI / 6), end.Y - arrowSize * Math.Sin(angle + Math.PI / 6));
        context.DrawLine(pen, start, end);
        context.DrawLine(pen, end, arrowEnd1);
        context.DrawLine(pen, end, arrowEnd2);
    }
}