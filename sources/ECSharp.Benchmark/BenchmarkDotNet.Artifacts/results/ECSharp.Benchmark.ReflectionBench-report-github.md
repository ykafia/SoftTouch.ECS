``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.400
  [Host]     : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT
  Job-EUVYCC : .NET Core 6.0.10 (CoreCLR 6.0.1022.47605, CoreFX 6.0.1022.47605), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|           Method |        Mean |      Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |------------:|-----------:|----------:|-------:|------:|------:|----------:|
|      JSerializer | 3,389.86 ns | 155.970 ns | 285.20 ns | 0.6142 |     - |     - |    1288 B |
|     JsSerializer | 3,567.37 ns | 226.663 ns | 425.73 ns | 0.6027 |     - |     - |    1272 B |
|    AOTReflection |    86.23 ns |   7.288 ns |  13.69 ns | 0.0421 |     - |     - |      88 B |
|       Reflection |   495.02 ns |  29.203 ns |  54.13 ns | 0.0381 |     - |     - |      80 B |
| CachedReflection |   421.75 ns |  30.458 ns |  57.95 ns | 0.0381 |     - |     - |      80 B |
