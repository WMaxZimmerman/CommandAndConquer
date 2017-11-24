﻿using System.Collections.Generic;
using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class DocumentationTests: BaseCliTest
    {
        [Test]
        public void AbleToRetriveProgramDocumentation()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "document - This is a test description.",
                "execute - This is a test description."
            };
            Processor.ProcessArguments(new[] { "?" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, false);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToRetriveControllerDocumentation()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                "-required (String): This parameter is Required.",
                "-opt (Int32): This parameter is Optional."
            };
            Processor.ProcessArguments(new[] { "document", "?" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToRetriveCommandDocumentation()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                "-required (String): This parameter is Required.",
                "-opt (Int32): This parameter is Optional."
            };
            Processor.ProcessArguments(new[] { "document", "example", "?" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToRetriveCommandDocumentationWithEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "example",
                "Description: This is an example description.",
                "Parameters:",
                "-sample (SampleEnum): This parameter is Required and must be one of the following (EnumOne, EnumTwo, EnumThree)."
            };
            Processor.ProcessArguments(new[] { "execute", "example", "?" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToRetriveCommandDocumentationWithListOfEnum()
        {
            mockConsole.Clear();
            var consoleLines = new List<string>
            {
                "list",
                "Description: This is an example description.",
                "Parameters:",
                "-values (List of SampleEnum): This parameter is Required and must be a collection of one of the following (EnumOne, EnumTwo, EnumThree).",
                "-something (Int32): This parameter is Required."
            };
            Processor.ProcessArguments(new[] { "execute", "list", "?" });
            var temp = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines, true);
            Assert.IsTrue(temp == expectedString);
        }
    }
}