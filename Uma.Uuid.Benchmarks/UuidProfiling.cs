using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Uma.Uuid.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class UuidProfiling
    {
        private const string SampleString = "5afd6486-4279-5a52-8dad-768c6a7025fe";

        private static readonly byte[] SampleArray =
        {
            0x5a, 0xfd, 0x64, 0x86, 0x42, 0x79, 0x5a, 0x52,
            0x8d, 0xad, 0x76, 0x8c, 0x6a, 0x70, 0x25, 0xfe
        };

        private static readonly IUuidGenerator _version4 = new Version4Generator();
        private static readonly IUuidGenerator _version5 = new Version5Generator(new Uuid(Version5Generator.NS_URL));

        [Benchmark(Baseline = true)]
        public void CreateGuidFromByteArray()
        {
            new Guid(SampleArray);
        }

        [Benchmark]
        public void CreateGuidFromString()
        {
            new Guid(SampleString);
        }

        [Benchmark]
        public void CreateUuidFromByteArray()
        {
            new Uuid(SampleArray);
        }

        [Benchmark]
        public void CreateUuidFromString()
        {
            new Uuid(SampleString);
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
