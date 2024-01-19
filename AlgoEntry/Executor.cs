namespace AlgoEntry;

public class Executor
{
    public static void RunUntilError(Action action)
    {
        try
        {
            while (true)
            {
                action();
            }
        }
        catch
        {
            Console.WriteLine("Exited.");
            // do nothing
        }
    }
}
