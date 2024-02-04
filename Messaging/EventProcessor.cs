namespace Messaging;

public class EventProcessor<TEvent>
{
    private readonly HashSet<IAsyncEventObserver<TEvent>> _observers;
    private readonly HashSet<TEvent> _events;

    public EventProcessor()
    {
        _observers = [];
        _events = [];
    }

    public async Task<IDisposable> Subscribe(IAsyncEventObserver<TEvent> observer)
    {
        if (_observers.Add(observer))
        {
            // Provide observer with existing data.
            foreach (var ev in _events)
            {
                await observer.OnNext(ev);
            }
        }

        return new Unsubscriber<TEvent>(_observers, observer);
    }

    public Task OnEvent(TEvent @event)
    {
        _events.Add(@event);
        return Task.WhenAll(_observers.Select(o => o.OnNext(@event)));
    }

    public async Task OnCompleted(int count = 0)
    {
         await Task.WhenAll(_observers.Select(o => o.OnCompleted(count)));
        _observers.Clear();
    }
}

internal sealed class Unsubscriber<TEvent> : IDisposable
{
    private readonly ISet<IAsyncEventObserver<TEvent>> _observers;
    private readonly IAsyncEventObserver<TEvent> _observer;

    internal Unsubscriber(
        ISet<IAsyncEventObserver<TEvent>> observers,
        IAsyncEventObserver<TEvent> observer) => (_observers, _observer) = (observers, observer);

    public void Dispose() => _observers.Remove(_observer);
}
