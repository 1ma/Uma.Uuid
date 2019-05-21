using Xunit;

namespace Uma.Uuid.Tests
{
    public class CombGeneratorTest
    {
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
