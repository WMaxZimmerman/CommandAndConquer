using System;

namespace CommandAndConquer.Standard.Attributes
{
    /// <summary>
    /// Indicates a class that contains several commands.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CliController : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Initializes the controller attribute.
        /// </summary>
        /// <param name="name">Controller name.</param>
        /// <param name="description">Controller description.</param>
        public CliController(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
