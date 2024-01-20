// See https://aka.ms/new-console-template for more information

using AlgoEntry;
using DynamicProgramming;


// Executor.RunUntilError(() =>
// {
//     Console.Write("N: ");
//     var input = int.Parse(Console.ReadLine()!);
//     int ways = 0;
//     Executor.MeasureRuntime(() =>
//     {
//         ways = CoinsCounter.GetNumberOfWays(input);
//     });
//     Console.WriteLine(ways);
// });


Executor.RunUntilError(() =>
{
    Console.Write("N: ");
    var input = int.Parse(Console.ReadLine()!);
    ParensGenerator.PrintAllPossibleParens(input);
});



