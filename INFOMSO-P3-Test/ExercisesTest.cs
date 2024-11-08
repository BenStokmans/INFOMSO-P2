using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;

namespace INFOMSO_P3_Test;

public class ExercisesTest
{
    [Test]
    public void TestFileMapParser()
    {
        string map = "oo+++\r\n+oo++\r\n++o++\r\n++o++\r\n++oox";
        var parser = new FileMapParser();
        MapElement[,] parsedMap = parser.Parse(map);

        Assert.That(parsedMap[0, 0], Is.EqualTo(MapElement.Open));
        Assert.That(parsedMap[1, 0], Is.EqualTo(MapElement.Open));
        Assert.That(parsedMap[2, 0], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[3, 0], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[4, 0], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[0, 1], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[1, 1], Is.EqualTo(MapElement.Open));
        Assert.That(parsedMap[2, 1], Is.EqualTo(MapElement.Open));
        Assert.That(parsedMap[3, 1], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[4, 1], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[0, 2], Is.EqualTo(MapElement.Blocked));
        Assert.That(parsedMap[1, 1], Is.EqualTo(MapElement.Open));
        Assert.That(parsedMap[4, 4], Is.EqualTo(MapElement.EndState));
    }

    [Test]
    public void TestShapeExercise()
    {
        var exercise = new ShapeExercise();
        Scene scene = new Scene(exercise.Map);

        Assert.That(exercise.IsCompleted(scene), Is.False);

        var character = scene.GetCharacter();
        for (var i = 0; i < exercise.Map.GetLength(0); i++)
        for (var j = 0; j < exercise.Map.GetLength(1); j++)
            if (exercise.Map[i, j] == MapElement.Open)
                character.Path.Add(new Vector2(i, j));

        Assert.That(exercise.IsCompleted(scene), Is.True);
    }

    [Test]
    public void TestPathfindingExercise()
    {
        var exercise = new PathfindingExercise();
        Scene scene = new Scene(exercise.Map);

        Assert.That(exercise.IsCompleted(scene), Is.False);

        var character = scene.GetCharacter();
        character.Position = scene.GoalPosition ?? new Vector2(0,0);

        Assert.That(exercise.IsCompleted(scene), Is.True);
    }
}