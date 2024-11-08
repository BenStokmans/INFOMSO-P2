using INFOMSO_P3.Game;

namespace INFOMSO_P3.Conditions;

public interface ICondition
{
    bool Holds(Scene scene);

    string ToString();
}