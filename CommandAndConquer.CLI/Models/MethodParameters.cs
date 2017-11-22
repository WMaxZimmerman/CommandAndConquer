using System.Collections.Generic;

namespace CommandAndConquer.CLI.Models
{
    public class MethodParameters
    {
        public List<object> Parameters { get; set; }
        public List<string> Errors { get; set; }

        public MethodParameters()
        {
            Parameters = new List<object>();
            Errors = new List<string>();
        }
    }
}
