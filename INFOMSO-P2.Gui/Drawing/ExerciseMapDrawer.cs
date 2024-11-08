using Avalonia;
using Avalonia.Media;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Gui.Drawing;

public abstract class ExerciseMapDrawer
{
    public virtual void DrawMap(DrawingContext context, Scene scene, Size cellSize)
    {
        for (var x = 0; x < scene.Width; x++)
        {
            for (var y = 0; y < scene.Height; y++)
            {
                var cellRect = new Rect(x * cellSize.Width, y * cellSize.Height, cellSize.Width, cellSize.Height);
                SolidColorBrush cellBrush = GetCellBrush(scene, x, y);
                context.FillRectangle(cellBrush, cellRect);
            }
        }
    }


    protected abstract SolidColorBrush GetCellBrush(Scene scene, int x, int y);
}