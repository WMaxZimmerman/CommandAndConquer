using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CLI
{
    public class DocumentationTests: BaseCliTest
    {
        [Fact]
        public void AbleToRetriveProgramDocumentation()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "default - This is a test description.",
                "document - This is a test description.",
                "execute - This is a test description."
            };
            Processor.ProcessArguments(new[] { helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, false);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveControllerDocumentation()
        {
            SetParamDetail("simple");
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}required (String): This parameter is Required.",
                $"{argPre}opt (Int32): This parameter is Optional."
            };
            Processor.ProcessArguments(new[] { "document", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentation()
        {
            SetParamDetail("simple");
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}required (String): This parameter is Required.",
                $"{argPre}opt (Int32): This parameter is Optional."
            };
            Processor.ProcessArguments(new[] { "document", "example", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        //[Fact]
        //public void AbleToRetriveCommandDocumentationWithDetailedParams()
        //{
        //    SetParamDetail("detailed");
        //    mockConsole.Clear();
        //    var consoleLines = new List<string>
        //    {
        //        "example",
        //        "Description: This is an example description.",
        //        "Parameters:",
        //        $"{argPre}required (String): This parameter is Required.",
        //        $"{argPre}opt (Int32): This parameter is Optional with a default value of 0."
        //    };
        //    Processor.ProcessArguments(new[] { "document", "example", helpString });
        //    var temp = mockConsole.ToString();
        //    var expectedString = ConvertConsoleLinesToString(consoleLines, true);
        //    Assert.Equal(expectedString, temp);
        //}

        [Fact]
        public void AbleToRetriveCommandDocumentationWithEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}sample (SampleEnum): This parameter is Required and must be one of the following (EnumOne, EnumTwo, EnumThree)."
            };
            Processor.ProcessArguments(new[] { "execute", "example", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentationFromNonStaticMethodWithEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "nonstatic",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}sample (SampleEnum): This parameter is Required and must be one of the following (EnumOne, EnumTwo, EnumThree)."
            };
            Processor.ProcessArguments(new[] { "execute", "nonstatic", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentationWithListOfEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "list",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}values (List of SampleEnum): This parameter is Required and must be a collection of one of the following (EnumOne, EnumTwo, EnumThree).",
                $"{argPre}something (Int32): This parameter is Required."
            };
            Processor.ProcessArguments(new[] { "execute", "list", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentationWithAlias()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "enumerable",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}values (List of String): This parameter is Required.",
                $"{argPre}something | {argPre}s (Int32): This parameter is Required."
            };
            Processor.ProcessArguments(new[] { "execute", "enumerable", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentationWithoutAlias()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "without-alias",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}something (Boolean): This parameter is Required.",
                "Description: without-alias"
            };
            Processor.ProcessArguments(new[] { "execute", "without-alias", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }

        [Fact]
        public void AbleToRetriveCommandDocumentationWithAliasAndDescription()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "array",
                "Description: This is an example description.",
                "Parameters:",
                $"{argPre}values (List of String): This parameter is Required.",
                $"{argPre}something | {argPre}s (Int32): This parameter is Required.",
                "Description: This parameter does something."
            };
            Processor.ProcessArguments(new[] { "execute", "array", helpString });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.Equal(expectedString, temp);
        }
    }
}
