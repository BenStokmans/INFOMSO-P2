using INFOMSO_P2.Game;
namespace INFOMSO_P2.Commands;

public interface ICondition
{
    bool Holds(Scene scene);

    void Parse(string condition);
    string ToString();
}