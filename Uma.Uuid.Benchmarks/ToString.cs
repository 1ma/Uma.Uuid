using System;
using BenchmarkDotNet.Attributes;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class ToString
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
        public string GuidToString()
        {
            return _guid.ToString();
        }

        [Benchmark]
        public string UuidToString()
        {
            return _uuid.ToString();
        }
    }
}
