using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CommandAndConquer.Tests.Integration
{
    [TestFixture]
    public class ExecutionTests
    {
        private string _directory;

        [SetUp]
        public void Init()
        {
            _directory = Directory.GetParent((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath).Parent.Parent.Parent.FullName + @"\CommandAndConquer.TestCLI\bin\Debug\";
        }

        [Test]
        public void ApplicationTerminatesWithFailedStatusWhenInvalidController()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" meth add --firstNum 1 --secondNum 2";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.AreEqual(1, proc.ExitCode);
        }

        [Test]
        public void ApplicationTerminatesWithFailedStatusWhenMissingCommand()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.AreEqual(1, proc.ExitCode);
        }

        [Test]
        public void ApplicationTerminatesWithFailedStatusWhenInvalidParameters()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math add --firstNom 1 --secondNum 2";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.AreEqual(1, proc.ExitCode);
        }

        [Test]
        public void ApplicationTerminatesWithFailedStatusWhenMissingParameters()
        {
            string strCmdText;
            strCmdText = $"/C \"{_directory}CommandAndConquer.TestCLI.exe\" math add --firstNum 1";
            var proc = Process.Start("CMD.exe", strCmdText);
            proc.WaitForExit();

            Assert.AreEqual(1, proc.ExitCode);
        }
    }
}
