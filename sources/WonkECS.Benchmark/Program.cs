using WonkECS;
using WonkECS.Components;
using BenchmarkDotNet.Running;

namespace WonkECS.Benchmark
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}