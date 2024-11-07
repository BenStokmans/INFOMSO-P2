using INFOMSO_P2.Commands;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2_Test;

public class CommandTest
{
    private MapElement[,] map = {
        {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.Open},
        {MapElement.Open, MapElement.Open, MapElement.Blocked, MapElement.Blocked},
        {MapElement.Open, MapElement.Open, MapElement.Open, MapElement.Blocked},
        {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.EndState},
    };
    private Scene scene;

    [SetUp]
    public void Setup()
    {
        scene = new Scene(map);
        scene.Entities.Add(new Character());
    }

    [Test]
    public void TurnCommandTest()
    {
        var command = new TurnCommand(90);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Direction, Is.EqualTo(Vector2.Down));

        command = new TurnCommand(-90);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Direction, Is.EqualTo(Vector2.Right));
    }

    [Test]
    public void MoveCommandTest()
    {
        new TurnCommand(270).Execute(scene);

        var command = new MoveCommand(1);
        Assert.Throws<BlockedException>(() => command.Execute(scene));

        new TurnCommand(90).Execute(scene);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Position, Is.EqualTo(new Vector2(1, 0)));

        new TurnCommand(90).Execute(scene);
        Assert.Throws<OutOfBoundsException>(() => command.Execute(scene));
    }

    [Test]
    public void RepeatCommandTest()
    {
        var command = new RepeatCommand(3, [
            new MoveCommand(1)
        ]);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Position, Is.EqualTo(new Vector2(3, 0)));
    }
}