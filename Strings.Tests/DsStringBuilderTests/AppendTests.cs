using NUnit.Framework;

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
}
