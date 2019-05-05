using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CLI
{
    public class DefaultCommandTest: BaseCliTest
    {
        [Fact]
        public void AbleToCallCommandWithName()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "Here is some output."
            };
            Processor.ProcessArguments(new[] { "default", "bool", $"{argPre}withOutput" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallCommandWithoutName()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "Here is some output."
            };
            Processor.ProcessArguments(new[] { "default", $"{argPre}withOutput" });
            var temp           = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }
    }
}
