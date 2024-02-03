namespace Parallelism.Philosophers;


public class DiningPhilosophers
{
    private readonly Philosopher[] _philosophers;
    private readonly Chopstick[] _chopsticks;
    private readonly IMealService _mealService;


    public DiningPhilosophers(Philosopher[] philosophers, Chopstick[] chopsticks, IMealService mealService)
    {
        _philosophers = philosophers;
        _chopsticks = chopsticks;
        _mealService = mealService;
    }

    public void Feast(CancellationToken ct = default)
    {

    }
}
