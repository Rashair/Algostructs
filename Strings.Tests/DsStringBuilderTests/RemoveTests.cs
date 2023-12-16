using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Tests.Common;

namespace Strings.Tests.DsStringBuilderTests;

[TestFixture]
[TestOf(typeof(DsStringBuilder))]
public class RemoveTests
{
    [Test]
    public void GivenStartRemove_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abff_fffc");

        // Act
        sb.Remove(0,6);
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("ffc"));
    }

    [Test]
    public void GivenMiddleRemove_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abff_fffc");

        // Act
        sb.Remove(2,6);
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abc"));
    }

    [Test]
    public void GivenEndRemove_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abff_fffc");

        // Act
        sb.Remove(3,6);
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("abf"));
    }

    [Test]
    public void GivenMultipleRemoves_ThenCorrectStringIsReturned()
    {
        // Arrange
        var sb = new DsStringBuilder("abc def ghi 145 12 41 2//");

        // Act
        sb.Remove(0,3);
        sb.Remove(1,3);
        sb.Remove(sb.Length - 3, 3);
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("  ghi 145 12 41 "));
    }

    [Test]
    public void GivenRemoveOnEmptyBuilder_ThenExceptionIsThrown()
    {
        // Arrange
        var sb = new DsStringBuilder();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sb.Remove(0,2));
        Assert.That(sb.ToString(), Is.EqualTo(""));
    }

    [Test]
    public void GivenRemoveWithInvalidPosition_ThenExceptionIsThrown()
    {
        // Arrange
        var sb = new DsStringBuilder("abc def SDFSóó");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sb.Remove(100,1));
        Assert.That(sb.ToString(), Is.EqualTo("abc def SDFSóó"));
    }

    [Test]
    public void GivenRemoveWithInvalidLength_ThenExceptionIsThrown()
    {
        // Arrange
        var sb = new DsStringBuilder("abc def SDFSóó");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sb.Remove(0,sb.Length + 1));
        Assert.That(sb.ToString(), Is.EqualTo("abc def SDFSóó"));
    }

    [TestCase(150_000, 6_000)]
    [TestCase(100_000, 1_500)]
    [TestCase(50_000, 500)]
    [TestCase(10_000, 30)]
    public void GivenEnormousNumberOfStrings_ThenTheRemoveIsFast(int numberOfRepeats,
        int timeoutMs)
    {
        // Arrange
        var ct = TestsHelper.CreateCancellationToken(timeoutMs);
        const string phrase = "a#G1";
        string finalStr = PrepareBigString(phrase, numberOfRepeats);
        var sb = new DsStringBuilder(finalStr);

        // Act
        for (int i = 0; i < numberOfRepeats - 1; ++i)
        {
            ct.ThrowIfCancellationRequested();
            sb.Remove(0, phrase.Length);
        }
        var result = sb.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(phrase));
    }

    private static string PrepareBigString(string phrase, int numberOfRepeats)
    {
        var externalBuilder = new System.Text.StringBuilder();
        foreach (var str in Enumerable.Repeat(phrase, numberOfRepeats))
        {
            externalBuilder.Append(str);
        }

        return externalBuilder.ToString();
    }
}
