namespace Parallelism;

public class ParallelFizzBuzz
{
    public const int MaxDelayMs = 10;

    private readonly int _n;
    private readonly object _lock = new();
    private int _currentValue = 1;
    private readonly TextWriter _writer;

    public ParallelFizzBuzz(int n, TextWriter? writer = null)
    {
        _n = n;
        _writer = writer ?? Console.Out;
    }

    public async Task Execute(CancellationToken ct = default)
    {
        _currentValue = 1;
        var piecesOfWork = new Task[4];

        piecesOfWork[0] = Task.Run(() => Number(), ct);
        piecesOfWork[1] = Task.Run(() => Fizz(), ct);
        piecesOfWork[2] = Task.Run(() => Buzz(), ct);
        piecesOfWork[3] = Task.Run(() => FizzBuzz(), ct);

        await Task.WhenAll(piecesOfWork);
    }

    private Task Number()
    {
        throw new NotImplementedException();
    }

    private Task Fizz()
    {
        throw new NotImplementedException();
    }

    private Task Buzz()
    {
        throw new NotImplementedException();
    }

    private Task FizzBuzz()
    {
        throw new NotImplementedException();
    }
}
