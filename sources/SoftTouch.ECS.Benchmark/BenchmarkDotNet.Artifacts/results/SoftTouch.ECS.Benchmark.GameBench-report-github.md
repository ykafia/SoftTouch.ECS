``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.403
  [Host]     : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT
  Job-RYOIPD : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT

IterationCount=5  LaunchCount=1  WarmupCount=5  

```
|               Method |       Mean |      Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|-----------:|----------:|-------:|------:|------:|----------:|
| SingleParallelUpdate |   696.9 μs |   188.4 μs |  29.16 μs |      - |     - |     - |   1.89 KB |
|    TenParallelUpdate | 8,202.9 μs | 1,841.7 μs | 478.28 μs | 7.8125 |     - |     - |  18.75 KB |
