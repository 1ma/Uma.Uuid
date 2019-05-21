using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class Version5GeneratorTest
    {
        [Fact]
        public void TestV5Generator()
        {
            var generator = new Version5Generator(new Uuid(Version5Generator.NS_DNS));
            var guid = generator.NewUuid("dot.net");

            Assert.Equal("5afd6486-4279-5a52-8dad-768c6a7025fe", guid.ToString());

            Assert.Throws<ArgumentException>(() => generator.NewUuid());
        }
    }
}
