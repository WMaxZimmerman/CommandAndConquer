using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class BaseCliTest
    {
        protected StringWriter consoleMock;
        protected StringBuilder mockConsole = new StringBuilder();
        protected const string NewLine = "\r\n";

        [SetUp]
        public void Init()
        {
            consoleMock = new StringWriter(mockConsole);
            Console.SetOut(consoleMock);
        }

        protected string ConvertConsoleLinesToString(List<string> lines, bool startingNewLine = false, bool endingNewLine = true)
        {
            var consoleString = string.Join(NewLine, lines);
            if (endingNewLine) consoleString += NewLine;
            if (startingNewLine) consoleString = NewLine + consoleString;
            return consoleString;
        }
    }
}
