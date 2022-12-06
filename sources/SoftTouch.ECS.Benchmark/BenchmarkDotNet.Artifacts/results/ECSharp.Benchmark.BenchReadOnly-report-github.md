``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.400
  [Host]     : .NET Core 6.0.9 (CoreCLR 6.0.922.41905, CoreFX 6.0.922.41905), X64 RyuJIT
  Job-PJPEJZ : .NET Core 6.0.9 (CoreCLR 6.0.922.41905, CoreFX 6.0.922.41905), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|               Method |     Mean |    Error |   StdDev |    Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------:|---------:|---------:|----------:|-------:|------:|------:|----------:|
|      BenchEnumerable | 139.6 ns |  6.26 ns | 11.60 ns | 135.21 ns | 0.0763 |     - |     - |     160 B |
|       BenchArchetype | 102.7 ns |  1.80 ns |  3.34 ns | 102.74 ns | 0.0459 |     - |     - |      96 B |
|    BenchEnumerableRo | 145.1 ns | 11.40 ns | 21.41 ns | 138.51 ns | 0.0763 |     - |     - |     160 B |
|     BenchArchetypeRo | 125.3 ns | 11.14 ns | 20.92 ns | 119.27 ns | 0.0459 |     - |     - |      96 B |
|   BenchArchetypeSpan | 102.4 ns |  1.72 ns |  3.11 ns | 102.26 ns | 0.0459 |     - |     - |      96 B |
| BenchArchetypeRoSpan | 107.8 ns | 11.88 ns | 22.02 ns |  96.69 ns | 0.0459 |     - |     - |      96 B |
