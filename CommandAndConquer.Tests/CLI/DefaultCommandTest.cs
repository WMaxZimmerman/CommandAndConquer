using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class DefaultCommandTest: BaseCliTest
    {
        [Test]
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
            Assert.AreEqual(expectedString, temp);
        }

        [Test]
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
            Assert.AreEqual(expectedString, temp);
        }
    }
}
