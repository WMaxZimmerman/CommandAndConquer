using System;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("document", "This is a test description.")]
    public static class DocumentationController
    {
        [CliCommand("example", "This is an example description.")]
        public static void TestMethod(string required, int opt = 0)
        {
            Console.WriteLine($"{required} {opt}");
        }
    }
}

public static class BlowShitUp
{
    
}
