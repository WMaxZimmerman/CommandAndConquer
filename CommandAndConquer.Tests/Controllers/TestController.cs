using System;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("test", "This is a test description.")]
    public static class TestController
    {
        [CliCommand("example", "This is an example description.")]
        public static void TestMethod(string required, int opt = 0)
        {
            Console.WriteLine($"{required} {opt}");
        }
    }
}
