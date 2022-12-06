``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.400
  [Host]     : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT
  Job-WZEXSG : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|            Method |           Mean |         Error |        StdDev |         Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |---------------:|--------------:|--------------:|---------------:|-------:|------:|------:|----------:|
|         DoNothing |      0.1054 ns |     0.0635 ns |     0.1192 ns |      0.0756 ns |      - |     - |     - |         - |
|       IterForeach | 34,009.0063 ns | 2,480.3687 ns | 4,719.1593 ns | 34,899.8947 ns | 5.0659 |     - |     - |   10640 B |
| IterForEachToList | 35,129.9227 ns | 2,529.1609 ns | 4,750.3773 ns | 35,098.6137 ns | 5.6152 |     - |     - |   11784 B |
|     IterForToList | 30,408.9628 ns | 1,773.1556 ns | 3,151.7822 ns | 29,207.7972 ns | 5.6152 |     - |     - |   11784 B |
