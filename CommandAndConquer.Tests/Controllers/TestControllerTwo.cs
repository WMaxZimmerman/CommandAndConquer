using System;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.Tests.Models;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("test2", "This is a test description.")]
    public static class TestControllerTwo
    {
        [CliCommand("example", "This is an example description.")]
        public static void TestMethod(SampleEnum sample)
        {
            Console.WriteLine($"{Enum.GetName(typeof(SampleEnum), sample)}");
        }
    }
}
