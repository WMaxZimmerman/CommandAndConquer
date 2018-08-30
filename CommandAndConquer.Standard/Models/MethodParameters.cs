using System.Collections.Generic;

namespace CommandAndConquer.Standard.Models
{
    /// <summary>
    /// Represents method parameters.
    /// </summary>
    public class MethodParameters
    {
        /// <summary>
        /// Lists of parameters.
        /// </summary>
        public List<object> Parameters { get; set; }
        /// <summary>
        /// List of errors.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Initializes an instance of method parameters.
        /// </summary>
        public MethodParameters()
        {
            Parameters = new List<object>();
            Errors = new List<string>();
        }
    }
}
