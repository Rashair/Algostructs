namespace Messaging;

public class EventObserver<TEvent> : IObserver<TEvent>
{
    private readonly Random _random;
    private readonly int _number;
    private readonly string _name;
    private readonly TextWriter _output;

    public EventObserver(int? number = null, TextWriter? output = null)
    {
        _random = new();
        _number = number ?? Random.Shared.Next(100);
        _name = $"[O{number}]";
        _output = output ?? Console.Out;
    }

    public void OnCompleted()
    {
        _output.WriteLine($"{_name} Completed!");
    }

    public void OnError(Exception error)
    {
        _output.WriteLine($"{_name} Error: {error.Message}");
    }

    public void OnNext(TEvent value)
    {
        _output.WriteLine($"{_name} Next: {value}");
        Thread.Sleep(_random.Next(100 / (_number + 1)));
    }
}
