namespace Searching.Arrays;

public class MatrixSearcher
{
    public Point? FindInSortedMatrix(int[,] matrix, int x)
    {
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        return BinarySearchInMatrix(matrix,
            new(0, 0), new(0, n - 1),
            new(m - 1, 0), new(m, n),
            x);
    }

    public Point? BinarySearchInMatrix(int[,] matrix,
        Point a,
        Point b,
        Point c,
        Point d,
        int x)
    {
        // Matrix of 0 size
        if (a == b || a == c)
        {
            return null;
        }

        var midPoint = new Point((a.X + d.X) / 2, (a.Y + d.Y) / 2);
        if (matrix[midPoint.X, midPoint.Y] == x)
        {
            return midPoint;
        }

        Point? firstQuadrantResult;
        if (matrix[midPoint.X, midPoint.Y] > x)
        {
            //  A---------B
            //  |  1 |  2 |
            //  |----M----|
            //  |  3 |    |
            //  C---------D
            firstQuadrantResult = BinarySearchInMatrix(matrix, a, new(b.X, midPoint.Y), new(midPoint.X, c.Y), midPoint, x);
        }
        else
        {
            //  A---------B
            //  |    |  2 |
            //  |----M----|
            //  | 3  | 1  |
            //  C---------D
            firstQuadrantResult = BinarySearchInMatrix(matrix, midPoint, new(midPoint.X, b.Y), new(c.X, midPoint.Y), d, x);
        }

        // If we didn't find anything in the 1st quadrant, leftover quadrants are the same for both cases
        var secondQuadrantResult = firstQuadrantResult ??
                                   BinarySearchInMatrix(matrix, new(a.X, midPoint.Y), b, midPoint, new(d.X, midPoint.Y), x);
        var thirdQuadrantResult = secondQuadrantResult ??
                                  BinarySearchInMatrix(matrix, new(midPoint.X, a.Y), midPoint, c, new(d.X, midPoint.Y), x);

        return thirdQuadrantResult;
    }
}
