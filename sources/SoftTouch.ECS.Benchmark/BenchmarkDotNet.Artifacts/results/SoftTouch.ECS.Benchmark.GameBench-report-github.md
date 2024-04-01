``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19045
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=8.0.100
  [Host]     : .NET Core 8.0.0 (CoreCLR 8.0.23.53103, CoreFX 8.0.23.53103), X64 RyuJIT
  Job-OMRNDM : .NET Core 8.0.0 (CoreCLR 8.0.23.53103, CoreFX 8.0.23.53103), X64 RyuJIT

IterationCount=5  LaunchCount=1  WarmupCount=5  

```
|          Method |     Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |---------:|---------:|---------:|------:|------:|------:|----------:|
| Spawn1Component | 247.6 ns | 124.3 ns | 32.28 ns |     - |     - |     - |         - |
