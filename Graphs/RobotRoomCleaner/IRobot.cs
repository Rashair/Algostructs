namespace Graphs.RobotRoomCleaner;

public interface IRobot
{
    /// returns true if next cell is open and robot moves into the cell.
    /// returns false if next cell is obstacle and robot stays on the current cell.
    bool Move();

    /// Robot will stay on the same cell after calling turnLeft/turnRight.
    /// Each turn will be 90 degrees.
    void TurnLeft();

    /// Robot will stay on the same cell after calling turnLeft/turnRight.
    /// Each turn will be 90 degrees.
    void TurnRight();

    /// Clean the current cell.
    void Clean();
}

public class Robot : IRobot
{
    public const int BlockedPos = 0;
    public const int FreePos = 1;
    public const int CleanedPos = 2;

    private int _m;
    private int _n;
    private readonly int[,] _board;

    private Direction _direction;
    private int _row;
    private int _col;

    public Robot(int initialRow,
        int initialCol,
        int[,] board)
    {
        _row = initialRow;
        _col = initialCol;
        _m = board.GetLength(0);
        _n = board.GetLength(1);
        _board = board;
    }

    public bool Move()
    {
        switch (_direction)
        {
            case Direction.Up:
                if (_row > 0)
                {
                    _row -= 1;
                    return true;
                }

                return false;

            case Direction.Down:
                if (_row < _m - 1)
                {
                    _row += 1;
                    return true;
                }

                return false;

            case Direction.Left:
                if (_col > 0)
                {
                    _col -= 1;
                    return true;
                }

                return false;
            case Direction.Right:
                if (_col < _n - 1)
                {
                    _col += 1;
                    return true;
                }

                return false;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void TurnLeft()
    {
        _direction = _direction switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => _direction
        };
    }

    public void TurnRight()
    {
        _direction = _direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => _direction
        };
    }

    public void Clean()
    {
        if (_board[_row, _col] == BlockedPos)
        {
            throw new InvalidOperationException("Invalid pos");
        }

        _board[_row, _col] = CleanedPos;
    }

    public bool IsBoardCleaned()
    {
        foreach (int row in _board)
        {
            if (row is FreePos)
            {
                return false;
            }
        }

        return true;
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
