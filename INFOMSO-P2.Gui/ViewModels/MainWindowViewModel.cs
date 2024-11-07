using System.Collections.Generic;
using System.ComponentModel;
using INFOMSO_P2.Game;

namespace INFOMSO_P2.Gui.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public Scene Scene { get; private set; }
    private ProgramControlViewModel? _vm = null;

    public MainWindowViewModel()
    {
        ResetScene();
    }

    public void ResetScene()
    {
        Scene = new Scene(new[,] {
            {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.Open},
            {MapElement.Open, MapElement.Open, MapElement.Blocked, MapElement.Blocked},
            {MapElement.Open, MapElement.Open, MapElement.Open, MapElement.Blocked},
            {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.EndState},
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public ProgramControlViewModel? Vm {
        get => _vm;
        set
        {
            if (_vm == value) return;
            _vm = value;
        }
    }
}