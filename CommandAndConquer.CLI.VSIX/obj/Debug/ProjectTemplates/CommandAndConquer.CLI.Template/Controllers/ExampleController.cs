using System;
using System.Collections.Generic;
using CommandAndConquer.CLI.Attributes;

namespace $safeprojectname$.Controllers
{
    [CliController("example", "This is a description of the controller.")]
    public static class ExampleController
    {
        [CliCommand("test", "This is an example of how you can setup methods.")]
        public static void MethodExample(string something, List<string> someMoreThings = null)
        {
            Console.WriteLine(something);
            foreach (var thing in someMoreThings)
            {
                Console.WriteLine(thing);
            }
        }
    }
}