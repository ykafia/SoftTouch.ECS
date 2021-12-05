using ECSharp;
using ECSharp.Components;
using BenchmarkDotNet.Running;

namespace ECSharp.Benchmark
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}