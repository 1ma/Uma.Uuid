using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class GeneratorTest
    {
        [Fact]
        public void TestV4Generator()
        {
            var generator = new V4Generator();
            var guid = generator.generate();

            Assert.Equal('4', guid.ToString()[14]);
        }

        [Fact]
        public void TestV5Generator()
        {
            var generator = new V5Generator(new Guid(V5Generator.NS_DNS));
            var guid = generator.generate("dot.net");

            Assert.Equal("5afd6486-4279-5a52-8dad-768c6a7025fe", guid.ToString());
        }
    }
}