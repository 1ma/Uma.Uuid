using System;
using BenchmarkDotNet.Attributes;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class ToByteArray
    {
        private Guid _guid;
        private Uuid _uuid;

        [GlobalSetup]
        public void Setup()
        {
            _guid = Guid.NewGuid();
            _uuid = new Version4Generator().NewUuid();
        }

        [Benchmark(Baseline = true)]
        public byte[] GuidToByteArray()
        {
            return _guid.ToByteArray();
        }

        [Benchmark]
        public byte[] UuidToByteArray()
        {
            return _uuid.ToByteArray();
        }
    }
}
