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
        public void GenerateGuid()
        {
            Guid.NewGuid();
        }

        [Benchmark]
        public void GenerateComb()
        {
            _comb.Generate();
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

        [Benchmark]
        public void GenerateSequential()
        {
            _sequential.Generate();
        }
    }
}
