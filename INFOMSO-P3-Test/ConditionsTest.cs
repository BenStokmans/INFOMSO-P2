using INFOMSO_P3.Conditions;
using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3_Test;

public class ConditionsTest
{

    private readonly MapElement[,] _map = {
        {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.Open},
        {MapElement.Blocked, MapElement.Open, MapElement.Blocked, MapElement.Blocked},
        {MapElement.Open, MapElement.Open, MapElement.Open, MapElement.Blocked},
        {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.EndState}
    };
    private Scene _scene;

    [SetUp]
    public void Setup()
    {
        _scene = new Scene(_map);
    }

    [Test]
    public void TestGridEdgeCondition()
    {
        var condition = new GridEdgeCondition();
        Assert.That(condition.Holds(_scene), Is.False);

        _scene.GetCharacter().Position = new Vector2(3, 0);
        Assert.That(condition.Holds(_scene), Is.True);
    }

    [Test]
    public void TestWallAheadCondition()
    {
        var condition = new WallAheadCondition();
        Assert.That(condition.Holds(_scene), Is.True);

        _scene.GetCharacter().Position = new Vector2(2, 0);
        Assert.That(condition.Holds(_scene), Is.False);
    }

    [Test]
    public void ReachedGoalCondition()
    {
        var condition = new ReachedGoalCondition();
        Assert.That(condition.Holds(_scene), Is.False);

        _scene.GetCharacter().Position = new Vector2(3, 3);
        Assert.That(condition.Holds(_scene), Is.True);
    }

    [Test]
    public void TestConditionParser()
    {
        var condition = ConditionParser.Parse("GridEdge");
        Assert.That(condition is GridEdgeCondition, Is.True);
        Assert.That(condition.Holds(_scene), Is.False);

        condition = ConditionParser.Parse("WallAhead");
        Assert.That(condition is WallAheadCondition, Is.True);
        Assert.That(condition.Holds(_scene), Is.True);

        condition = ConditionParser.Parse("ReachedGoal");
        Assert.That(condition is ReachedGoalCondition, Is.True);
        Assert.That(condition.Holds(_scene), Is.False);
    }

}