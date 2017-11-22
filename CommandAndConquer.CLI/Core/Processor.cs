using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.CLI.Models;

namespace CommandAndConquer.CLI.Core
{
    public static class Processor
    {
        private const string helpString = "?";

        public static void ProcessArguments(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Please enter a controller.  Use '{helpString}' to see available controllers.");
                return;
            }

            var controllers = GetControllers(Assembly.GetCallingAssembly());
            var arguments = ProcessArgs(args);

            if (arguments.IsHelpCall)
            {
                var controller = controllers.FirstOrDefault(c => c.Name == arguments.Controller);
                if (controller == null)
                {
                    foreach (var c in controllers)
                    {
                        c.OutputDocumentation();
                    }
                    return;
                }

                controller.DocumentCommand(arguments.Command);
            }
            else
            {
                var controller = controllers.FirstOrDefault(c => c.Name == arguments.Controller);
                if (controller == null)
                {
                    Console.WriteLine($"'{args[0]}' is not a valid controller.  Use '{helpString}' to see available controllers.");
                    return;
                }

                controller.ExecuteCommand(arguments.Command, arguments.Arguments);
            }
        }

        private static List<Controller> GetControllers(Assembly callingAssembly)
        {
            var controllerList = callingAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace.EndsWith(".Controllers") && Attribute.GetCustomAttributes(t).Any(a => a is CliController))
                .Select(t => new Controller(t));
            
            return controllerList.ToList();
        }

        private static ProcessedArguments ProcessArgs(string[] args)
        {
            var processedArguments = new ProcessedArguments();

            if (args.Length == 0) return processedArguments;

            processedArguments.IsHelpCall = args[args.Length - 1] == helpString;
            processedArguments.Controller = TryGetArg(args, 0);
            processedArguments.Command = TryGetArg(args, 1);

            if (args.Length > 2 && !processedArguments.IsHelpCall)
            {
                var argsList = args.ToList();
                argsList.RemoveRange(0, 2);
                processedArguments.Arguments = SetArguments(argsList);
            }

            return processedArguments;
        }

        private static string TryGetArg(string[] args, int index)
        {
            try
            {
                var arg = args[index];
                return arg == helpString ? null : arg;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static List<CommandLineArgument> SetArguments(IEnumerable<string> args)
        {
            var arguments = new List<CommandLineArgument>();
            CommandLineArgument tempArg = null;

            foreach (var argument in args)
            {
                if (argument.StartsWith("-"))
                {
                    if (tempArg != null) arguments.Add(tempArg);
                    tempArg = new CommandLineArgument
                    {
                        Command = argument.Substring(1),
                        Order = arguments.Count + 1
                    };
                }
                else
                {
                    tempArg.Value = argument;
                }
            }
            arguments.Add(tempArg);

            return arguments;
        }
    }
}
