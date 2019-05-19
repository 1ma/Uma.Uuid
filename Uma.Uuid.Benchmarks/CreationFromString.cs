using System;
using BenchmarkDotNet.Attributes;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class CreationFromString
    {
        private const string SampleString = "5afd6486-4279-5a52-8dad-768c6a7025fe";

        [Benchmark(Baseline = true)]
        public Guid CreateGuidFromString()
        {
            return new Guid(SampleString);
        }

        [Benchmark]
        public Uuid CreateUuidFromString()
        {
            return new Uuid(SampleString);
        }
    }
}
