namespace ProductBacklog
{
    using FluentAssertions;
    using Xbehave;

    public class SmokeSpecs
    {
        [Scenario]
        public void SpecsRun(
            Smoke smoke,
            int result)
        {
            "establish smoke test infrastructure".x(()
                => smoke = new Smoke());

            "when running smoke test".x(()
                => result = smoke.Run("check"));

            "if should succeed".x(()
                => result.Should().Be(5));
        }
    }
}
