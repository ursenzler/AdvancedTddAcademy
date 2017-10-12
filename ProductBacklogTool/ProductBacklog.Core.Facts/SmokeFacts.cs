namespace ProductBacklog
{
    using FluentAssertions;
    using Xunit;

    public class SmokeFacts
    {
        [Fact]
        public void FactsRun()
        {
            var testee = new Smoke();

            var result = testee.Run("check");

            result.Should().Be(5);
        }
    }
}
