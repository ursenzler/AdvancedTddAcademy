namespace ProductBacklog
{
    using System;
    using System.Collections;
    using Console = System.Console;

    public static class Program
    {
        static void Main(string[] args)
        {
            var compositionRoot = new CompositionRoot(Environment.GetEnvironmentVariables());
            var facade = compositionRoot.CreateSmokeTestFacade();

            var input = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(input))
            {
                switch (input)
                {
                    case "exit":
                        return;

                    case string i when i.StartsWith("run smoke"):
                        var value = input.Substring(10);
                        var result = facade.RunSmoke(value);
                        Console.WriteLine(result);
                        break;

                    default:
                        Console.WriteLine("sorry, I did not understand you.");
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }

    public class CompositionRoot
    {
        private readonly IDictionary environmentVariables;

        public CompositionRoot(IDictionary environmentVariables)
        {
            this.environmentVariables = environmentVariables;
        }

        public ISmokeTestFacade CreateSmokeTestFacade()
        {
            ISmokeTestFacade facade = this.environmentVariables[FakeSmokeTestFacade.Fake] == null
                ? (ISmokeTestFacade)new SmokeTestFacade()
                : (ISmokeTestFacade)new FakeSmokeTestFacade();
            return facade;
        }
    }

    public interface ISmokeTestFacade
    {
        int RunSmoke(string value);
    }

    public class SmokeTestFacade : ISmokeTestFacade
    {
        public int RunSmoke(string value)
        {
            var smoke = new Smoke();
            return smoke.Run(value);
        }
    }

    public class FakeSmokeTestFacade : ISmokeTestFacade
    {
        public const string Fake = "fake";
        public const string FakeSmokeRun = "fake-smoke-run";

        public int RunSmoke(string value)
        {
            return int.Parse(Environment.GetEnvironmentVariable(FakeSmokeRun) ?? "0");
        }
    }
}
