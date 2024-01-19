// See https://aka.ms/new-console-template for more information

using AlgoEntry;
using DynamicProgramming;


Executor.RunUntilError(() =>
{
    Console.Write("N: ");
    var input = int.Parse(Console.ReadLine()!);
    Console.WriteLine(CoinsCounter.GetNumberOfWays(input));
});


Executor.RunUntilError(() =>
{
    Console.Write("N: ");
    var input = int.Parse(Console.ReadLine()!);
    ParensGenerator.PrintAllPossibleParens(input);
});



