namespace Messaging;

public class EventProcessor<TEvent> : IObservable<TEvent>
{
    private readonly HashSet<IObserver<TEvent>> _observers;
    private readonly HashSet<TEvent> _events;

    public EventProcessor()
    {
        _observers = [];
        _events = [];
    }

    public IDisposable Subscribe(IObserver<TEvent> observer)
    {
        if (_observers.Add(observer))
        {
            // Provide observer with existing data.
            foreach (var item in _events)
            {
                observer.OnNext(item);
            }
        }

        return new Unsubscriber<TEvent>(_observers, observer);
    }

    public void OnEvent(TEvent @event)
    {
        _events.Add(@event);
        Parallel.ForEach(_observers, o => o.OnNext(@event));
    }

    public void OnCompleted()
    {
        Parallel.ForEach(_observers, o => o.OnCompleted());
        _observers.Clear();
    }
}

internal sealed class Unsubscriber<TEvent> : IDisposable
{
    private readonly ISet<IObserver<TEvent>> _observers;
    private readonly IObserver<TEvent> _observer;

    internal Unsubscriber(
        ISet<IObserver<TEvent>> observers,
        IObserver<TEvent> observer) => (_observers, _observer) = (observers, observer);

    public void Dispose() => _observers.Remove(_observer);
}
