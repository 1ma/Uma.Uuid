using System;
using BenchmarkDotNet.Attributes;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class CreationFromByteArray
    {
        private static readonly byte[] SampleArray =
        {
            0x5a, 0xfd, 0x64, 0x86, 0x42, 0x79, 0x5a, 0x52,
            0x8d, 0xad, 0x76, 0x8c, 0x6a, 0x70, 0x25, 0xfe
        };

        [Benchmark(Baseline = true)]
        public void CreateGuidFromByteArray()
        {
            new Guid(SampleArray);
        }

        [Benchmark]
        public void CreateUuidFromByteArray()
        {
            new Uuid(SampleArray);
        }
    }
}
