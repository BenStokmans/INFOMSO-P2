using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3.Gui.ViewModels;

public class MainWindowViewModel
{
    public Scene Scene { get; private set; } = null!;
    public Exercise Exercise { get; set; }

    public MainWindowViewModel()
    {
        Exercise = new ShapeExercise();
        ResetScene();
    }

    public void ResetScene()
    {
        Scene = new Scene(Exercise.Map);
    }
}