namespace ForkJoin;

/// <summary>
/// C# console application to demonstrate the use of the forking and joining.
/// Demonstrates converting a concurrent activity diagram into threaded code.
/// </summary>
public static class Program
{
    private static Thread? _washWhiteClothesThread;
    private static Thread? _washDarkClothesThread;

    public static void Main()
    {
        Console.WriteLine("DO LAUNDRY ACTIVITY STARTING");

        Console.WriteLine("Sorting Clothes Action");

        //start concurrent operation
        Console.WriteLine("FORKING to wash White and Dark clothes concurrently, join to sort when both are finished");

        //start washing white clothes - fork thread to execute
        _washWhiteClothesThread = new Thread(WashClothesAction)
        {
            Name = "* WHITE Clothes Action Thread"
        };
        _washWhiteClothesThread.Start();

        //start washing dark clothes - 
        _washDarkClothesThread = new Thread(WashClothesAction)
        {
            Name = "# DARK Clothes Action Thread"
        };
        _washDarkClothesThread.Start();

        //join threads back into main flow
        _washWhiteClothesThread.Join();
        _washDarkClothesThread.Join();
        //at this point, the main thread is waiting on both white and dark clothes threads to finish

        //concurrent operation complete, resume main process
        Console.WriteLine("JOINED, all Washing Clothes Actions are complete");

        Console.WriteLine("Drying clothes Action");

        Console.WriteLine("Folding clothes Action");

        Console.WriteLine("DO LAUNDRY ACTIVITY COMPLETE");
    }

    private static void WashClothesAction()
    {
        //if white clothes, 5 cycles, otherwise 10
        var runsForCycles = Thread.CurrentThread.Name!.Contains("white", StringComparison.OrdinalIgnoreCase) 
            ? 5 
            : 10;
        Console.WriteLine($"{Thread.CurrentThread.Name}: STARTING, {runsForCycles} cycles to complete");

        for (var i = 1; i <= runsForCycles; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: Wash cycle {i} of {runsForCycles}");
            Thread.Sleep(2000);
        }

        Console.WriteLine($"{Thread.CurrentThread.Name}: COMPLETE");
    }
}