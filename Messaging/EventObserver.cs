namespace Messaging;

public class EventObserver<TEvent> : IAsyncEventObserver<TEvent>
{
    private readonly Random _random;
    private readonly int _number;
    private readonly string _name;
    private readonly TextWriter _output;
    private int _eventsCount;

    public EventObserver(int? number = null, TextWriter? output = null)
    {
        _random = new();
        _number = number ?? Random.Shared.Next(100);
        _name = $"[O{number}]";
        _output = output ?? Console.Out;
    }

    public async Task OnCompleted(int count = 0)
    {
        while (_eventsCount < count)
        {
            await Task.Delay(100);
        }

        _output.WriteLine($"{_name} Completed {_eventsCount}!");
    }

    public void OnError(Exception error)
    {
        _output.WriteLine($"{_name} Error: {error.Message}");
    }

    public async Task OnNext(TEvent value)
    {
        await _output.WriteLineAsync($"{_name} Next: {value}");
        await Task.Delay(10_000 / (_number + 1));
        Interlocked.Increment(ref _eventsCount);
    }
}
