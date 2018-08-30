using System;

namespace CommandAndConquer.Standard.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CliDefaultCommand : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CliDefaultCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
