using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Uma.Uuid.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class UuidProfiling
    {
        private const string SampleUuid = "5afd6486-4279-5a52-8dad-768c6a7025fe";

        private static readonly IUuidGenerator _version4 = new Version4Generator();
        private static readonly IUuidGenerator _version5 = new Version5Generator(new Uuid(Version5Generator.NS_URL));

        [Benchmark(Baseline = true)]
        public void CreateGuid()
        {
            new Guid(SampleUuid);
        }

        [Benchmark]
        public void CreateUuid()
        {
            new Uuid(SampleUuid);
        }

        [Benchmark]
        public void GenerateGuid()
        {
            Guid.NewGuid();
        }

        [Benchmark]
        public void GenerateVersion4()
        {
            _version4.Generate();
        }

        [Benchmark]
        public void GenerateVersion5()
        {
            _version5.Generate("dot.net");
        }
    }
}
