using System;
using System.Configuration;

namespace CommandAndConquer.CLI.Core
{
    public static class Settings
    {
        /// <summary>
        /// Sets the string that will show help.
        /// </summary>
        public static string HelpString = ConfigurationManager.AppSettings["helpString"] ?? "?";
        /// <summary>
        /// Prefix for arguments.
        /// </summary>
        public static string ArgumentPrefix = ConfigurationManager.AppSettings["argumentPrefix"] ?? "--";
        /// <summary>
        /// If set to <c>"detailed"</c>, shows more information on help.
        /// </summary>
        public static string ParamDetail = ConfigurationManager.AppSettings["paramDetail"] ?? "simple";
        /// <summary>
        /// Input indication when application loop is enabled.
        /// </summary>
        public static string InputIndicator = ConfigurationManager.AppSettings["inputIndicator"] ?? ">";
        /// <summary>
        /// Command to exit from the application loop.
        /// </summary>
        public static string ExitString = ConfigurationManager.AppSettings["exitString"] ?? "exit";
        /// <summary>
        /// If set to <c>true</c> and there are no arguments, creates a command shell.
        /// </summary>
        public static bool ApplicationLoopEnabled = ConfigurationManager.AppSettings["applicationLoopEnabled"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["applicationLoopEnabled"]);
    }
}
