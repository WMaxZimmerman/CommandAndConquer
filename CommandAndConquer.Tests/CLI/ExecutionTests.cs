using CommandAndConquer.CLI.Core;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestFixture]
    public class ExecutionTests: BaseCliTest
    {
        [Test]
        public void AbleToCallCommandWithoutOptionalParameter()
        {
            mockConsole.Clear();
            var testString = "test line";
            Processor.ProcessArguments(new[] {"test", "example", "-required", "bleh"});
            var temp = mockConsole.ToString();
            var expectedString = "bleh 0" + NewLine;
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithOptionalParameter()
        {
            mockConsole.Clear();
            var testString = "test line";
            Processor.ProcessArguments(new[] { "test", "example", "-required", "bleh", "-opt", "5" });
            var temp = mockConsole.ToString();
            var expectedString = "bleh 5" + NewLine;
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToCallCommandWithEnumParameter()
        {
            mockConsole.Clear();
            var testString = "test line";
            Processor.ProcessArguments(new[] { "test2", "example", "-sample", "EnumOne" });
            var temp = mockConsole.ToString();
            var expectedString = "EnumOne" + NewLine;
            Assert.IsTrue(temp == expectedString);
        }

        [Test]
        public void AbleToHandleBadCallToCommandWithEnumParameter()
        {
            mockConsole.Clear();
            var testString = "test line";
            Processor.ProcessArguments(new[] { "test2", "example", "-sample", "Enum" });
            var temp = mockConsole.ToString();
            var expectedString = "The specified value of 'Enum' is not valid for the parameter 'sample'." + NewLine;
            Assert.IsTrue(temp == expectedString);
        }
    }
}
