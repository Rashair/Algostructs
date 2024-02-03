using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Parallelism.Tests;

[TestFixture]
[TestOf(typeof(ParallelFizzBuzz))]
public class ParallelFizzBuzzTests
{
    [TestCase(1)]
    [TestCase(17)]
    [TestCase(50)]
    [TestCase(50)]
    [TestCase(5160)]
    public async Task ShouldPrintCorrectValuesInOrder(int n)
    {
        var streamWriter = new StringWriter();
        var parallelFizzBuzz = new ParallelFizzBuzz(n, streamWriter);

        // Act
        await parallelFizzBuzz.Execute();

        // Assert
        var result = streamWriter.ToString();

        var split = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 1; i <= n; ++i)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                Assert.That(split[i - 1], Is.EqualTo("FizzBuzz"));
            }
            else if (i % 3 == 0)
            {
                Assert.That(split[i - 1], Is.EqualTo("Fizz"));
            }
            else if (i % 5 == 0)
            {
                Assert.That(split[i - 1], Is.EqualTo("Buzz"));
            }
            else
            {
                Assert.That(split[i - 1], Is.EqualTo(i.ToString()));
            }
        }
    }

    [Test]
    public async Task ShouldBeCancellable()
    {
        // Arrange
        var parallelFizzBuzz = new ParallelFizzBuzz(100);
        var source = new CancellationTokenSource();
        await source.CancelAsync();

        // Act
        var ex = Assert.ThrowsAsync<TaskCanceledException>(async () => await parallelFizzBuzz.Execute(source.Token));
        Assert.That(ex, Is.Not.Null);
    }
}
