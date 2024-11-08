using INFOMSO_P2.Conditions;
using INFOMSO_P2.Game;

namespace INFOMSO_P2.Commands;

public class RepeatCommand : RepeatUntilCommand
{
    public int Times;
    public RepeatCommand() {}
    public RepeatCommand(int times, List<Command> commands) : base(new ExecutionCounterCondition(times), commands)
    {
        Times = times;
    }

    protected override void ParseCondition(string condition)
    {
        string[] parts = condition.Split(' ');
        if (parts is not ["Repeat", _, "times"] || !int.TryParse(parts[1], out Times))
            throw new CommandException(Line, "Invalid repeat command: not in the form 'Repeat <times> times'");

        Condition = new ExecutionCounterCondition(Times);
    }

    public override void Execute(Scene scene)
    {
        base.Execute(scene);
        Condition = new ExecutionCounterCondition(Times);
    }

    public override string ToString() => $"Repeat";
}