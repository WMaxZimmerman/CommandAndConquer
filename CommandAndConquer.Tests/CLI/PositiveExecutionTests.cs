using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CLI
{
    public class PositiveExecutionTests: BaseCliTest
    {
        [Fact]
        public void AbleToCallCommandWithoutOptionalParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"bleh 0"};
            Processor.ProcessArguments(new[] {"document", "example", $"{argPre}required", "bleh"});
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallCommandWithOptionalParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"bleh 5"};
            Processor.ProcessArguments(new[] { "document", "example", $"{argPre}required", "bleh", $"{argPre}opt", "5" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallCommandWithEnumParameter()
        {
            mockConsole.Clear();
            var consoleLines = new List<string> {"EnumOne"};
            Processor.ProcessArguments(new[] { "execute", "example", $"{argPre}sample", "EnumOne" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallCommandWithParameterAlias()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "thingOne",
                "thingThree",
                "thingTwo",
                "3"
            };
            Processor.ProcessArguments(new[] { "execute", "enumerable", $"{argPre}values", "thingOne", "thingThree", "thingTwo", $"{argPre}s", "3" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
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
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallCommandWithBooleanParameterWithNoValues()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "Here is some output."
            };
            Processor.ProcessArguments(new[] { "execute", "bool", $"{argPre}withOutput" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToCallNonStaticComamndWithEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "EnumOne"
            };
            Processor.ProcessArguments(new[] { "execute", "nonstatic", $"{argPre}sample", "EnumOne" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.Equal(expectedString, temp);
        }
    }
}
