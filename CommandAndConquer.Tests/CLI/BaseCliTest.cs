using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using CommandAndConquer.CLI.Core;
using Xunit;

namespace CommandAndConquer.Tests.CLI
{
    public class BaseCliTest
    {
        protected StringWriter consoleMock;
        protected StringBuilder mockConsole = new StringBuilder();
        protected string helpString = Settings.HelpString;
        protected string argPre = Settings.ArgumentPrefix;

        public BaseCliTest()
        {
            consoleMock = new StringWriter(mockConsole);
            Console.SetOut(consoleMock);
            SetApplicationLoopEnabled(false);
            SetParamDetail("simple");
        }

        protected string ConvertConsoleLinesToString(List<string> lines, bool startingNewLine = false, bool endingNewLine = true)
        {
            var consoleString = string.Join(Environment.NewLine, lines);
            if (endingNewLine) consoleString += Environment.NewLine;
            if (startingNewLine) consoleString = Environment.NewLine + consoleString;
            return consoleString;
        }

        protected void SetApplicationLoopEnabled(bool value)
        {
            UpdateConfigValue("applicationLoopEnabled", value.ToString());
        }

        protected void SetParamDetail(string detail)
        {
            UpdateConfigValue("paramDetail", detail);
        }

        private static void UpdateConfigValue(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appsettings");
        }
    }
}
