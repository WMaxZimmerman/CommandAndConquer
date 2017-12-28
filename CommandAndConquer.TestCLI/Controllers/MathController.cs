using System;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.TestCLI.Controllers
{
    [CliController("math", "Some Math controller thing.")]
    public static class MathController
    {
        [CliCommand("add", "adds two numbers together")]
        public static void AddTwoNumbers(float firstNum, float secondNum)
        {
            Console.WriteLine(firstNum + secondNum);
        }
    }
}
