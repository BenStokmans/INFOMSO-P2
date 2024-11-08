using INFOMSO_P2.Exercises;
using INFOMSO_P2.Game;

namespace INFOMSO_P2.Gui.ViewModels;

public class MainWindowViewModel
{
    public Scene Scene { get; private set; }
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