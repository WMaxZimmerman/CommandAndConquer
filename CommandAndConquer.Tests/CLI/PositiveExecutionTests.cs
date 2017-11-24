using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class PositiveExecutionTests: BaseCliTest
    {
        [Test]
        public void AbleToCallCommandWithoutOptionalParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"bleh 0"};
            Processor.ProcessArguments(new[] {"document", "example", $"{argPre}required", "bleh"});
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithOptionalParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"bleh 5"};
            Processor.ProcessArguments(new[] { "document", "example", $"{argPre}required", "bleh", $"{argPre}opt", "5" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithEnumParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"EnumOne"};
            Processor.ProcessArguments(new[] { "execute", "example", $"{argPre}sample", "EnumOne" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithArrayParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "thingOne",
                "thingThree",
                "thingTwo",
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "array", $"{argPre}values", "thingOne", "thingThree", "thingTwo", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithListParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "EnumTwo",
                "EnumOne",
                "EnumThree",
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "list", $"{argPre}values", "EnumTwo", "EnumOne", "EnumThree", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithIEnumerableParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "thingOne",
                "thingThree",
                "thingTwo",
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "enumerable", $"{argPre}values", "thingOne", "thingThree", "thingTwo", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithArrayParameterWithNoValues()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "array", $"{argPre}values", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithListParameterWithNoValues()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "list", $"{argPre}values", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithEnumerableParameterWithNoValues()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "enumerable", $"{argPre}values", $"{argPre}something", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.IsTrue(temp == expectedString);
        }
    }
}
