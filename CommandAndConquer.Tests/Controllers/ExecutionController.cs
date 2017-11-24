using System;
using System.Collections.Generic;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.Tests.Models;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("execute", "This is a test description.")]
    public static class ExecutionController
    {
        [CliCommand("example", "This is an example description.")]
        public static void TestMethod(SampleEnum sample)
        {
            Console.WriteLine($"{Enum.GetName(typeof(SampleEnum), sample)}");
        }

        [CliCommand("example2", "This is an example description.")]
        public static void TestMethod1(string[] list, int something)
        {
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }
        }

        [CliCommand("example3", "This is an example description.")]
        public static void TestMethod2(List<string> list, int something)
        {
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }
        }

        [CliCommand("example4", "This is an example description.")]
        public static void TestMethod3(IEnumerable<string> list, int something)
        {
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }
        }
    }
}
