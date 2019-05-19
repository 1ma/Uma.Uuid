using System;
using BenchmarkDotNet.Attributes;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class Generation
    {
        private static readonly IUuidGenerator _comb = new CombGenerator();
        private static readonly IUuidGenerator _version4 = new Version4Generator();
        private static readonly IUuidGenerator _version5 = new Version5Generator(new Uuid(Version5Generator.NS_URL));
        private static readonly IUuidGenerator _sequential = new SequentialGenerator();

        [Benchmark(Baseline = true)]
        public Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        [Benchmark]
        public Uuid GenerateComb()
        {
            return _comb.NewUuid();
        }

        [Benchmark]
        public Uuid GenerateVersion4()
        {
            return _version4.NewUuid();
        }

        [Benchmark]
        public Uuid GenerateVersion5()
        {
            return _version5.NewUuid("dot.net");
        }

        [Benchmark]
        public Uuid GenerateSequential()
        {
            return _sequential.NewUuid();
        }
    }
}
