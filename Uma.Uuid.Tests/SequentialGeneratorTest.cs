using Xunit;

namespace Uma.Uuid.Tests
{
    public class SequentialGeneratorTest
    {
        [Fact]
        public void TestBasicUsage()
        {
            var generator = new SequentialGenerator();

            Assert.Equal("00000000-0000-0000-0000-000000000000", generator.NewUuid().ToString());
            Assert.Equal("00000000-0000-0000-0000-000000000001", generator.NewUuid().ToString());
            Assert.Equal("00000000-0000-0000-0000-000000000002", generator.NewUuid().ToString());
        }

        [Fact]
        public void TestCustomSequentialGeneration()
        {
            var generator = new SequentialGenerator(0xdeadf00d, 0xff);

            Assert.Equal("00000000-dead-f00d-0000-0000000000ff", generator.NewUuid().ToString());
            Assert.Equal("00000000-dead-f00d-0000-000000000100", generator.NewUuid().ToString());
            Assert.Equal("00000000-dead-f00d-0000-000000000101", generator.NewUuid().ToString());
        }
    }
}
