namespace Graphs.RobotRoomCleaner;

public class RoomCleaner
{
    private int _row = 0;
    private int _col = 0;
    private RobotDirection _currentDirection = RobotDirection.Up;

    public void CleanRoom(IRobot robot)
    {
        robot.Clean();
        int count = 0;
        while (Move(robot))
        {
            ++count;
            robot.Clean();
        }

        if (count == 0)
        {
            return;
        }

        robot.TurnLeft();
        CleanRoom(robot);
        robot.TurnRight();

        ReverseDirection(robot);
        while (count > 0)
        {
            Move(robot);
            --count;
        }
    }

    private bool Move(IRobot robot)
    {
        var moveResult = robot.Move();
        if (!moveResult)
        {
            return false;
        }

        switch (_currentDirection)
        {
            case RobotDirection.Up:
                _row -= 1;
                break;

            case RobotDirection.Down:
                _row += 1;
                break;

            case RobotDirection.Left:
                _col -= 1;
                break;

            case RobotDirection.Right:
                _col += 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }

    private void ReverseDirection(IRobot robot)
    {
        TurnLeft(robot);
        TurnLeft(robot);
    }

    private void ChangeDirection(IRobot robot, RobotDirection targetDirection)
    {
        while (_currentDirection != targetDirection)
        {
            TurnLeft(robot);
        }
    }

    private void TurnLeft(IRobot robot)
    {
        robot.TurnLeft();
        _currentDirection = _currentDirection switch
        {
            RobotDirection.Up => RobotDirection.Left,
            RobotDirection.Left => RobotDirection.Down,
            RobotDirection.Down => RobotDirection.Right,
            RobotDirection.Right => RobotDirection.Up,
            _ => _currentDirection
        };
    }

    private enum RobotDirection
    {
        Up,
        Down,
        Left,
        Right,
    }
}
