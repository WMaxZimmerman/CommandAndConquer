using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CommandLineTests
{
    [TestFixture]
    public class GetCommandLineArgsTests
    {
        [Test]
        public void GetCommandLineArgsAbleToHandleQuotes()
        {
            var inputString = "controller command --parameter \"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new []{ "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.AreEqual(actualArgArray, expectedArgArray);
        }

        [Test]
        public void GetCommandLineArgsAbleToHandleExtraWhiteSpace()
        {
            var inputString = " controller command --parameter   \"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.AreEqual(actualArgArray, expectedArgArray);
        }

        [Test]
        public void GetCommandLineArgsAbleToHandleMissingWhiteSpace()
        {
            var inputString = " controller command --parameter\"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.AreEqual(actualArgArray, expectedArgArray);
        }

        [Test]
        public void GetCommandLineArgsAbleToHandleSingleArgument()
        {
            var inputString = "?";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "?" };

            Assert.AreEqual(actualArgArray, expectedArgArray);
        }
    }
}
