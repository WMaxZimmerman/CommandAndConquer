using System.Collections.Generic;

namespace CommandAndConquer.Standard.Core
{
    public static class CommandLine
    {
        /// <summary>
        /// Splits a string in several arguments.
        /// </summary>
        /// <param name="argString">String to split.</param>
        /// <returns>List of arguments.</returns>
        public static string[] GetCommandLineArgs(string argString)
        {
            argString = argString.Trim();
            var argList = new List<string>();
            var inQuotes = false;
            var tempString = "";

            foreach (var c in argString)
            {
                if (c == ' ' && inQuotes == false)
                {
                    if (tempString != "")
                    {
                        argList.Add(tempString);
                        tempString = "";
                    }
                }
                else if (c == '\"' && inQuotes == true)
                {
                    argList.Add(tempString);
                    inQuotes = false;
                    tempString = "";
                }
                else if (c == '\"')
                {
                    if (tempString != "")
                    {
                        argList.Add(tempString);
                        tempString = "";
                    }
                    inQuotes = true;
                }
                else
                {
                    tempString += c;
                }
            }
            if (tempString != "")
            {
                argList.Add(tempString);
            }

            return argList.ToArray();
        }
    }
}
