namespace Searching.Arrays;

public class MatrixSearcher
{
    public Point? FindInSortedMatrix(int[,] matrix, int x)
    {
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        return BinarySearchInMatrix(matrix,
            new(new(0, 0), new(0, n - 1),
                new(m - 1, 0), new(m - 1, n - 1)),
            x);
    }

    private Point? BinarySearchInMatrix(int[,] matrix,
        Quadrant quadrant,
        int x)
    {
        var midPoint = quadrant.GetMidPoint();
        if (matrix.GetValue(midPoint) == x)
        {
            return midPoint;
        }

        // First quadrant is different for both
        return matrix.GetValue(midPoint) > x
            ? FindInUpperMidQuadrants(matrix, quadrant, x)
            : FindInLowerRightQuadrants(matrix, quadrant, x);
    }

    private Point? FindInLowerRightQuadrants(int[,] matrix, Quadrant quadrant, int x)
    {
        //  A---------B
        //  |    |  2 |
        //  |----M----|
        //  | 3  | 1  |
        //  C---------D
        var lowerRightMidQuadrant = quadrant.GetLowerRightSubQuadrant();
        var rightMidQuadrant = quadrant.GetRightMidSubQuadrant();
        var lowerMidQuadrant = quadrant.GetLowerMidSubQuadrant();

        return FindInQuadrants(matrix,
            lowerRightMidQuadrant,
            rightMidQuadrant,
            lowerMidQuadrant,
            x);
    }

    private Point? FindInUpperMidQuadrants(int[,] matrix, Quadrant quadrant, int x)
    {
        //  A---------B
        //  |  1 |  2 |
        //  |----M----|
        //  |  3 |    |
        //  C---------D
        var upperLeftSubQuadrant = quadrant.GetUpperLeftSubQuadrant();
        var upperMidQuadrant = quadrant.GetUpperMidSubQuadrant();
        var leftMidQuadrant = quadrant.GetLeftMidSubQuadrant();

        return FindInQuadrants(matrix,
            upperLeftSubQuadrant,
            upperMidQuadrant,
            leftMidQuadrant,
            x);
    }

    private Point? FindInQuadrants(int[,] matrix,
        Quadrant firstQuadrant,
        Quadrant secondQuadrant,
        Quadrant thirdQuadrant,
        int x)
    {
        var finalResult = FindInQuadrant(matrix, firstQuadrant, x);
        finalResult ??= FindInQuadrant(matrix, secondQuadrant, x);
        finalResult ??= FindInQuadrant(matrix, thirdQuadrant, x);

        return finalResult;
    }

    private Point? FindInQuadrant(int[,] matrix,
        Quadrant quadrant,
        int x)
    {
        if (IsOutsideBounds(matrix, quadrant) || !quadrant.IsValid())
        {
            return null;
        }

        if (quadrant.Size == 1)
        {
            return matrix.GetValue(quadrant.A) == x ? quadrant.A : null;
        }

        return BinarySearchInMatrix(matrix, quadrant, x);
    }

    private static bool IsOutsideBounds(int[,] matrix, Quadrant quadrant)
    {
        return IsOutsideBounds(matrix, quadrant.A)
               || IsOutsideBounds(matrix, quadrant.B)
               || IsOutsideBounds(matrix, quadrant.C)
               || IsOutsideBounds(matrix, quadrant.D);
    }

    private static bool IsOutsideBounds(int[,] matrix, Point point)
    {
        return point.X < 0
               || point.Y < 0
               || point.X >= matrix.GetLength(0)
               || point.Y >= matrix.GetLength(1);
    }
}
