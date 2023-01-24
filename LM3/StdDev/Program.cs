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
        Console.WriteLine($"\nAll threads finished in {stopWatch.ElapsedMilliseconds}ms");

        //show output
        Console.WriteLine($"\nStandard Deviations for {listOfArrays.Count} Lists of Integers:");
        for (var stdDevIdx = 0; stdDevIdx < listOfArrays.Count; stdDevIdx++)
            Console.WriteLine(
                $"{stdDevIdx + 1}) Elements: {listOfArrays[stdDevIdx].Length,9:n0}    " +
                $"StdDev: {_standardDeviations[stdDevIdx],15:0,0.000}"
            );
    }

    /// <summary>
    ///     Computes the standard deviation of a list of integers.
    /// </summary>
    /// <param name="threadIdx">Sets the position where the value will be written in <see cref="_standardDeviations" /></param>
    /// <param name="integers"></param>
    private static void ComputeStandardDeviation(int threadIdx, IReadOnlyCollection<int> integers)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var mean = integers.Average();
        var sumOfSquaresOfDifferences = integers.Sum(val => Math.Pow(val - mean, 2));
        var standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / integers.Count);

        //no need to lock - each thread writes to a different position in the array
        _standardDeviations![threadIdx] = standardDeviation;

        stopWatch.Stop();
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