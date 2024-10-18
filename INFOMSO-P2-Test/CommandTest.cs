using INFOMSO_P2.Commands;
using INFOMSO_P2.Game;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2_Test;

public class CommandTest
{
    private Scene scene;

    [SetUp]
    public void Setup()
    {
        scene = new Scene();
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
        var command = new MoveCommand(1);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Position, Is.EqualTo(new Vector2(1, 0)));

        command = new MoveCommand(2);
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Position, Is.EqualTo(new Vector2(3, 0)));
    }

    [Test]
    public void RepeatCommandTest()
    {
        var command = new RepeatCommand(3, new List<ICommand>
        {
            new MoveCommand(1),
        });
        command.Execute(scene);

        Assert.That(scene.GetCharacter()?.Position, Is.EqualTo(new Vector2(3, 0)));
    }
}