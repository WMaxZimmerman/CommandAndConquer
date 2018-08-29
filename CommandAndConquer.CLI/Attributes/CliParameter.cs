using System;

namespace CommandAndConquer.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CliParameter: Attribute
    {
        public char Alias { get; set; }
        public string Description { get; set; }

        public CliParameter(char alias, string description = null)
        {
            Alias = alias;
            Description = description;
        }

        public CliParameter(string description = null)
        {
            Description = description;
        }
    }
}
