using System.Collections.Generic;

namespace CommandAndConquer.CLI.Models
{
    public class CommandLineArgument
    {
        public string Command { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public List<string> Values { get; set; }

        public CommandLineArgument()
        {
            Values = new List<string>();
        }
    }
}
