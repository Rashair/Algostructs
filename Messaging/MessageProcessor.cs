using System.Threading.Channels;

namespace Messaging;

public class MessageProcessor<TMessage> : IAsyncDisposable
{
    private readonly TextWriter _outputStream;

    private readonly CancellationTokenSource _cts;
    private readonly Channel<TMessage> _channel;
    private readonly Task _processingTask;

    public MessageProcessor(TextWriter? output = null)
    {
        _outputStream = output ?? Console.Out;
        _cts = new();
        _channel = Channel.CreateUnbounded<TMessage>(new());
        _processingTask = Task.Run(() => ProcessMessages(_cts.Token));
    }

    public async Task OnMessage(TMessage message)
    {
        await _channel.Writer.WriteAsync(@message, _cts.Token);
    }

    private async Task ProcessMessages(CancellationToken ct)
    {
        while (await _channel.Reader.WaitToReadAsync(ct))
        {
            while (_channel.Reader.TryRead(out var @event))
            {
                // Simulate processing time
                await Task.Delay(100, ct);
                _outputStream.WriteLine(@event);
            }
        }
    }

    public async Task StopProcessing()
    {
        _channel.Writer.Complete();
        await _processingTask;
        await _cts.CancelAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await StopProcessing();
        _cts.Dispose();
        _processingTask.Dispose();
    }
}
