using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class TranscodingTest
    {
        [Fact]
        public void TestHexToBinTranscoding()
        {
            Assert.Equal(new byte[] { }, Transcoding.Transcoding.HexToBin(""));
            Assert.Equal(new byte[] {0xab, 0xcd}, Transcoding.Transcoding.HexToBin("abcd"));
            Assert.Equal(new byte[] {0x00, 0x01}, Transcoding.Transcoding.HexToBin("0001"));
            Assert.Equal(new byte[] {0xff, 0xff}, Transcoding.Transcoding.HexToBin("FFFF"));
            Assert.Throws<ArgumentException>(() => Transcoding.Transcoding.HexToBin("a"));
            Assert.Throws<ArgumentException>(() => Transcoding.Transcoding.HexToBin("xy"));
        }

        [Fact]
        public void TestBinToHexTranscoding()
        {
            Assert.Equal("", Transcoding.Transcoding.BinToHex(new byte[] {}));
            Assert.Equal("abcd", Transcoding.Transcoding.BinToHex(new byte[] {0xab, 0xcd}));
            Assert.Equal("0001", Transcoding.Transcoding.BinToHex(new byte[] {0x00, 0x01}));
            Assert.Equal("ffff", Transcoding.Transcoding.BinToHex(new byte[] {0xff, 0xff}));
        }


    }
}