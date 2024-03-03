namespace Strings.Tests;

public class ReSpacerTests
{
    [Test]
    public void BaseCase()
    {
        var document = "jesslookedjustliketimherbrother";
        var dictionary = new HashSet<string>()
        {
            "looked",
            "just",
            "like",
            "her",
            "brother"
        };

        var spacer = new ReSpacer(dictionary, document);

        // Act
       var res = spacer.ReSpace();

        Assert.That(res, Is.EqualTo("jess looked just like tim her brother"));
    }

    [Test]
    public void OtherCase()
    {
        var document = "jesstephen";
        var dictionary = new HashSet<string>()
        {
            "jess",
            "stephen",
        };

        var spacer = new ReSpacer(dictionary, document);

        // Act
        var res = spacer.ReSpace();

        Assert.That(res, Is.EqualTo("jes stephen"));
    }
}
