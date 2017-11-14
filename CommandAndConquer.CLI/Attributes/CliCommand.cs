using System;

namespace CommandAndConquer.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CliCommand: Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CliCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
