using Avalonia.Media;
using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Gui.Drawing;

public class DefaultMapDrawer : ExerciseMapDrawer
{
    protected override SolidColorBrush GetCellBrush(Scene scene, int x, int y)
    {
        return scene.Map[x, y] switch
        {
            MapElement.Open => new SolidColorBrush(Colors.White),
            MapElement.Blocked => new SolidColorBrush(Colors.Coral),
            _ => new SolidColorBrush(Colors.White)
        };
    }
}