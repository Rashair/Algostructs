using NUnit.Framework;
using Parallelism.Philosophers;

namespace Parallelism.Tests.Philosophers;

[TestFixture]
[TestOf(typeof(Philosopher))]
public class PhilosopherTests
{
    [Test]
    public void AfterEat_ShouldNotBeHungry()
    {
        // Arrange
        var philosopher = new Philosopher();
        var meal = new Noddles();
        var leftChopstick = new Chopstick();
        var rightChopstick = new Chopstick();

        // Act
        philosopher.Eat(leftChopstick, rightChopstick, meal);

        // Assert
        Assert.That(philosopher.IsHungry(), Is.False);
    }
}
