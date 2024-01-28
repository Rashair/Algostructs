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

        // First quadrant is different for both
        if (matrix[midPoint.X, midPoint.Y] > x)
        {
            //  A---------B
            //  |  1 |  2 |
            //  |----M----|
            //  |  3 |    |
            //  C---------D
            var leftUpMidPoint = new Point(midPoint.X - 1, midPoint.Y - 1);
            var firstQuadrantResult = BinarySearchInMatrix(matrix, a, new(b.X, leftUpMidPoint.Y), new(leftUpMidPoint.X, c.Y),
                leftUpMidPoint, x);

            var upMidPoint = new Point(leftUpMidPoint.X, midPoint.Y);
            var leftMidPoint = new Point(midPoint.X, leftUpMidPoint.Y);

            return firstQuadrantResult ?? FindInNeighbouringQudrants(matrix, a, b, c, d,
                upMidPoint,
                leftMidPoint,
                x);
        }
        else
        {
            //  A---------B
            //  |    |  2 |
            //  |----M----|
            //  | 3  | 1  |
            //  C---------D
            var rightDownMidPoint = new Point( midPoint.X + 1, midPoint.Y + 1);
            var firstQuadrantResult = BinarySearchInMatrix(matrix, rightDownMidPoint, new(rightDownMidPoint.X, b.Y),
                new(c.X, rightDownMidPoint.Y), d, x);

            var rightMidPoint = new Point(midPoint.X, rightDownMidPoint.Y);
            var downMidPoint = new Point(rightDownMidPoint.X, midPoint.Y);
             return firstQuadrantResult ?? FindInNeighbouringQudrants(matrix, a, b, c, d,
                 rightMidPoint,
                 downMidPoint,
                x);
        }
    }

    private Point? FindInNeighbouringQudrants(int[,] matrix,
        Point a,
        Point b,
        Point c,
        Point d,
        Point secondQuadrantMidPointNeighbour,
        Point thirdQuadrantMidPointNeighbour,
        int x)
    {
        // 2nd quadrant
        var finalResult = BinarySearchInMatrix(matrix, new(a.X, secondQuadrantMidPointNeighbour.Y), b,
            secondQuadrantMidPointNeighbour, new(secondQuadrantMidPointNeighbour.X, d.Y), x);

        // 3rd quadrant
        finalResult ??= BinarySearchInMatrix(matrix, new(thirdQuadrantMidPointNeighbour.X, a.Y), thirdQuadrantMidPointNeighbour,
            c, new(d.X, thirdQuadrantMidPointNeighbour.Y), x);

        return finalResult;
    }

    private static bool IsOutsideBounds(int[,] matrix, Point point)
    {
        return point.X < 0
               || point.Y < 0
               || point.X >= matrix.GetLength(0)
               || point.Y >= matrix.GetLength(1);
    }

}
