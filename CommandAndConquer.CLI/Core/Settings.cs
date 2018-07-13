using System;
using System.Configuration;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.CLI.Core
{
    public static class Settings
    {
        public static string HelpString = ConfigurationManager.AppSettings["helpString"] ?? "?";
        public static string ArgumentPrefix = ConfigurationManager.AppSettings["argumentPrefix"] ?? "--";
        public static string ParamDetail = ConfigurationManager.AppSettings["paramDetail"] ?? "simple";
        public static string InputIndicator = ConfigurationManager.AppSettings["inputIndicator"] ?? ">";
        public static string ExitString = ConfigurationManager.AppSettings["exitString"] ?? "exit";
        public static bool ApplicationLoopEnabled = ConfigurationManager.AppSettings["applicationLoopEnabled"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["applicationLoopEnabled"]);
        /// <summary>
        /// Separator between <see cref="CliController.Name"/> and <see cref="CliController.Description"/>
        /// </summary>
        public static string ControllerSeparator = ConfigurationManager.AppSettings["controllerSeparator"] ?? " - ";
        /// <summary>
        /// Enables a shorter description more in line with common command line interfaces
        /// </summary>
        public static bool UseShortDescription = ConfigurationManager.AppSettings["useShortDescription"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["useShortDescription"]);
        /// <summary>
        /// Provides the indentation at the start of a line with parameter help
        /// </summary>
        public static string ParameterIndentation = ConfigurationManager.AppSettings["parameterIndentation"] ?? "  ";
        /// <summary>
        /// Provides the separator between a parameter and its description
        /// </summary>
        public static string ParameterSeparator = ConfigurationManager.AppSettings["parameterSeparator"] ?? "\t";
    }
}
