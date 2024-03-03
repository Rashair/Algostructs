using Graphs.RobotRoomCleaner;
using NUnit.Framework;

namespace Graphs.Tests;

[TestFixture]
[TestOf(typeof(RoomCleaner))]
public class CleanerTests
{
    [Test]
    public void BaseCase()
    {
        var board = new int[,]
        {
            { 1, 1, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 1 },
            { 1, 0, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 1, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        var initialRow = 1;
        var initialCol = 3;

        var robot = new Robot(initialRow, initialCol, board);

        var cleaner = new RoomCleaner();

        // Act
        cleaner.CleanRoom(robot);

        // Assert
        Assert.That(robot.IsBoardCleaned(), Is.True);
    }
}
