using System;
using CommandAndConquer.CLI.Attributes;

namespace $safeprojectname$.Controllers
{
    [CliController("ex", "The is an example of a controller that you can setup.")]
    public static class ExampleController
    {
        [CliCommand("hello", "This is a simple hello world command. It is to be used a reference when creating your own commands.")]
        public static void HelloWorld(string firstName, string lastName = null)
        {
            Console.WriteLine($"Hello, {firstName} {lastName}");
        }
    }
}
