namespace Parallelism.Philosophers;

public class Philosopher
{
    private bool _isHungry;

    public Philosopher()
    {
        _isHungry = true;
    }

    public bool IsHungry()
    {
        return _isHungry;
    }

    public void Eat (ICutleryPiece leftChopstick, ICutleryPiece rightChopstick, IMeal meal)
    {
        _isHungry = false;
    }
}


