namespace Searching.Arrays;

public class Quadrant
{
    public Point A { get; }
    public Point B { get; }
    public Point C { get; }
    public Point D { get; }

    public Quadrant(Point a, Point b, Point c, Point d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public Point GetMidPoint()
    {
        return new((A.X + D.X) / 2, (A.Y + D.Y) / 2);
    }

    public int Size => (D.X - A.X + 1) * (D.Y - A.Y + 1);

    public bool IsValid()
    {
        return A.X <= D.X && A.Y <= D.Y;
    }

    public Quadrant GetUpperLeftSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var leftUpMidPoint = new Point(midPoint.X - 1, midPoint.Y - 1);
        return new(A, new(B.X, leftUpMidPoint.Y), new(leftUpMidPoint.X, C.Y), leftUpMidPoint);
    }

    public Quadrant GetUpperMidSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var upMidPoint = new Point(midPoint.X - 1, midPoint.Y);
        return new(new(A.X, upMidPoint.Y), B, upMidPoint, new(upMidPoint.X, D.Y));
    }

    public Quadrant GetLeftMidSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var leftMidPoint = new Point(midPoint.X, midPoint.Y - 1);
        return new(new(leftMidPoint.X, A.Y), leftMidPoint, C, new(D.X, leftMidPoint.Y));
    }

    public Quadrant GetLowerRightSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var rightDownMidPoint = new Point(midPoint.X + 1, midPoint.Y + 1);
        return new(rightDownMidPoint, new(rightDownMidPoint.X, B.Y), new(C.X, rightDownMidPoint.Y), D);
    }

    public Quadrant GetRightMidSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var rightMidPoint = new Point(midPoint.X, midPoint.Y + 1);
        return new(new(A.X, rightMidPoint.Y), B, rightMidPoint, new(rightMidPoint.X, D.Y));
    }

    public Quadrant GetLowerMidSubQuadrant()
    {
        var midPoint = GetMidPoint();
        var downMidPoint = new Point(midPoint.X + 1, midPoint.Y);
        return new(new(downMidPoint.X, A.Y), downMidPoint,
            C, new(D.X, downMidPoint.Y));
    }
}
