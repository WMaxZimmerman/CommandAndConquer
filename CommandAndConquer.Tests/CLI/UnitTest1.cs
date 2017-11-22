using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class UnitTest1
    {
        private StringWriter consoleMock;
        private string mockConsole;
        private StringBuilder sb = new StringBuilder();

        [SetUp]
        public void Init()
        {
            mockConsole = "";
            //consoleMock = new StreamWriter(mockConsole);
            consoleMock = new StringWriter(sb);
            Console.SetOut(consoleMock);
        }

        [Test]
        public void TestMethod1()
        {
            var testString = "test line";
            var newLine = "\r\n";
            Console.WriteLine(testString);
            Console.WriteLine(testString);
            var temp = sb.ToString();
            Assert.IsTrue(temp == (testString + newLine + testString + newLine));
        }
    }
}
