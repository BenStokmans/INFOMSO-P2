using INFOMSO_P3.Commands;
using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3_Test;

public class CommandTest
{
    private readonly MapElement[,] _map = {
        {MapElement.Open, MapElement.Blocked, MapElement.Open, MapElement.Open},
        {MapElement.Open, MapElement.Open, MapElement.Blocked, MapElement.Blocked},
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
    public void TurnCommandTest()
    {
        var command = new TurnCommand(90);
        command.Execute(_scene);

        Assert.That(_scene.GetCharacter().Direction, Is.EqualTo(Vector2.Down));

        command = new TurnCommand(-90);
        command.Execute(_scene);

        Assert.That(_scene.GetCharacter().Direction, Is.EqualTo(Vector2.Right));
    }

    [Test]
    public void MoveCommandTest()
    {
        new TurnCommand(270).Execute(_scene);

        var command = new MoveCommand(1);
        Assert.Throws<CommandException>(() => command.Execute(_scene));

        new TurnCommand(90).Execute(_scene);
        command.Execute(_scene);

        Assert.That(_scene.GetCharacter().Position, Is.EqualTo(new Vector2(1, 0)));

        new TurnCommand(90).Execute(_scene);
        Assert.Throws<CommandException>(() => command.Execute(_scene));
    }

    [Test]
    public void RepeatCommandTest()
    {
        var command = new RepeatCommand(3, [
            new MoveCommand(1)
        ]);
        command.Execute(_scene);

        Assert.That(_scene.GetCharacter().Position, Is.EqualTo(new Vector2(3, 0)));
    }
}