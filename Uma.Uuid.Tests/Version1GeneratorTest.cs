using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class Version1GeneratorTest
    {
        [Fact]
        public void TestValidMacAddress()
        {
            var generator = new Version1Generator("01:23:45:67:89:ab");

            Assert.True(Uuid.IsUuid(generator.NewUuid().ToString()));
        }

        [Fact]
        public void TestInvalidMacAddress()
        {
            Assert.Throws<ArgumentException>(() => new Version1Generator("wh:at:is:th:is:?!"));
        }
    }
}
