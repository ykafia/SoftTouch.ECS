``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19044
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100
  [Host]     : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT
  Job-GSZKMW : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|                          Method |         Mean |       Error |      StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------------- |-------------:|------------:|------------:|-------:|-------:|------:|----------:|
|  CreateNewEntitiesSameArchetype |     2.244 μs |   0.0935 μs |   0.1613 μs | 1.0338 | 0.2556 |     - |   3.17 KB |
| CreateNewEntitiesSameArchetype2 |     2.623 μs |   0.0624 μs |   0.1093 μs | 1.0452 | 0.2594 |     - |   3.21 KB |
|                 RemoveComponent | 1,015.657 μs | 112.7182 μs | 214.4580 μs | 4.5166 | 1.0986 |     - |   13.9 KB |
