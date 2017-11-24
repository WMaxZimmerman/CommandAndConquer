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

        [CliCommand("exception", "This is an command that will throw an exception.")]
        public static void ThrowExceptionMethod(SampleEnum sample)
        {
            throw new Exception("I blew up yer thingy.");
        }

        [CliCommand("array", "This is an example description.")]
        public static void TestMethod1(string[] values, int something)
        {
            foreach (var l in values)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine(something);
        }

        [CliCommand("list", "This is an example description.")]
        public static void TestMethod2(List<SampleEnum> values, int something)
        {
            foreach (var l in values)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine(something);
        }

        [CliCommand("enumerable", "This is an example description.")]
        public static void TestMethod3(IEnumerable<string> values, int something)
        {
            foreach (var l in values)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine(something);
        }
    }
}
