using System;
using System.Configuration;

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
    }
}
