using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class NegativeExecutionTests: BaseCliTest
    {
        [Test]
        public void AbleToHandleBadCallToCommandWithEnumParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "The specified value of 'Enum' is not valid for the parameter 'sample'."
            };
            Processor.ProcessArguments(new[] { "execute", "example", "-sample", "Enum" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleBadCallToCommand()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "An error occured while attempting to execute the command.",
                "This is most likely due to invalid arguments.",
                "Please verify the command usage with '?' and try again."
            };
            Processor.ProcessArguments(new[] { "execute", "example", "-sample", "Enum" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }
    }
}
