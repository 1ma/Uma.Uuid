using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class UuidTest
    {
        [Fact]
        public void TestStringSerialization()
        {
            const string text = "00112233-4455-6677-8899-aabbccddeeff";

            var uuid = new Uuid(text);

            Assert.Equal(text, uuid.ToString());
        }

        [Fact]
        public void TestByteArraySerialization()
        {
            var bytes = new byte[]
            {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff
            };

            var uuid = new Uuid(bytes);

            Assert.Equal(bytes, uuid.ToByteArray());
        }

        [Fact]
        public void TestGuidConversion()
        {
            const string text = "00112233-4455-6677-8899-aabbccddeeff";

            var uuid = new Uuid(text);
            var guid = new Guid(text);

            Assert.Equal(uuid.ToGuid(), guid);
            Assert.Equal(uuid.ToString(), guid.ToString());
        }

        [Fact]
        public void TestInvalidInitializations()
        {
            Assert.Throws<ArgumentException>(() => new Uuid(""));
            Assert.Throws<ArgumentException>(() => new Uuid("not-a-uuid"));
            Assert.Throws<ArgumentException>(() => new Uuid("00112233-4455-6677-8899-aabbccddeef"));
            Assert.Throws<ArgumentException>(() => new Uuid("00112233-4455-66778-899-aabbccddeeff"));
            Assert.Throws<ArgumentException>(() => new Uuid("00112233-4455-66x7-8899-aabbccddeeff"));
            Assert.Throws<ArgumentException>(() => new Uuid(new byte[] { }));
            Assert.Throws<ArgumentException>(() => new Uuid(new byte[] {0x00, 0x01}));
        }
    }
}
