``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.403
  [Host]     : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT
  Job-ZHTRHW : .NET Core 7.0.13 (CoreCLR 7.0.1323.51816, CoreFX 7.0.1323.51816), X64 RyuJIT

IterationCount=5  LaunchCount=1  WarmupCount=5  

```
|                        Method |     Mean |     Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |---------:|----------:|---------:|------:|------:|------:|----------:|
|          SingleParallelUpdate | 621.7 μs |  15.20 μs |  2.35 μs |     - |     - |     - |       1 B |
| SingleParallelUpdateNoWUpdate | 632.4 μs |  37.31 μs |  5.77 μs |     - |     - |     - |       1 B |
|              SingleSyncUpdate | 716.0 μs | 301.41 μs | 78.28 μs |     - |     - |     - |         - |
