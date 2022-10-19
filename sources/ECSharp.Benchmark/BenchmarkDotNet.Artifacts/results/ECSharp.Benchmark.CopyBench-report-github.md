``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.400
  [Host]     : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT
  Job-UUOJII : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|     Method |     Mean |    Error |   StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------- |---------:|---------:|---------:|---------:|------:|------:|------:|----------:|
| CopyToList |       NA |       NA |       NA |       NA |     - |     - |     - |         - |
|  CopyRange | 117.9 ns | 39.91 ns | 71.97 ns | 77.97 ns |     - |     - |     - |         - |
|   CopySpan |       NA |       NA |       NA |       NA |     - |     - |     - |         - |

Benchmarks with issues:
  CopyBench.CopyToList: Job-UUOJII(IterationCount=15, LaunchCount=3, WarmupCount=10)
  CopyBench.CopySpan: Job-UUOJII(IterationCount=15, LaunchCount=3, WarmupCount=10)
