using System.Collections.Generic;

namespace CommandAndConquer.CLI.Models
{
    /// <summary>
    /// Represents processed arguments.
    /// </summary>
    public class ProcessedArguments
    {
        /// <summary>
        /// Controller.
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Command.
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// List of arguments.
        /// </summary>
        public List<CommandLineArgument> Arguments { get; set; }
        /// <summary>
        /// If <c>true</c>, means help has been requested, <c>false</c> otherwise.
        /// </summary>
        public bool IsHelpCall { get; set; }

        /// <summary>
        /// Initializes an instance of processed arguments.
        /// </summary>
        public ProcessedArguments()
        {
            Controller = null;
            Command = null;
            Arguments = new List<CommandLineArgument>();
        }
    }
}
