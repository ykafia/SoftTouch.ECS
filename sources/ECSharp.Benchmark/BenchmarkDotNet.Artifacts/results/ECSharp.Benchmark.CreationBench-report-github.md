``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.302
  [Host]     : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT
  Job-DUGQIC : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|                          Method |      Mean |     Error |    StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------------- |----------:|----------:|----------:|--------:|-------:|------:|----------:|
|  CreateNewEntitiesSameArchetype |  4.132 μs | 0.0500 μs | 0.0876 μs |  2.1133 | 0.5264 |     - |   4.33 KB |
| CreateNewEntitiesSameArchetype2 | 16.892 μs | 0.0827 μs | 0.1533 μs |  8.6365 | 1.2207 |     - |  17.67 KB |
|                 RemoveComponent | 45.573 μs | 0.4586 μs | 0.8386 μs | 23.3154 | 1.2207 |     - |  47.68 KB |
