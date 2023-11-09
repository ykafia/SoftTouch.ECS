``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.403
  [Host]     : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT
  Job-TECXTQ : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|               Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
| SingleParallelUpdate | 529.7 μs | 17.51 μs | 33.32 μs | 0.9766 |     - |     - |    3856 B |
|     SingleSyncUpdate | 718.0 μs | 14.17 μs | 26.27 μs |      - |     - |     - |     185 B |
