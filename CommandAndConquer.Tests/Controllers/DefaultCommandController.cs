using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.Tests.Models;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("default", "This is a test description.")]
    public class DefaultCommandController
    {
        [CliDefaultCommand("bool", "This is an example description.")]
        public static void TestMethod(bool withOutput)
        {
            if (withOutput)
            {
                Console.WriteLine("Here is some output.");
            }
        }
    }
}
