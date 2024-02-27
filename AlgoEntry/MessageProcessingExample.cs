using Messaging;

namespace AlgoEntry;

public class MessageProcessingExample
{
    public static async Task Play()
    {
        var processor = new MessageProcessor<int>();

        for (int j = 0; j < 1_000; j++)
        {
            processor.OnMessage(j);
            await Task.Delay(10);
        }

        await processor.StopProcessing();
    }
}
