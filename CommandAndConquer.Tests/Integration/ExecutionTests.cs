using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xunit;

namespace CommandAndConquer.Tests.Integration
{
    public class ExecutionTests
    {
        private string _directory;

        public ExecutionTests()
        {
            _directory = Directory.GetParent((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath).Parent.Parent.Parent.FullName + @"\CommandAndConquer.TestCLI\bin\Debug\";
        }

        [Fact]
        public void ApplicationTerminatesWithFailedStatusWhenInvalidController()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" meth add --firstNum 1 --secondNum 2";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.Equal(1, proc.ExitCode);
        }

        [Fact]
        public void ApplicationTerminatesWithFailedStatusWhenMissingCommand()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.Equal(1, proc.ExitCode);
        }

        [Fact]
        public void ApplicationTerminatesWithFailedStatusWhenInvalidParameters()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math add --firstNom 1 --secondNum 2";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.Equal(1, proc.ExitCode);
        }

        [Fact]
        public void ApplicationTerminatesWithFailedStatusWhenMissingParameters()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math add --firstNum 1";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.Equal(1, proc.ExitCode);
        }
    }
}
