using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class NegativeExecutionTests: BaseCliTest
    {
        [Test]
        public void AbleToHandleMissingAllAguments()
        {
            SetApplicationLoopEnabled(false);

            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                $"Please enter a controller.  Use '{helpString}' to see available controllers."
            };
            Processor.ProcessArguments(new string[] { });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleMissingAllAgumentsAfterController()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                $"'' is not a valid command.  Use '{helpString}' to see available commands."
            };
            Processor.ProcessArguments(new[] { "execute" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleMissingParameters()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "The parameter 'sample' must be specified."
            };
            Processor.ProcessArguments(new[] { "execute", "example" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleInvalidParameters()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "The parameter 'invalidParam' is not a valid parameter."
            };
            Processor.ProcessArguments(new[] { "execute", "example", $"{argPre}sample", "EnumOne", $"{argPre}invalidParam", "bad" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleInvalidParameterValue()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "An error occured while attempting to execute the command.",
                "This is most likely due to invalid arguments.",
                $"Please verify the command usage with '{helpString}' and try again."
            };
            Processor.ProcessArguments(new[] { "execute", "example", $"{argPre}sample", "Enum" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }
        
        [Test]
        public void AbleToHandleCallToCommandThatThrowsException()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "An error occurred while executing the command.",
                "Message: I blew up yer thingy.",
                @"Stack Trace: at CommandAndConquer.Tests.Controllers.ExecutionController.ThrowExceptionMethod(SampleEnum sample) in C:\git\CommandAndConquer\CommandAndConquer.Tests\Controllers\ExecutionController.cs:line 27"
            };
            Processor.ProcessArguments(new[] { "execute", "exception", $"{argPre}sample", "EnumOne" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }
    }
}
