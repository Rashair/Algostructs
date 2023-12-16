using Tests.Common;

namespace Strings.Tests.DsStringBuilderTests;

[TestFixture]
[TestOf(typeof(DsStringBuilder))]
public class ReplaceTests
{
    [Test]
    public void GivenReplaceInMiddle_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abcabcabc");

        // Act
        sb.Replace("bca", "123");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("a123123bc"));
    }

    [Test]
    public void GivenReplaceAtStart_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abcabcabc");

        // Act
        sb.Replace("abc", "123");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("123123123"));
    }

    [Test]
    public void GivenReplaceAtEnd_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abcabcabc");

        // Act
        sb.Replace("cab", "123");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("ab123123c"));
    }

    [Test]
    public void GivenReplaceWithEmptyString_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abcabcabc");

        // Act
        sb.Replace("abc", "");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GivenReplaceNonExistingPattern_ThenOriginalStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abcabcabc");

        // Act
        sb.Replace("def", "123");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abcabcabc"));
    }

    [TestCase(50_000, 10_000)]
    [TestCase(25_000, 5_000)]
    [TestCase(10_000, 1_000)]
    [TestCase(1_000, 10)]
    public void GivenEnormousNumberOfStrings_ThenTheReplaceIsFast(int numberOfStrings,
        int timeoutMs)
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        var ct = TestsHelper.CreateCancellationToken(timeoutMs);
        const string guid = "F32BD441-DF16-44C0-AC0D-6DBABE95260D";
        for (int i = 0; i < numberOfStrings; ++i)
        {
            sb.Append(guid);
            ct.ThrowIfCancellationRequested();
            sb.Replace(guid, "1");
        }
        var result = sb.ToString();

        // Assert
        Assert.That(result, Has.Length.EqualTo(numberOfStrings));
    }


    [TestCase(50_000, 10_000)]
    [TestCase(25_000, 5_000)]
    [TestCase(10_000, 1_000)]
    [TestCase(1_000, 10)]
    public void GivenEnormousNumberOfReplacements_ThenTheReplaceIsFast(int numberOfReplacements,
        int timeoutMs)
    {
        // Arrange
        var sb = new DsStringBuilder(new('a', numberOfReplacements));

        // Act
        var ct = TestsHelper.CreateCancellationToken(timeoutMs);
        const string replacement = "23";
        for (int i = 0; i < numberOfReplacements; ++i)
        {
            ct.ThrowIfCancellationRequested();
            sb.Replace("a", replacement);
        }
        var result = sb.ToString();

        // Assert
        Assert.That(result, Has.Length.EqualTo(numberOfReplacements * replacement.Length));
    }
}
