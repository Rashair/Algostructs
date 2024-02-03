namespace Parallelism;

public class ParallelFizzBuzz
{
    private readonly int _n;
    private readonly object _lock;
    private readonly TextWriter _writer;

    private int _currentValue;

    public ParallelFizzBuzz(int n, TextWriter? writer = null)
    {
        _n = n;
        _lock = new();
        _writer = writer ?? Console.Out;
    }

    public async Task Execute(CancellationToken ct = default)
    {
        _currentValue = 1;
        var piecesOfWork = new Task[4];

        piecesOfWork[0] = Task.Run(Number, ct);
        piecesOfWork[1] = Task.Run(Fizz, ct);
        piecesOfWork[2] = Task.Run(Buzz, ct);
        piecesOfWork[3] = Task.Run(FizzBuzz, ct);

        await Task.WhenAll(piecesOfWork);
    }

    private void Number()
    {
        HandleNumber(i => i % 3 != 0 && i % 5 != 0, (v) => v.ToString());
    }

    private void Fizz()
    {
        HandleNumber(i => i % 3 == 0 && i % 5 != 0, (v) => "Fizz");
    }

    private void Buzz()
    {
        HandleNumber(i => i % 3 != 0 && i % 5 == 0, (v) => "Buzz");
    }

    private void FizzBuzz()
    {
        HandleNumber(i => i % 3 == 0 && i % 5 == 0, (v) => "FizzBuzz");
    }

    private void HandleNumber(Predicate<int> condition, Func<int, string> getStringToPrint)
    {
        while (_currentValue <= _n)
        {
            lock (_lock)
            {
                while (_currentValue <= _n && condition(_currentValue))
                {
                    _writer.WriteLine(getStringToPrint(_currentValue));
                    ++_currentValue;
                }
            }
        }
    }
}
