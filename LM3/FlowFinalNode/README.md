# FlowFinalNode Demo

This small C# console application demonstrates converting the following activity diagram into threaded code.

![diagram.png](diagram.png)

## To Execute

1. If needed, install the dotnet framework: https://dotnet.microsoft.com/download
2. Open a terminal.
3. Navigate to the ForkJoin project directory.
4. Execute the following command: `dotnet run`

## Output

```csharp
SOLUTION SEARCH ACTIVITY STARTING
Post Problem Action
FORKING 3 threads to search for solution
3: Search Thread 3: STARTING, SEARCHING FOR A VALUE GREATER THAN 0.98
1: Search Thread 1: STARTING, SEARCHING FOR A VALUE GREATER THAN 0.98
2: Search Thread 2: STARTING, SEARCHING FOR A VALUE GREATER THAN 0.98
2: Search Thread 2: Search iteration 1, found: 0.7786502887121685, :(
3: Search Thread 3: Search iteration 1, found: 0.057402904737136495, :(
1: Search Thread 1: Search iteration 1, found: 0.5208862541641001, :(
2: Search Thread 2: Search iteration 2, found: 0.8453726510373731, :(
1: Search Thread 1: Search iteration 2, found: 0.8188097772598439, :(
3: Search Thread 3: Search iteration 2, found: 0.2457516867365528, :(
2: Search Thread 2: Search iteration 3, found: 0.24723117044009624, :(
3: Search Thread 3: Search iteration 3, found: 0.4604258725247352, :(
1: Search Thread 1: Search iteration 3, found: 0.45920239397121243, :(
2: Search Thread 2: Search iteration 4, found: 0.7295405690705945, :(
3: Search Thread 3: Search iteration 4, found: 0.9283785532384793, :(
1: Search Thread 1: Search iteration 4, found: 0.09274044799851033, :(
2: Search Thread 2: Search iteration 5, found: 0.2001136773476493, :(
1: Search Thread 1: Search iteration 5, found: 0.019400856537558075, :(
3: Search Thread 3: Search iteration 5, found: 0.19075865783972257, :(
3: Search Thread 3: Search iteration 6, found: 0.11280188897829357, :(
2: Search Thread 2: Search iteration 6, found: 0.37288848802911323, :(
1: Search Thread 1: Search iteration 6, found: 0.7647365395522935, :(
3: Search Thread 3: Search iteration 7, found: 0.05807594331930066, :(
2: Search Thread 2: Search iteration 7, found: 0.7913239549586616, :(
2: Search Thread 2: Search iteration 8, found: 0.9179529804222597, :(
1: Search Thread 1: Search iteration 7, found: 0.05722876723751624, :(
3: Search Thread 3: Search iteration 8, found: 0.057724466888279924, :(
2: Search Thread 2: Search iteration 9, found: 0.03327519025942682, :(
1: Search Thread 1: Search iteration 8, found: 0.25176183452549783, :(
3: Search Thread 3: Search iteration 9, found: 0.7683138022728685, :(
2: Search Thread 2: Search iteration 10, found: 0.5443985368521728, :(
1: Search Thread 1: Search iteration 9, found: 0.99651608723177, :)
1: Search Thread 1: SOLUTION FOUND! 0.99651608723177, I WON! :)
1: Search Thread 1: COMPLETE
3: Search Thread 3: Search iteration 10, found: 0.27363166998929855, :(
3: Search Thread 3: SOLUTION FOUND BY OTHER THREAD, I AM STOPPING!
3: Search Thread 3: COMPLETE
2: Search Thread 2: Search iteration 11, found: 0.9503919725414454, :(
2: Search Thread 2: SOLUTION FOUND BY OTHER THREAD, I AM STOPPING!
2: Search Thread 2: COMPLETE
JOINED, all Search Threads are complete
Select Solution Action: Search Value is: 0.99651608723177
SOLUTION SEARCH ACTIVITY COMPLETE
* Found in 30 total iterations over 3 threads.
* Execution time: 00:00:06.8249554
```