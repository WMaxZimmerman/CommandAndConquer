using System;

namespace CommandAndConquer.Standard.Attributes
{
    /// <summary>Indicates a command.</summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CliCommand : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Initializes the command attribute.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="description">Command description.</param>
        public CliCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
