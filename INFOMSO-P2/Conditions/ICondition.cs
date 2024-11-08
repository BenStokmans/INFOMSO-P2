using INFOMSO_P2.Game;
namespace INFOMSO_P2.Conditions;

public interface ICondition
{
    bool Holds(Scene scene);

    string ToString();
}