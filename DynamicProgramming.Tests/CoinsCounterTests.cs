using NUnit.Framework;

namespace DynamicProgramming.Tests;

[TestFixture]
[TestOf(typeof(CoinsCounter))]
public class CoinsCounterTests
{

    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(10, 4)]
    [TestCase(26, 13)]
    [TestCase(1000, 142511)]
    [TestCase(10000, 134235101)]
    public void GetNumberOfWays_ReturnsCorrectResults(int input, int expectedResult)
    {
        var result = CoinsCounter.GetNumberOfWays(input);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
