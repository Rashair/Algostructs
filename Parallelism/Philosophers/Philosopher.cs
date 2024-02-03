namespace Parallelism.Philosophers;

public class Philosopher
{
    private bool _isHungry;

    public Philosopher(bool isHungry = true)
    {
        _isHungry = isHungry;
    }

    public bool IsHungry()
    {
        return _isHungry;
    }

    public void Eat(ICutleryPiece leftChopstick, ICutleryPiece rightChopstick, IMeal meal)
    {
        _isHungry = false;
    }
}
