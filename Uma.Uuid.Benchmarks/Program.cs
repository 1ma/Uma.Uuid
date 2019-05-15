using System;
using BenchmarkDotNet.Running;

namespace Uma.Uuid.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<UuidProfiling>();
        }
    }
}
