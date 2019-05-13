using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class TranscoderTest
    {
        [Fact]
        public void TestHexToBinTranscoding()
        {
            Assert.Equal(new byte[] { }, Transcoding.Transcoder.HexToBin(""));
            Assert.Equal(new byte[] {0xab, 0xcd}, Transcoding.Transcoder.HexToBin("abcd"));
            Assert.Equal(new byte[] {0x00, 0x01}, Transcoding.Transcoder.HexToBin("0001"));
            Assert.Equal(new byte[] {0xff, 0xff}, Transcoding.Transcoder.HexToBin("FFFF"));
            Assert.Throws<ArgumentException>(() => Transcoding.Transcoder.HexToBin("a"));
            Assert.Throws<ArgumentException>(() => Transcoding.Transcoder.HexToBin("xy"));
        }

        [Fact]
        public void TestBinToHexTranscoding()
        {
            Assert.Equal("", Transcoding.Transcoder.BinToHex(new byte[] { }));
            Assert.Equal("abcd", Transcoding.Transcoder.BinToHex(new byte[] {0xab, 0xcd}));
            Assert.Equal("0001", Transcoding.Transcoder.BinToHex(new byte[] {0x00, 0x01}));
            Assert.Equal("ffff", Transcoding.Transcoder.BinToHex(new byte[] {0xff, 0xff}));
        }
    }
}