namespace Parallelism.Philosophers;


public class DiningPhilosophers
{
    private readonly Philosopher[] _philosophers;
    private readonly Chopstick[] _chopsticks;
    private readonly IMealService _mealService;

    public DiningPhilosophers(Philosopher[] philosophers, Chopstick[] chopsticks, IMealService mealService)
    {
        if (philosophers.Length != chopsticks.Length)
        {
            throw new ArgumentException("Philosophers and chopsticks must have the same length");
        }

        _philosophers = philosophers;
        _chopsticks = chopsticks;
        _mealService = mealService;
    }

    public void Feast(CancellationToken ct = default)
    {
        var options = new ParallelOptions
        {
            CancellationToken = ct,
        };

        Parallel.ForEach(_philosophers, options, (p, state, i) =>
        {
            Feast(p, (int)i);
        });
    }

    private void Feast(Philosopher philosopher, int philosopherIndex)
    {
        var (chopstickA, chopstickB) = GetChopstickNumbersForPhilosopherIndex(philosopherIndex);

        lock (_chopsticks[chopstickA])
        lock (_chopsticks[chopstickB])
        {
            philosopher.Eat(_chopsticks[chopstickA], _chopsticks[chopstickB],
                _mealService.GetMeal());
        }
    }

    private (int chopstickA, int chopstickB) GetChopstickNumbersForPhilosopherIndex(int philosopherIndex)
    {
        var nextIndex = (philosopherIndex + 1) % _philosophers.Length;
        return philosopherIndex % 2 == 0
            ? (philosopherIndex, nextIndex)
            : (nextIndex, philosopherIndex);
    }
}
