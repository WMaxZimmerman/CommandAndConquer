using System;

namespace CommandAndConquer.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CliParameter: Attribute
    {
        public string Alias { get; set; }
        public string Description { get; set; }

        public CliParameter(string alias)
        {
            Alias = alias;
        }
    }
}
