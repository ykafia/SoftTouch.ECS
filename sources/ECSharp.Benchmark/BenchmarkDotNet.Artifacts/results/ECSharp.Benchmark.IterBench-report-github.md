``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.400
  [Host] : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|            Method | Mean | Error |
|------------------ |-----:|------:|
|       IterForeach |   NA |    NA |
| IterForEachToList |   NA |    NA |
|     IterForToList |   NA |    NA |

Benchmarks with issues:
  IterBench.IterForeach: Job-CNDBPX(IterationCount=15, LaunchCount=3, WarmupCount=10)
  IterBench.IterForEachToList: Job-CNDBPX(IterationCount=15, LaunchCount=3, WarmupCount=10)
  IterBench.IterForToList: Job-CNDBPX(IterationCount=15, LaunchCount=3, WarmupCount=10)
