# ForkJoin Demo

This small C# console application demonstrates converting the following activity diagram into threaded code.

![diagram.png](diagram.png)

## To Execute

1. If needed, install the dotnet framework: https://dotnet.microsoft.com/download
2. Open a terminal.
3. Navigate to the ForkJoin project directory.
4. Execute the following command: `dotnet run`

## Output

```csharp
DO LAUNDRY ACTIVITY STARTING
Sorting Clothes Action
FORKING to wash White and Dark clothes concurrently, join to sort when both are finished
* WHITE Clothes Action Thread: STARTING, 5 cycles to complete
* WHITE Clothes Action Thread: Wash cycle 1 of 5
# DARK Clothes Action Thread: STARTING, 10 cycles to complete
# DARK Clothes Action Thread: Wash cycle 1 of 10
# DARK Clothes Action Thread: Wash cycle 2 of 10
* WHITE Clothes Action Thread: Wash cycle 2 of 5
# DARK Clothes Action Thread: Wash cycle 3 of 10
* WHITE Clothes Action Thread: Wash cycle 3 of 5
# DARK Clothes Action Thread: Wash cycle 4 of 10
* WHITE Clothes Action Thread: Wash cycle 4 of 5
# DARK Clothes Action Thread: Wash cycle 5 of 10
* WHITE Clothes Action Thread: Wash cycle 5 of 5
# DARK Clothes Action Thread: Wash cycle 6 of 10
* WHITE Clothes Action Thread: COMPLETE
# DARK Clothes Action Thread: Wash cycle 7 of 10
# DARK Clothes Action Thread: Wash cycle 8 of 10
# DARK Clothes Action Thread: Wash cycle 9 of 10
# DARK Clothes Action Thread: Wash cycle 10 of 10
# DARK Clothes Action Thread: COMPLETE
JOINED, all Washing Clothes Actions are complete
Drying clothes Action
Folding clothes Action
DO LAUNDRY ACTIVITY COMPLETE
```