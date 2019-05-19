using BenchmarkDotNet.Attributes;
using System;
using Uma.Uuid.Transcoding;

namespace Uma.Uuid.Benchmarks
{
    [MemoryDiagnoser]
    public class Transcoding
    {
        private string _text;
        private byte[] _bytes;

        [Params(16, 128, 8192)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _bytes = new byte[N];

            new Random().NextBytes(_bytes);

            _text = Transcoder.BinToHex(_bytes);
        }

        [Benchmark]
        public string BinToHex()
        {
            return Transcoder.BinToHex(_bytes);
        }

        [Benchmark]
        public byte[] HexToBin()
        {
            return Transcoder.HexToBin(_text);
        }
    }
}
