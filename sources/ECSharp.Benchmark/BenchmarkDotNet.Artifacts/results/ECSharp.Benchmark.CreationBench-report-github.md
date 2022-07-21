``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.302
  [Host]     : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT
  Job-OOUKRJ : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|                          Method |       Mean |      Error |     StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------------- |-----------:|-----------:|-----------:|--------:|-------:|------:|----------:|
|  CreateNewEntitiesSameArchetype |   6.553 μs |  0.2832 μs |  0.5107 μs |  2.5024 | 0.6180 |     - |   5.12 KB |
| CreateNewEntitiesSameArchetype2 | 180.427 μs | 24.4734 μs | 44.1306 μs |  9.7046 | 1.1597 |     - |  19.91 KB |
|                 RemoveComponent | 208.018 μs | 21.7971 μs | 36.4180 μs | 25.3906 | 1.0986 |     - |  52.09 KB |
