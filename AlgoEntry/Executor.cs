using System.Diagnostics;

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
        catch (Exception ex)
        {
            Console.WriteLine("Exited: " + ex.Message);
        }
    }

    public static void MeasureRuntime(Action action)
    {
        var st = Stopwatch.StartNew();
        action();
        Console.WriteLine($"Took {st.ElapsedMilliseconds}ms");
    }
}
