using System;
using Uma.Uuid.Transcoding;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class TranscoderTest
    {
        [Fact]
        public void TestHexToBinTranscoding()
        {
            Assert.Equal(new byte[] { }, Transcoder.HexToBin(""));
            Assert.Equal(new byte[] {0xab, 0xcd}, Transcoder.HexToBin("abcd"));
            Assert.Equal(new byte[] {0x00, 0x01}, Transcoder.HexToBin("0001"));
            Assert.Equal(new byte[] {0xff, 0xff}, Transcoder.HexToBin("FFFF"));
            Assert.Throws<ArgumentException>(() => Transcoder.HexToBin("a"));
            Assert.Throws<ArgumentException>(() => Transcoder.HexToBin("ah"));
            Assert.Throws<ArgumentException>(() => Transcoder.HexToBin("x"));
            Assert.Throws<ArgumentException>(() => Transcoder.HexToBin("xy"));
        }

        [Fact]
        public void TestBinToHexTranscoding()
        {
            Assert.Equal("", Transcoder.BinToHex(new byte[] { }));
            Assert.Equal("abcd", Transcoder.BinToHex(new byte[] {0xab, 0xcd}));
            Assert.Equal("0001", Transcoder.BinToHex(new byte[] {0x00, 0x01}));
            Assert.Equal("ffff", Transcoder.BinToHex(new byte[] {0xff, 0xff}));
        }
    }
}
