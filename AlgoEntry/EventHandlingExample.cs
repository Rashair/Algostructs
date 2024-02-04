using Messaging;

namespace AlgoEntry;

public class EventHandlingExample
{
    public static void Play()
    {
        var processor = new EventProcessor<int>();
        for (int i = 0; i < 5; ++i)
        {
            var observer = new EventObserver<int>(i);
            processor.Subscribe(observer);
        }

        for (int j = 0; j < 100; j++)
        {
            processor.OnEvent(j);
        }

        processor.OnCompleted();
    }
}
