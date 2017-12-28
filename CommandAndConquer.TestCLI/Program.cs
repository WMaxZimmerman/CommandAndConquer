using CommandAndConquer.CLI.Core;

namespace CommandAndConquer.TestCLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Processor.ProcessArguments(args);
        }
    }
}
