using System;

namespace CommandAndConquer.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CliController: Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CliController(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
