using System.Configuration;

namespace CommandAndConquer.CLI.Core
{
    public static class Settings
    {
        public static string HelpString = ConfigurationManager.AppSettings["helpString"] ?? "?";
        public static string ArgumentPrefix = ConfigurationManager.AppSettings["argumentPrefix"] ?? "--";
    }
}
