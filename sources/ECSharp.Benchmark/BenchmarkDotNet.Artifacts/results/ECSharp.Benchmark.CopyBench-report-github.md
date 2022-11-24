``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.51805, CoreFX 7.0.22.51805), X64 RyuJIT
  Job-JVZJDM : .NET Core 7.0.0 (CoreCLR 7.0.22.51805, CoreFX 7.0.22.51805), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|           Method |     Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|----------:|----------:|-------:|-------:|------:|----------:|
| CopyRangeObjects | 1.804 μs | 0.0626 μs | 0.1096 μs | 0.0572 | 0.0267 |     - |     368 B |
| CopyRangeStructs |       NA |        NA |        NA |      - |      - |     - |         - |

Benchmarks with issues:
  CopyBench.CopyRangeStructs: Job-JVZJDM(IterationCount=15, LaunchCount=3, WarmupCount=10)
