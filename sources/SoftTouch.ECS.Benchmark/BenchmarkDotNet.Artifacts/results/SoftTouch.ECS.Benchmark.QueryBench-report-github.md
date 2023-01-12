``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.101
  [Host]     : .NET Core 7.0.1 (CoreCLR 7.0.122.56804, CoreFX 7.0.122.56804), X64 RyuJIT
  Job-FFVDUL : .NET Core 7.0.1 (CoreCLR 7.0.122.56804, CoreFX 7.0.122.56804), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|          Method |     Mean |     Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |---------:|----------:|----------:|---------:|-------:|------:|------:|----------:|
| QueryArchetypes | 1.257 μs | 0.0785 μs | 0.1475 μs | 1.247 μs | 0.1564 |     - |     - |     328 B |
| QueryComponents | 7.471 μs | 0.2431 μs | 0.4626 μs | 7.280 μs | 2.3499 |     - |     - |    4840 B |
