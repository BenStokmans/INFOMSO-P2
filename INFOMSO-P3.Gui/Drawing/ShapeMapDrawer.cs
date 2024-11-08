using Avalonia.Media;
using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Gui.Drawing;

public class ShapeMapDrawer : ExerciseMapDrawer
{
    protected override SolidColorBrush GetCellBrush(Scene scene, int x, int y)
    {
        return scene.Map[x, y] switch
        {
            MapElement.Blocked => new SolidColorBrush(Colors.White),
            _ => new SolidColorBrush(Colors.Coral)
        };

    }
}