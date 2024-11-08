using INFOMSO_P2.Commands;

namespace INFOMSO_P2_Test;

public class CommandParserTest
{
    [Test]
    public void CommandParserTestMove()
    {
        Command? command = CommandParser.ParseCommand(0, "Move 1");
        Assert.Multiple(() =>
        {
            Assert.That(command, Is.TypeOf<MoveCommand>());
            Assert.That(((MoveCommand)command).Distance, Is.EqualTo(1));
        });
    }

    [Test]
    public void CommandParserTestTurn()
    {
        Command? command = CommandParser.ParseCommand(0, "Turn right");
        Assert.Multiple(() =>
        {
            Assert.That(command, Is.TypeOf<TurnCommand>());
            Assert.That(((TurnCommand)command).Degrees, Is.EqualTo(90));
        });
    }

    [Test]
    public void CommandParserTestRepeat()
    {
        Command? command = CommandParser.ParseCommand(0,"Repeat 3 times\n\tMove 1");
        Assert.Multiple(() =>
        {
            Assert.That(command, Is.TypeOf<RepeatCommand>());
            Assert.That(((RepeatCommand)command).Times, Is.EqualTo(3));
            Assert.That(((RepeatCommand)command).Commands.Count, Is.EqualTo(1));
            Assert.That(((RepeatCommand)command).Commands[0], Is.TypeOf<MoveCommand>());
            Assert.That(((MoveCommand)((RepeatCommand)command).Commands[0]).Distance, Is.EqualTo(1));
        });
    }
}