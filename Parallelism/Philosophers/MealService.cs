namespace Parallelism.Philosophers;

public interface IMealService
{
    IMeal GetMeal();
}

public class MealService : IMealService
{
    public IMeal GetMeal()
    {
        return new Noddles();
    }
}
