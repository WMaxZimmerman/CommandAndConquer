using System.Collections.Generic;

namespace CommandAndConquer.Standard.Models
{
    /// <summary>
    /// Represents a command line argument.
    /// </summary>
    public class CommandLineArgument
    {
        /// <summary>
        /// Command.
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// Value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Order.
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// List of values.
        /// </summary>
        public List<string> Values { get; set; }

        public CommandLineArgument()
        {
            Values = new List<string>();
        }
    }
}
