using System.Text.RegularExpressions;
using NUnit.Framework;
using Parallelism;

namespace Parallelism.Tests;

[TestFixture]
[TestOf(typeof(ParallelFizzBuzz))]
public class ParallelFizzBuzzTests
{
    [TestCase(1)]
    [TestCase(17)]
    [TestCase(50)]
    public async Task ShouldPrintCorrectValues(int n)
    {
        var streamWriter = new StringWriter();
        var parallelFizzBuzz = new ParallelFizzBuzz(n);

        // Act
        await parallelFizzBuzz.Execute();

        var result = streamWriter.ToString();
        var (num, fizz, buzz, fizzBuzz) = GetCountsPerCategory(n);

        var actualCountFizzBuzz = Regex.Matches(result, "FizzBuzz").Count;
        Assert.That(actualCountFizzBuzz, Is.EqualTo(fizzBuzz));

        var actualCountFizz = Regex.Matches(result, "Fizz").Count;
        Assert.That(actualCountFizz, Is.EqualTo(fizz));

        var actualCountBuzz = Regex.Matches(result, "Buzz").Count;
        Assert.That(actualCountBuzz, Is.EqualTo(buzz));

        var actualCountNum = Regex.Matches(result, "\\d+").Count;
        Assert.That(actualCountNum, Is.EqualTo(num));
    }

    [Test]
    public async Task ShouldBeCancellable()
    {
        // Arrange
        var parallelFizzBuzz = new ParallelFizzBuzz(100);
        var source = new CancellationTokenSource();
        await source.CancelAsync();

        // Act
        var ex = Assert.ThrowsAsync<OperationCanceledException>(async () => await parallelFizzBuzz.Execute(source.Token));
        Assert.That(ex, Is.Not.Null);
    }

    private static (int, int, int, int) GetCountsPerCategory(int n)
    {
        var num = 0;
        var fizz = 0;
        var buzz = 0;
        var fizzBuzz = 0;
        for (var i = 1; i <= n; ++i)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                ++fizzBuzz;
            }
            else if (i % 3 == 0)
            {
                ++fizz;
            }
            else if (i % 5 == 0)
            {
                ++buzz;
            }
            else
            {
                ++num;
            }
        }

        return (num, fizz, buzz, fizzBuzz);
    }

    [TestCase(1, 1, 0, 0, 0)]
    [TestCase(15, 8, 4, 2, 1)]
    [TestCase(30, 16, 8, 4, 2)]
    public void GetCountsPerCategory_ReturnsCorrectCounts(int n, int num, int fizz, int buzz, int fizzBuz)
    {
        var actual = GetCountsPerCategory(n);
        Assert.That(actual, Is.EqualTo((num, fizz, buzz, fizzBuz)));
    }
}
