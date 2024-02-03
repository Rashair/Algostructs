// See https://aka.ms/new-console-template for more information

using AlgoEntry;
using DynamicProgramming;
using Parallelism;


Executor.RunUntilError(() =>
{
    Console.Write("N: ");
    var input = int.Parse(Console.ReadLine()!);
    Executor.MeasureRuntime(() =>
    {
        var fb = new ParallelFizzBuzz(input);
        fb.Execute().Wait();
    });
});




