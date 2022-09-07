``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19044
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100
  [Host]     : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT
  Job-FCKHFW : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|             Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----------:|----------:|----------:|------:|------:|------:|----------:|
|    StructComponent | 0.8448 ns | 0.0777 ns | 0.1420 ns |     - |     - |     - |         - |
| StructRefComponent | 0.3327 ns | 0.0257 ns | 0.0464 ns |     - |     - |     - |         - |
|     ClassComponent | 0.5225 ns | 0.0322 ns | 0.0605 ns |     - |     - |     - |         - |
