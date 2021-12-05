``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100
  [Host]     : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT


```
|                          Method |      Mean |    Error |    StdDev |
|-------------------------------- |----------:|---------:|----------:|
|  CreateNewEntitiesSameArchetype |  54.32 ms | 3.851 ms | 10.607 ms |
| CreateNewEntitiesSameArchetype2 |  67.98 ms | 2.597 ms |  7.575 ms |
|                 RemoveComponent | 114.58 ms | 2.179 ms |  4.596 ms |
