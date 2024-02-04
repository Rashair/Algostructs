using Messaging;

namespace AlgoEntry;

public class EventHandlingExample
{
    public static async Task Play()
    {
        var processor = new EventProcessor<int>();
        for (int i = 0; i < 5; ++i)
        {
            var observer = new EventObserver<int>(i);
            await processor.Subscribe(observer);
        }

        var tasks = new Task[50];
        for (int i = 0; i < tasks.Length; ++i)
        {
            tasks[i] = processor.OnEvent(i);
        }

        await processor.OnCompleted(50);
    }
}
