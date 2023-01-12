``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19044
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.51805, CoreFX 7.0.22.51805), X64 RyuJIT
  Job-TMZFQK : .NET Core 7.0.0 (CoreCLR 7.0.22.51805, CoreFX 7.0.22.51805), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|          Method |      Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----------:|----------:|----------:|-------:|------:|------:|----------:|
| QueryArchetypes |  1.627 μs | 0.1137 μs | 0.2163 μs | 0.2728 |     - |     - |     856 B |
| QueryComponents | 24.274 μs | 2.6087 μs | 4.8353 μs | 8.1177 |     - |     - |   25339 B |
