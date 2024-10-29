using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public interface ICommand
{
    void Parse(string command);
    void Execute(Scene scene);
    string ToString();
}