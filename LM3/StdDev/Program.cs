using System.Diagnostics;

namespace StdDev;

public static class Program
{
    private static double[]? _standardDeviations;

    private static void Main()
    {
        var listOfArrays = CreateRandomListOfArrays();
        _standardDeviations = new double[listOfArrays.Count];

        //start all threads
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var threads = new Thread[listOfArrays.Count];
        for (var threadIdx = 0; threadIdx < threads.Length; threadIdx++)
        {
            //local copy necessary because closure would capture the last value of threadIdx
            var localThreadIdx = threadIdx;
            var newThread = new Thread(() => ComputeStandardDeviation(localThreadIdx, listOfArrays[localThreadIdx]));
            threads[threadIdx] = newThread;
            newThread.Start();
        }

        //join all threads
        foreach (var thread in threads) thread.Join();

        //show time to execute all threads
        stopWatch.Stop();
        Console.WriteLine($"\nAll threads finished in {stopWatch.ElapsedMilliseconds}ms");

        //show output
        Console.WriteLine($"\nStandard Deviations for {listOfArrays.Count} Lists of Integers:");
        for (var stdDevIdx = 0; stdDevIdx < listOfArrays.Count; stdDevIdx++)
            Console.WriteLine(
                $"{stdDevIdx + 1}) Elements: {listOfArrays[stdDevIdx].Length,9:n0}    " +
                $"StdDev: {_standardDeviations[stdDevIdx],15:0,0.000}"
            );
        
        //for fun, compare single-threaded time to multi-threaded time 
        //generally the multi-threaded time will be faster though
        //single-threaded time is generally faster on small lists
        RunSequentiallyForSpeedComparison(listOfArrays, stopWatch.ElapsedMilliseconds);
    }

    private static void RunSequentiallyForSpeedComparison(List<int[]> listOfArrays, long multiThreadedExecTimeMs)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        listOfArrays.ForEach((lst) => ComputeStandardDeviation(0, lst, false)); //sequential execution, ignore results
        stopWatch.Stop();

        //compare times
        Console.WriteLine($"\n{new string('=', 80)}\n");
        Console.WriteLine("Comparison of multi-threaded and single-threaded execution times...");
        Console.WriteLine($"- Multi-threaded execution in {multiThreadedExecTimeMs}ms");
        Console.WriteLine($"- Single threaded execution in {stopWatch.ElapsedMilliseconds}ms");
        var timeDiffMs = stopWatch.ElapsedMilliseconds - multiThreadedExecTimeMs;
        var wasFaster = timeDiffMs < 0 ? "faster" : "slower";
        Console.WriteLine($"- Single threaded was {Math.Abs(timeDiffMs)}ms {wasFaster}");
    }

    /// <summary>
    ///     Computes the standard deviation of a list of integers.
    /// </summary>
    /// <param name="threadIdx">Sets the position where the value will be written in <see cref="_standardDeviations" /></param>
    /// <param name="integers"></param>
    private static void ComputeStandardDeviation(int threadIdx, IReadOnlyCollection<int> integers, bool showOutput = true)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var mean = integers.Average();
        var sumOfSquaresOfDifferences = integers.Sum(val => Math.Pow(val - mean, 2));
        var standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / integers.Count);

        //no need to lock - each thread writes to a different position in the array
        _standardDeviations![threadIdx] = standardDeviation;

        stopWatch.Stop();
        if (showOutput)
           Console.WriteLine($"Thread {threadIdx + 1} finished in {stopWatch.ElapsedMilliseconds}ms");
    }

    /// <summary>
    ///     Generate list of arrays with random sizes and random values.
    /// </summary>
    private static List<int[]> CreateRandomListOfArrays()
    {
        var lists = new List<int[]>();
        var rand = new Random();
        var numLists = rand.Next(1, 10);
        for (var listIdx = 0; listIdx < numLists; listIdx++)
        {
            var arraySize = rand.Next(1, 1000000);
            var min = rand.Next(0, int.MaxValue);
            var max = rand.Next(min, int.MaxValue);
            var randomValues = Enumerable.Range(0, arraySize).Select(_ => rand.Next(min, max)).ToArray();
            lists.Add(randomValues);
        }

        return lists;
    }
}