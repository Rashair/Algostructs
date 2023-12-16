using Tests.Common;

namespace Strings.Tests.DsStringBuilderTests;

[TestFixture]
[TestOf(typeof(DsStringBuilder))]
public class InsertTests
{
    [Test]
    public void GivenInsertAtStart_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act
        sb.Insert(0, "def");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("defabc"));
    }

    [Test]
    public void GivenInsertAtMiddle_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act
        sb.Insert(1, "def");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("adefbc"));
    }

    [Test]
    public void GivenInsertAtEnd_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act
        sb.Insert(3, "def");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abcdef"));
    }

    [Test]
    public void GivenInsertWithEmptyString_ThenOriginalStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act
        sb.Insert(1, "");
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abc"));
    }

    [Test]
    public void GivenInsertWithInvalidPosition_ThenExceptionIsThrown()
    {
        // Arrange
        var sb = new DsStringBuilder("abc");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sb.Insert(5, "def"));
    }
    
  
    [TestCase(30_000, 6_000)]
    [TestCase(10_000, 2_000)]
    [TestCase(5_000, 1_000)]
    [TestCase(1_000, 10)]
    public void GivenEnormousNumberOfStrings_ThenTheInsertIsFast(int numberOfInserts,
        int timeoutMs)
    {
        // Arrange
        var sb = new DsStringBuilder("E92224CB-9977-4526-A2D0-F67F68A712AF");

        // Act
        var ct = TestsHelper.CreateCancellationToken(timeoutMs);
        const string guid = "F32BD441-DF16-44C0-AC0D-6DBABE95260D";
        for (int i = 0; i < numberOfInserts; ++i)
        {
            ct.ThrowIfCancellationRequested();
            sb.Insert(guid.Length, guid);
        }
        var result = sb.ToString();

        // Assert
        Assert.That(result, Has.Length.EqualTo((numberOfInserts + 1) * guid.Length));
    }
}
