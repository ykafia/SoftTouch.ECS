``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19045
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=8.0.204
  [Host]     : .NET Core 8.0.4 (CoreCLR 8.0.424.16909, CoreFX 8.0.424.16909), X64 RyuJIT
  Job-MVAGWJ : .NET Core 8.0.4 (CoreCLR 8.0.424.16909, CoreFX 8.0.424.16909), X64 RyuJIT

IterationCount=5  LaunchCount=1  WarmupCount=5  

```
|                     Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
| CommunityParallelClearWith | 18.85 μs | 1.692 μs | 0.439 μs | 2.4414 |     - |     - |   7.38 KB |
|    CommunityParallelHelper | 13.92 μs | 3.202 μs | 0.496 μs | 2.2125 |     - |     - |   6.72 KB |
|           ParallelForEachT | 10.90 μs | 1.792 μs | 0.465 μs | 1.3885 |     - |     - |    4.3 KB |
