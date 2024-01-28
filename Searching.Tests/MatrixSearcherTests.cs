using Searching.Arrays;
using NUnit.Framework;

namespace Searching.Tests;

[TestFixture]
[TestOf(typeof(MatrixSearcher))]
public class MatrixSearcherTests
{
    private static MatrixSearcher InitMatrixSearcher()
    {
        return new();
    }

    [Test]
    public void FindInSortedMatrix_FindsAllNumbers()
    {
        var matrix = GenerateSortedMatrix(10, 10);
        var searcher = InitMatrixSearcher();

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);
        for (int i = 0; i < m; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                var targetPoint = new Point(i, j);
                var resultPoint = searcher.FindInSortedMatrix(matrix, matrix[i, j]);
                Assert.That(resultPoint, Is.EqualTo(targetPoint));
            }
        }
    }

    private static int[,] GenerateSortedMatrix(int m, int n)
    {
        var result = new int[m, n];

        for (int i = 0; i < m; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                result[i, j] = i * n + j;
            }
        }

        return result;
    }
}
