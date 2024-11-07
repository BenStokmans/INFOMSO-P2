using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.ComponentModel;
using INFOMSO_P2.Commands;

namespace INFOMSO_P2.Gui.ViewModels;

public class ProgramControlViewModel: INotifyPropertyChanged
{
    public Program Program { get; } = new();

    public void AddCommand(CommandType type)
    {

    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}