``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22000
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.302
  [Host]     : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT
  Job-PUBLZP : .NET Core 6.0.7 (CoreCLR 6.0.722.32202, CoreFX 6.0.722.32202), X64 RyuJIT

IterationCount=15  LaunchCount=3  WarmupCount=10  

```
|     Method |    Mean |    Error |   StdDev |  Median |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------- |--------:|---------:|---------:|--------:|----------:|------:|------:|----------:|
| Processor1 | 1.930 s | 0.1524 s | 0.2900 s | 1.833 s | 4000.0000 |     - |     - |   9.31 MB |
| Processor2 | 2.018 s | 0.1862 s | 0.3452 s | 1.842 s | 6000.0000 |     - |     - |  12.67 MB |
| Processor3 | 1.840 s | 0.0541 s | 0.1003 s | 1.855 s | 6000.0000 |     - |     - |  12.67 MB |
