using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CommandLineTests
{
    public class GetCommandLineArgsTests
    {
        [Fact]
        public void GetCommandLineArgsAbleToHandleQuotes()
        {
            var inputString = "controller command --parameter \"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new []{ "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.Equal(expectedArgArray, actualArgArray);
        }

        [Fact]
        public void GetCommandLineArgsAbleToHandleExtraWhiteSpace()
        {
            var inputString = " controller command --parameter   \"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.Equal(expectedArgArray, actualArgArray);
        }

        [Fact]
        public void GetCommandLineArgsAbleToHandleMissingWhiteSpace()
        {
            var inputString = " controller command --parameter\"value\" --paramaterTwo -2";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "controller", "command", "--parameter", "value", "--paramaterTwo", "-2" };

            Assert.Equal(expectedArgArray, actualArgArray);
        }

        [Fact]
        public void GetCommandLineArgsAbleToHandleSingleArgument()
        {
            var inputString = "?";
            var actualArgArray = CommandLine.GetCommandLineArgs(inputString);
            var expectedArgArray = new[] { "?" };

            Assert.Equal(expectedArgArray, actualArgArray);
        }
    }
}
