using System.Collections.Generic;

namespace CommandAndConquer.CLI.Core
{
    public static class CommandLine
    {
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
