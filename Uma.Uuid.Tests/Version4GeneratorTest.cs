using System;
using Xunit;

namespace Uma.Uuid.Tests
{
    public class Version4GeneratorTest
    {
        [Fact]
        public void TestV4Generator()
        {
            var generator = new Version4Generator();
            var guid = generator.NewUuid();

            Assert.Equal('4', guid.ToString()[14]);
        }

        [Fact]
        public void TestV5Generator()
        {
            var generator = new Version5Generator(new Uuid(Version5Generator.NS_DNS));
            var guid = generator.NewUuid("dot.net");

            Assert.Equal("5afd6486-4279-5a52-8dad-768c6a7025fe", guid.ToString());

            Assert.Throws<ArgumentException>(() => generator.NewUuid());
        }

        [Fact]
        public void TestCombGenerator()
        {
            var generator = new CombGenerator();

            for (var i = 0; i < 20; i++)
            {
                Assert.Equal('4', generator.NewUuid().ToString()[14]);
            }

            Assert.True(true);
        }
    }
}
