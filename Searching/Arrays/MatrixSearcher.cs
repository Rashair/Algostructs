namespace Searching.Arrays;

public class MatrixSearcher
{
    public Point? FindInSortedMatrix(int[,] matrix, int x)
    {
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        return BinarySearchInMatrix(matrix,
            new(0, 0), new(0, n - 1),
            new(m - 1, 0), new(m - 1, n - 1),
            x);
    }

    private Point? BinarySearchInMatrix(int[,] matrix,
        Point a,
        Point b,
        Point c,
        Point d,
        int x)
    {
        if (IsOutsideBounds(matrix, a)
            || IsOutsideBounds(matrix, b)
            || IsOutsideBounds(matrix, c)
            || IsOutsideBounds(matrix, d))
        {
            return null;
        }

        if (a == d)
        {
            return matrix[a.X, a.Y] == x ? a : null;
        }

        if (a.X > d.X || a.Y > d.Y)
        {
            return null;
        }

        var midPoint = new Point((a.X + d.X) / 2, (a.Y + d.Y) / 2);
        if (matrix[midPoint.X, midPoint.Y] == x)
        {
            return midPoint;
        }

        var preMidPointX = midPoint.X - 1;
        var preMidPointY = midPoint.Y - 1;
        var postMidPointX = midPoint.X + 1;
        var postMidPointY = midPoint.Y + 1;

        Point? firstQuadrantResult;
        if (matrix[midPoint.X, midPoint.Y] > x)
        {
            //  A---------B
            //  |  1 |  2 |
            //  |----M----|
            //  |  3 |    |
            //  C---------D
            var leftUpMidPoint = new Point(preMidPointX, preMidPointY);
            firstQuadrantResult = BinarySearchInMatrix(matrix, a, new(b.X, leftUpMidPoint.Y), new(leftUpMidPoint.X, c.Y),
                leftUpMidPoint, x);
            var upMidPoint = new Point(leftUpMidPoint.X, midPoint.Y);
            var secondQuadrantResult = firstQuadrantResult ??
                                       BinarySearchInMatrix(matrix, new(a.X, upMidPoint.Y), b, upMidPoint, new(upMidPoint.X, d.Y), x);

            var leftMidPoint = new Point(midPoint.X, leftUpMidPoint.Y);
            var thirdQuadrantResult = secondQuadrantResult ??
                                      BinarySearchInMatrix(matrix, new(leftMidPoint.X, a.Y), leftMidPoint, c, new(d.X, leftMidPoint.Y), x);
            return thirdQuadrantResult;
        }
        else
        {
            //  A---------B
            //  |    |  2 |
            //  |----M----|
            //  | 3  | 1  |
            //  C---------D
            var rightDownMidPoint = new Point(postMidPointX, postMidPointY);
            firstQuadrantResult = BinarySearchInMatrix(matrix, rightDownMidPoint, new(rightDownMidPoint.X, b.Y),
                new(c.X, rightDownMidPoint.Y), d, x);
            var rightMidPoint = new Point(midPoint.X, rightDownMidPoint.Y);
            var secondQuadrantResult = firstQuadrantResult ??
                                       BinarySearchInMatrix(matrix, new(a.X, rightMidPoint.Y), b, rightMidPoint, new(rightMidPoint.X, d.Y),
                                           x);

            var downMidPoint = new Point(rightDownMidPoint.X, midPoint.Y);
            var thirdQuadrantResult = secondQuadrantResult ??
                                      BinarySearchInMatrix(matrix, new(downMidPoint.X, a.Y), downMidPoint, c, new(d.X, downMidPoint.Y), x);
            return thirdQuadrantResult;
        }
    }

    private static bool IsOutsideBounds(int[,] matrix, Point point)
    {
        return point.X < 0
               || point.Y < 0
               || point.X >= matrix.GetLength(0)
               || point.Y >= matrix.GetLength(1);
    }
}
