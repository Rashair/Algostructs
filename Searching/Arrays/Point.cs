namespace Searching.Arrays;

public class Point
{
    protected bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }
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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Point) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public static class PointExtensions
{
    public static int GetValue(this int[,] matrix, Point point)
    {
        return matrix[point.X, point.Y];
    }
}

