using System;
using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CLI
{
    public class AsyncTests : BaseCliTest
    {
        [Fact]
        public void ApplicationWaitsForAnyWaitedTasksBeforExiting()
        {
            mockConsole.Clear();
            Processor.ProcessArguments(new[] { "execute", "longRunning", $"{argPre}firstNum", "5", $"{argPre}secondNum", "9" });
            var temp = mockConsole.ToString();
            Assert.True(temp.Length > 0);
        }
    }
}
