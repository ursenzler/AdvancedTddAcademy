namespace Console.Specs
{
    using System.Diagnostics;
    using FluentAssertions;
    using ProductBacklog;
    using Xbehave;

    public class SmokeSpecs
    {
        [Scenario]
        public void RunSmokeTest(
            Process process)
        {
            "establish console app".x(()
                => process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ProductBacklog.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        CreateNoWindow = true
                    }
                });

            "run console app".x(()
                => process.Start());

            "wenn running smoke test".x(()
                => process.StandardInput.Write("run smoke check\r\n"));

            "test should run successfully".x(()
                => process.StandardOutput.ReadLine().Should().Be("5"));
        }

        [Scenario]
        public void RunSmokeTestWithFakedBusinessLogic(
            Process process)
        {
            "establish console app".x(()
                => process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ProductBacklog.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        CreateNoWindow = true,
                        Environment =
                        {
                            { FakeSmokeTestFacade.Fake, "true" },
                            { FakeSmokeTestFacade.FakeSmokeRun, "3" }
                        }
                    }
                });

            "run console app".x(()
                => process.Start());

            "wenn running smoke test".x(()
                => process.StandardInput.Write("run smoke check\r\n"));

            "test should run successfully".x(()
                => process.StandardOutput.ReadLine().Should().Be("3"));
        }
    }
}
