using NUnit.Framework;
using Parallelism.Philosophers;

namespace Parallelism.Tests.Philosophers;

[TestFixture]
[TestOf(typeof(DiningPhilosophers))]
public class DiningPhilosophersTests
{

    [Test]
    [Timeout(1000)]
    public void WhenFeastCompletes_ThenPhilosophersAreNotHungry()
    {
        // Arrange
        var philosophers = new Philosopher[]
        {
            new Philosopher()
        };
        var chopsticks = new Chopstick[]
        {
            new Chopstick()
        };
        var mealService = new MealService();

        var diningPhilosophers = new DiningPhilosophers(philosophers, chopsticks, mealService);

        // Act
        diningPhilosophers.Feast();

        // Assert
        Assert.That(philosophers.All(p => !p.IsHungry()));
    }
}
