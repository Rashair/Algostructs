namespace Searching.Arrays;

public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Point? a, Point? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b == null)
        {
            return false;
        }

        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
