using System.Collections.Generic;

namespace CommandAndConquer.CLI.Models
{
    public class ProcessedArguments
    {
        public string Controller { get; set; }
        public string Command { get; set; }
        public List<CommandLineArgument> Arguments { get; set; }
        public bool IsHelpCall { get; set; }

        public ProcessedArguments()
        {
            Controller = null;
            Command = null;
            Arguments = new List<CommandLineArgument>();
        }
    }
}
