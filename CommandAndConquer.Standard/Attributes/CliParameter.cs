using System;

namespace CommandAndConquer.Standard.Attributes
{
    /// <summary>
    /// Documents a command parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CliParameter : Attribute
    {
        public char Alias { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Initializes the parameter attribute.
        /// </summary>
        /// <param name="alias">Single-char alias for the parameter.</param>
        /// <param name="description">Parameter description.</param>
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
