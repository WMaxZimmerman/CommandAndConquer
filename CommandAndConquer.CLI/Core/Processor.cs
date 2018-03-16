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
        public static void ProcessArguments(string[] args)
        {
            if (args.Length == 0)
            {
                if (Settings.ApplicationLoopEnabled)
                {
                    ApplicationLoop(Assembly.GetCallingAssembly());
                }
                else
                {
                    Console.WriteLine($"Please enter a controller.  Use '{Settings.HelpString}' to see available controllers.");
                }
                return;
            }

            var wasSuccess = ProcessArguments(args, Assembly.GetCallingAssembly());
            if (wasSuccess == false)
            {
                Environment.Exit(1);
            }
        }

        private static void ApplicationLoop(Assembly projectAssembly)
        {
            Console.Write(Settings.InputIndicator + " ");
            var input = CommandLine.GetCommandLineArgs(Console.ReadLine());

            while (input.Length == 0 || input[0] != Settings.ExitString)
            {
                if (input.Length > 0) Processor.ProcessArguments(input, projectAssembly);
                Console.WriteLine();
                Console.Write(Settings.InputIndicator + " ");
                input = CommandLine.GetCommandLineArgs(Console.ReadLine());
            }
        }

        private static bool ProcessArguments(string[] args, Assembly ProjectAssembly)
        {
            var controllers = GetControllers(ProjectAssembly);
            
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
                    return true;
                }

                controller.DocumentCommand(arguments.Command);
                return true;
            }
            else
            {
                var controller = controllers.FirstOrDefault(c => c.Name == arguments.Controller);
                if (controller == null)
                {
                    Console.WriteLine($"'{args[0]}' is not a valid controller.  Use '{Settings.HelpString}' to see available controllers.");
                    return false;
                }

                var invokeSuccessful = controller.ExecuteCommand(arguments.Command, arguments.Arguments);
                return invokeSuccessful;
            }
        }

        private static List<Controller> GetControllers(Assembly callingAssembly)
        {
            var controllerList = callingAssembly.GetTypes()
                .Where(t => Attribute.GetCustomAttributes(t).Any(a => a is CliController))
                .Select(t => new Controller(t));
            
            return controllerList.ToList();
        }

        private static ProcessedArguments ProcessArgs(string[] args)
        {;
            var processedArguments = new ProcessedArguments();

            if (args.Length == 0) return processedArguments;

            processedArguments.IsHelpCall = args[args.Length - 1] == Settings.HelpString;
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
                return arg == Settings.HelpString ? null : arg;
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
                if (argument.StartsWith(Settings.ArgumentPrefix))
                {
                    if (tempArg != null) arguments.Add(tempArg);
                    tempArg = new CommandLineArgument
                    {
                        Command = argument.Replace(Settings.ArgumentPrefix, ""),
                        Order = arguments.Count + 1
                    };
                }
                else
                {
                    if (tempArg == null) continue;
                    tempArg.Values.Add(argument);
                }
            }
            arguments.Add(tempArg);

            return arguments;
        }
    }
}
