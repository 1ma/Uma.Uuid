using Xunit;

namespace Uma.Uuid.Tests
{
    public class SequentialGeneratorTest
    {
        [Fact]
        public void TestBasicUsage()
        {
            var generator = new SequentialGenerator();

            Assert.Equal("00000000-0000-0000-0000-000000000000", generator.Generate().ToString());
            Assert.Equal("00000000-0000-0000-0000-000000000001", generator.Generate().ToString());
            Assert.Equal("00000000-0000-0000-0000-000000000002", generator.Generate().ToString());
        }

        [Fact]
        public void TestCustomSequentialGeneration()
        {
            var generator = new SequentialGenerator(0xdeadf00d, 0xff);

            Assert.Equal("00000000-dead-f00d-0000-0000000000ff", generator.Generate().ToString());
            Assert.Equal("00000000-dead-f00d-0000-000000000100", generator.Generate().ToString());
            Assert.Equal("00000000-dead-f00d-0000-000000000101", generator.Generate().ToString());
        }
    }
}
