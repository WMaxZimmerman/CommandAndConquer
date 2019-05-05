using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.Tests.Models;

namespace CommandAndConquer.Tests.Controllers
{
    [CliController("execute", "This is a test description.")]
    public class ExecutionController
    {
        [CliCommand("example", "This is an example description.")]
        public static void TestMethod(SampleEnum sample)
        {
            Console.WriteLine($"{Enum.GetName(typeof(SampleEnum), sample)}");
        }

        [CliCommand("nonstatic", "This is an example description.")]
        public void NonStaticTestMethod(SampleEnum sample)
        {
            Console.WriteLine($"{Enum.GetName(typeof(SampleEnum), sample)}");
        }

        [CliCommand("exception", "This is an command that will throw an exception.")]
        public static void ThrowExceptionMethod(SampleEnum sample)
        {
            throw new Exception("I blew up yer thingy.");
        }

        [CliCommand("array", "This is an example description.")]
        public static void TestMethod1(string[] values, [CliParameter('s', "This parameter does something.")]int something)
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
        public static void TestMethod3(IEnumerable<string> values, [CliParameter('s')]int something)
        {
            foreach (var l in values)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine(something);
        }

        [CliCommand("bool", "This is an example description.")]
        public static void TestMethod4(bool withOutput)
        {
            if (withOutput)
            {
                Console.WriteLine("Here is some output.");
            }
        }

        [CliCommand("without-alias", "This is an example description.")]
        public static void TestMethod5([CliParameter("without-alias")]bool something)
        {
            Console.WriteLine(something);
        }

        [CliCommand("longRunning", "performs long running async tasks")]
        public static void LongRunningTests(int firstNum, int secondNum)
        {
            PerformAsyncStuff(firstNum, secondNum).Wait();
        }

        private static async Task<bool> PerformAsyncStuff(int firstNum, int secondNum)
        {
            await LoopingSomeLongRunningThing(firstNum);

            await LoopingSomeLongRunningThing(secondNum);

            return true;
        }

        private static async Task LoopingSomeLongRunningThing(float times)
        {
            var tasks = new List<Task>();
            for (var i = 0; i < times; i++)
            {
                var num = i;
                tasks.Add(Task.Run(() => SomeLongRunningThing(num)));
            }
            await Task.WhenAll(tasks);
        }

        private static void SomeLongRunningThing(int times)
        {
            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"I am on iteration {times}");
        }
    }
}
