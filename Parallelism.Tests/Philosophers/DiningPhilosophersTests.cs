using NUnit.Framework;
using Parallelism.Philosophers;

namespace Parallelism.Tests.Philosophers;

[TestFixture]
[TestOf(typeof(DiningPhilosophers))]
public class DiningPhilosophersTests
{

    [Test]
    public async Task WhenFeastCompletes_ThenPhilosophersAreNotHungry()
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
        await RunTaskWithCancellation(() => diningPhilosophers.Feast());

        // Assert
        Assert.That(philosophers.All(p => !p.IsHungry()));
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Feast_ShouldComplete(CancellationToken ct)
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
        await RunTaskWithCancellation(() => diningPhilosophers.Feast(ct));
    }

    [Test]
    public async Task Feast_ShouldBeCancellable()
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

        var source = new CancellationTokenSource();
        await source.CancelAsync();

        // Act
        var ex = Assert.ThrowsAsync<OperationCanceledException>(async () => await RunTaskWithCancellation(() => diningPhilosophers.Feast(source.Token)));
        Assert.That(ex, Is.Not.Null);
    }

    private async Task RunTaskWithCancellation(Action action)
    {
        await Task.Run(action, CreateEmergencyCancellationToken());
    }

    // Used to cancel the task in case of deadlock
    private CancellationToken CreateEmergencyCancellationToken()
    {
        return new CancellationTokenSource(1_000).Token;
    }
}
