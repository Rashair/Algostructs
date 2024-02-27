namespace Messaging;

public interface IAsyncEventObserver<TEvent>
{
    Task OnNext(TEvent value);
    Task OnCompleted(int count);
}
