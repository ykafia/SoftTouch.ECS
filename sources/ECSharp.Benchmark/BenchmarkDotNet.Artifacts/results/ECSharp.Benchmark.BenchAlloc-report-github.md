``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19044
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100
  [Host]     : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT
  Job-BMQXUV : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|             Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
|     AllocComponent | 218.1 ns | 3.27 ns | 6.23 ns | 0.1018 |     - |     - |     320 B |
| AllocNullComponent | 198.8 ns | 3.41 ns | 6.40 ns | 0.0815 |     - |     - |     256 B |
