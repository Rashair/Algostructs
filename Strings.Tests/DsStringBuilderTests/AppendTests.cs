using Tests.Common;

namespace Strings.Tests.DsStringBuilderTests;

[TestFixture]
[TestOf(typeof(DsStringBuilder))]
public class AppendTests
{
    [Test]
    public void GivenSingleAppend_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        sb.Append("abc");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abc"));
    }

    [Test]
    public void GivenMultipleAppends_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        sb.Append("abc");
        sb.Append("def");
        sb.Append("ghi");
        sb.Append("14512412");
        var result = sb.ToString();


        // Assert
        Assert.That(result, Is.EqualTo("abcdefghi14512412"));
    }

    [Test]
    public void GivenNoAppends_ThenEmptyStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GivenEmptyStringAppend_ThenEmptyStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        sb.Append("");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GivenConstructorArgument_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("absbsbasf");

        // Act
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("absbsbasf"));
    }

    [Test]
    public void GivenConstructorArgumentWithAppend_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("absbsbasf");

        // Act
        sb.Append("1231");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("absbsbasf1231"));
    }

    [TestCase(10_000_000, 10_000)]
    [TestCase(1_000_000, 1500)]
    [TestCase(100_000, 75)]
    [TestCase(10_000, 15)]
    public void GivenEnormousNumberOfStrings_ThenTheIsertIsFast(int numberOfAppends,
        int timeoutMs)
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act
        var ct = TestsHelper.CreateCancellationToken(timeoutMs);
        const string guid = "F32BD441-DF16-44C0-AC0D-6DBABE95260D";
        for (int i = 0; i < numberOfAppends; ++i)
        {
            ct.ThrowIfCancellationRequested();
            sb.Append(guid);
        }
        var result = sb.ToString();

        // Assert
        Assert.That(result, Has.Length.EqualTo(numberOfAppends * guid.Length));
    }
}
