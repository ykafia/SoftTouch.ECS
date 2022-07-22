``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.302
  [Host]     : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT
  Job-KGCKUW : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|             Method |       Mean |    Error |   StdDev |     Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |-----------:|---------:|---------:|-----------:|------:|------:|------:|----------:|
|    StructComponent | 1,684.1 ns |  8.74 ns | 16.20 ns | 1,683.5 ns |     - |     - |     - |         - |
| StructRefComponent |   449.7 ns |  2.80 ns |  5.19 ns |   449.5 ns |     - |     - |     - |         - |
|     ClassComponent |   932.2 ns | 17.28 ns | 32.03 ns |   951.1 ns |     - |     - |     - |         - |
