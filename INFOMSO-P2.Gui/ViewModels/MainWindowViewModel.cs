using System.Collections.Generic;
using System.ComponentModel;

namespace INFOMSO_P2.Gui.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private ProgramControlViewModel? _vm = null;


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