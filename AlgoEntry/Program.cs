// See https://aka.ms/new-console-template for more information

using AlgoEntry;
using Messaging;


var processor = new MessageProcessor<int>();

for (int j = 0; j < 1_000; j++)
{
    processor.OnMessage(j);
    await Task.Delay(10);
}


await processor.StopProcessing();
