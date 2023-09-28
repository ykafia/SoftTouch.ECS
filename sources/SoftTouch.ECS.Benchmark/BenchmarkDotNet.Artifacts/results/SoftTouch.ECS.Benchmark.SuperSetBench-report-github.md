``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19045
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.401
  [Host]     : .NET Core 7.0.11 (CoreCLR 7.0.1123.42427, CoreFX 7.0.1123.42427), X64 RyuJIT
  Job-BYVDZS : .NET Core 7.0.11 (CoreCLR 7.0.1123.42427, CoreFX 7.0.1123.42427), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|   Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
| SuperSet | 342.7 ns | 22.81 ns | 42.84 ns | 0.0916 |     - |     - |     288 B |
