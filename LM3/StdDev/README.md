# Multi-threaded Standard Deviation Calculator

Generates random lists of integers and calculates the standard deviation of each list using 
multiple threads.

## To Execute

1. If needed, install the dotnet framework: https://dotnet.microsoft.com/download
2. Open a terminal.
3. Navigate to the StdDev project directory.
4. Execute the following command: `dotnet run`

## Sample Output

```text
Thread 2 finished in 7ms
Thread 3 finished in 8ms
Thread 1 finished in 19ms
Thread 7 finished in 25ms
Thread 6 finished in 32ms
Thread 8 finished in 37ms
Thread 4 finished in 50ms
Thread 5 finished in 52ms
Thread 9 finished in 55ms

All threads finished in 58ms

Standard Deviations for 9 Lists of Integers:
1) Elements:   190,790    StdDev:  12,116,243.877![img.png](img.png)
2) Elements:     5,196    StdDev:  60,123,001.614
3) Elements:    35,673    StdDev: 212,130,078.320
4) Elements:   876,235    StdDev:  61,083,062.252
5) Elements:   817,607    StdDev: 479,193,458.847
6) Elements:   425,374    StdDev:  80,265,886.099
7) Elements:   298,239    StdDev: 224,877,771.677
8) Elements:   466,414    StdDev:  28,367,268.533
9) Elements:   814,898    StdDev: 115,124,334.962
```

## Activity Diagram

[![diagram.png](diagram.png)](http://www.plantuml.com/plantuml/umla/dP8_Rzim4CLtVef35jijJDcReyP17BiedBJfe245N0sqHClNY7z0dYouHRzxzKTbZNOe40uSxhttkmU2Tvw4eqgLRBGnMQMa9a5eD1iTYQoFeyYMGhS6dXaLYOpmX7IUvYWA9zHqBqamDlgiIzGItkT8nlNP9bhNDR-C1rQKDvU84lFyijsgOfNGfdgwNpsu-m_VFcwKNU_N3_jFwX6_B6oINv_eUc4VSRvzNimOwnepsrpODeMhXFADF7am1tY2hIPPI0ShvyJPNy4F1is9KTkAOYZHqmrgsiqvVvFUGjghlYF4S2YDe5rhuXw_otJG3yQzC1XEl3QgOKderxOYTFJTB7qNN0EocIrxt_YtPt811L48rEo_PCvvO0TqCmpmO7iLNlyBrt-XTG1BdVzva1YNitvS7x6KC4QuwQ3z0qu0FOpFqay2FpcQUcZmLRSLXeLuqG5X2GXlOHusTlH1dzvcHjbIakmJdenbFzcTrEsl-WK0)

## PlantUML

[Click here to edit diagram in PlantUML.](http://www.plantuml.com/plantuml/umla/dP8_Rzim4CLtVef35jijJDcReyP17BiedBJfe245N0sqHClNY7z0dYouHRzxzKTbZNOe40uSxhttkmU2Tvw4eqgLRBGnMQMa9a5eD1iTYQoFeyYMGhS6dXaLYOpmX7IUvYWA9zHqBqamDlgiIzGItkT8nlNP9bhNDR-C1rQKDvU84lFyijsgOfNGfdgwNpsu-m_VFcwKNU_N3_jFwX6_B6oINv_eUc4VSRvzNimOwnepsrpODeMhXFADF7am1tY2hIPPI0ShvyJPNy4F1is9KTkAOYZHqmrgsiqvVvFUGjghlYF4S2YDe5rhuXw_otJG3yQzC1XEl3QgOKderxOYTFJTB7qNN0EocIrxt_YtPt811L48rEo_PCvvO0TqCmpmO7iLNlyBrt-XTG1BdVzva1YNitvS7x6KC4QuwQ3z0qu0FOpFqay2FpcQUcZmLRSLXeLuqG5X2GXlOHusTlH1dzvcHjbIakmJdenbFzcTrEsl-WK0)