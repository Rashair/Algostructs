using Tests.Common;

namespace Strings.Tests.DsStringBuilderTests;

[TestFixture]
[TestOf(typeof(DsStringBuilder))]
public class ClearTests
{
    [Test]
    public void GivenNonEmptyString_ThenClearMakesItEmpty()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act
        sb.Clear();
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GivenEmptyString_ThenClearKeepsItEmpty()
    {
        // Arrange
        var sb = new DsStringBuilder("");

        // Act
        sb.Clear();
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GivenLargeString_ThenClearMakesItEmpty()
    {
        // Arrange
        var ct = TestsHelper.CreateCancellationToken(20);
        var sb = new DsStringBuilder(new('a', 1000000));

        // Act
        sb.Clear();
        ct.ThrowIfCancellationRequested();
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }
}
