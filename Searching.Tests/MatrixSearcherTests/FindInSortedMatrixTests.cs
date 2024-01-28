using System;
using NUnit.Framework;
using Searching.Arrays;

namespace Searching.Tests.MatrixSearcherTests;

[TestFixture]
[TestOf(typeof(MatrixSearcher))]
public class FindInSortedMatrixTests
{
    private static MatrixSearcher InitMatrixSearcher()
    {
        return new();
    }

    [TestCase(10, 10)]
    [TestCase(10, 11)]
    [TestCase(11, 10)]
    [TestCase(5, 7)]
    [TestCase(1, 1)]
    [TestCase(10, 1)]
 //   [TestCase(1_000, 1_000)]
    public void WhenAllNumbersAreDistinct_FindsAllNumbers(int m, int n)
    {
        var matrix = GenerateMatrix(m, n, (i, j) => i * n + j);
        var searcher = InitMatrixSearcher();

        // Act & Assert
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

    [TestCase(10, 10)]
    [TestCase(10, 11)]
    [TestCase(11, 10)]
    [TestCase(5, 7)]
    [TestCase(1, 1)]
    [TestCase(10, 1)]
 //   [TestCase(1_000, 1_000)]
    public void WhenAllNumbersAreTheSame_FindsProperNumber(int m, int n)
    {
        const int value = 10;
        var matrix = GenerateMatrix(m, n, (_, _) => value);

        var searcher = InitMatrixSearcher();

        // Act & Assert
        var point = searcher.FindInSortedMatrix(matrix, value);
        Assert.That(point, Is.Not.Null);
        var pointA = searcher.FindInSortedMatrix(matrix, value - 1);
        Assert.That(pointA, Is.Null);
        var pointB = searcher.FindInSortedMatrix(matrix, value + 1);
        Assert.That(pointB, Is.Null);
    }

    [TestCase(10, 10)]
    [TestCase(10, 11)]
    [TestCase(11, 10)]
    [TestCase(5, 7)]
    [TestCase(1, 1)]
    [TestCase(10, 1)]
 //   [TestCase(1_000, 1_000)]
    public void WhenNumbersInRowAreTheSame_FindsAllNumbers(int m, int n)
    {
        var matrix = GenerateMatrix(m, n, (i, _) => i);

        var searcher = InitMatrixSearcher();

        // Act & Assert
        for (int i = 0; i < m; ++i)
        {
            var resultPoint = searcher.FindInSortedMatrix(matrix, matrix[i, 0]);
            Assert.That(resultPoint, Is.Not.Null);
            Assert.That(matrix[i, 0], Is.EqualTo(matrix[resultPoint.X, resultPoint.Y]));
        }
    }

    [TestCase(10, 10)]
    [TestCase(10, 11)]
    [TestCase(11, 10)]
    [TestCase(5, 7)]
    [TestCase(1, 1)]
    [TestCase(10, 1)]
 //   [TestCase(1_000, 1_000)]
    public void WhenNumbersInColumnAreTheSame_FindsAllNumbers(int m, int n)
    {
        var matrix = GenerateMatrix(m, n, (_, j) => j);

        var searcher = InitMatrixSearcher();

        // Act & Assert
        for (int j = 0; j < n; ++j)
        {
            var resultPoint = searcher.FindInSortedMatrix(matrix, matrix[0, j]);
            Assert.That(resultPoint, Is.Not.Null);
            Assert.That(matrix[0, j], Is.EqualTo(matrix[resultPoint.X, resultPoint.Y]));
        }
    }

    private static int[,] GenerateMatrix(int m, int n, Func<int, int, int> valueGenerator)
    {
        var result = new int[m, n];

        for (int i = 0; i < m; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                result[i, j] = valueGenerator(i, j);
            }
        }

        return result;
    }
}
