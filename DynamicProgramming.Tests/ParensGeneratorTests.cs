using NUnit.Framework;

namespace DynamicProgramming.Tests;

[TestFixture]
[TestOf(typeof(ParensGenerator))]
public class ParensGeneratorTests
{
    [TestCase(1, "()")]
    [TestCase(2, "(()), ()()")]
    [TestCase(3, "((())), (()()), (())(), ()(()), ()()()")]
    [TestCase(4, "(((()))), ((()())), ((())()), ((()))(), (()(())), (()()()), (()())(), (())(()), (())()(), ()((())), ()(()()), ()(())(), ()()(()), ()()()()")]
    [TestCase(5, "((((())))), (((()()))), (((())())), (((()))()), (((())))(), ((()(()))), ((()()())), ((()())()), ((()()))(), ((())(())), ((())()()), ((())())(), ((()))(()), ((()))()(), (()((()))), (()(()())), (()(())()), (()(()))(), (()()(())), (()()()()), (()()())(), (()())(()), (()())()(), (())((())), (())(()()), (())(())(), (())()(()), (())()()(), ()(((()))), ()((()())), ()((())()), ()((()))(), ()(()(())), ()(()()()), ()(()())(), ()(())(()), ()(())()(), ()()((())), ()()(()()), ()()(())(), ()()()(()), ()()()()()")]
    public void GenerateParens_ReturnsCorrectResults(int input, string expectedResult)
    {
        var results = ParensGenerator.GenerateParens(input);
        Assert.That(string.Join(", ", results), Is.EqualTo(expectedResult));
    }
}
