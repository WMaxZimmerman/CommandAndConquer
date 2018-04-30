using CommandAndConquer.CLI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace CommandAndConquer.Tests.CLI
{
    [TestClass]
    public class AsyncTests : BaseCliTest
    {
        [Test]
        public void ApplicationWaitsForAnyWaitedTasksBeforExiting()
        {
            mockConsole.Clear();
            Processor.ProcessArguments(new[] { "execute", "longRunning", $"{argPre}firstNum", "5", $"{argPre}secondNum", "9" });
            var temp = mockConsole.ToString();
            NUnit.Framework.Assert.IsTrue(temp.Length > 0);
        }
    }
}
