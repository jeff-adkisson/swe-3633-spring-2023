using System.Diagnostics;

namespace FlowFinalNode;

/// <summary>
/// C# console application to demonstrate the use of flow final node concept.
/// Demonstrates converting a concurrent activity diagram into threaded code.
/// </summary>
public static class Program
{
    //number of search threads
    private const int SearchThreadCount = 3;
    
    //random search value we want has a simulated probability greater than this...
    private const double StopWhenRandomValueIsGreaterThan = 0.98d;
    
    //threads, synchronization, counters, and stopwatch
    private static Thread[]? _solutionSearchThreads;
    private static double  _solutionFoundValue = Double.NaN; //search until this shared variable is no longer NaN
    private static readonly object SolutionFoundLock = new object();
    private static int _overallSearchIterationCount = 0;
    private static Stopwatch _stopwatch = new Stopwatch();
    
    /// <summary>
    /// Starts <see cref="SearchThreadCount"/> threads to search for a solution.
    /// The "solution" is simply a random number greater than <see cref="StopWhenRandomValueIsGreaterThan"/>.
    /// Once a solution is found, the threads join back into the main flow to
    /// present the solution.
    /// </summary>
    public static void Main()
    {
        Console.WriteLine("SOLUTION SEARCH ACTIVITY STARTING");
        _stopwatch.Start();

        Console.WriteLine("Post Problem Action");

        //start concurrent operation
        Console.WriteLine($"FORKING {SearchThreadCount} threads to search for solution");
        
        //make threads and start them
        _solutionSearchThreads = new Thread[SearchThreadCount];
        for (int i = 0; i < SearchThreadCount; i++)
        {
            _solutionSearchThreads[i] = new Thread(PerformSearch)
            {
                Name = $"{i + 1}: Search Thread {i + 1}"
            };
            _solutionSearchThreads[i].Start();
        }

        //join threads back into main flow
        foreach (var t in _solutionSearchThreads)
        {
            t.Join();
        }
        //at this point, the main thread is waiting on all search threads to finish

        //concurrent operation complete, resume main process
        Console.WriteLine("JOINED, all Search Threads are complete");

        Console.WriteLine($"Select Solution Action: Search Value is: {_solutionFoundValue}");
        
        Console.WriteLine("SOLUTION SEARCH ACTIVITY COMPLETE");
        _stopwatch.Stop();
        Console.WriteLine($"* Found in {_overallSearchIterationCount} total iterations over {SearchThreadCount} threads.");
        Console.WriteLine($"* Execution time: {_stopwatch.Elapsed}");
    }

    /// <summary>
    /// Loops until a random value greater than <see cref="StopWhenRandomValueIsGreaterThan"/>
    /// is found. If found, the value is stored in <see cref="_solutionFoundValue"/> and
    /// execution stops. All threads will be joined back into the main flow when they
    /// note that <see cref="_solutionFoundValue"/> is no longer <see cref="Double.NaN"/>.
    /// </summary>
    private static void PerformSearch()
    {
        var rnd = new Random();
        Console.WriteLine($"{Thread.CurrentThread.Name}: STARTING, SEARCHING FOR A VALUE GREATER THAN {StopWhenRandomValueIsGreaterThan}");

        var iterationCount = 0;
        double searchValue;
        bool foundSearchValue;
        
        do
        {
            lock (SolutionFoundLock) _overallSearchIterationCount += 1;
            if (double.IsNaN(_solutionFoundValue)) 
                Thread.Sleep(rnd.Next(250, 1000)); //simulate search effort
            
            searchValue = rnd.NextDouble();
            foundSearchValue = IsSearchValueFound(searchValue);
            Console.WriteLine($"{Thread.CurrentThread.Name}: Search iteration {++iterationCount}, " +
                              $"found: {searchValue}, " +
                              $"{(foundSearchValue ? ":)" : ":(")}");
            
        } while (double.IsNaN(_solutionFoundValue) && !foundSearchValue);

        lock (SolutionFoundLock)
        {
            if (double.IsNaN(_solutionFoundValue) && foundSearchValue)
            {
                _solutionFoundValue = searchValue;
                Console.WriteLine($"{Thread.CurrentThread.Name}: SOLUTION FOUND! {_solutionFoundValue}, I WON! :)");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: SOLUTION FOUND BY OTHER THREAD, I AM STOPPING!");
            }
            
        }
        
        Console.WriteLine($"{Thread.CurrentThread.Name}: COMPLETE");
    }

    /// <summary>
    /// Returns true when the search value is greater than the
    /// <see cref="StopWhenRandomValueIsGreaterThan"/> search stop threshold.
    /// </summary>
    /// <param name="searchValue"></param>
    /// <returns></returns>
    private static bool IsSearchValueFound(double searchValue) 
        => searchValue > StopWhenRandomValueIsGreaterThan;
}