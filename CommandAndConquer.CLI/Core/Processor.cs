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

            var callingAssembly = Assembly.GetCallingAssembly();

            var controller = GetController(callingAssembly, args[0]);
            if (controller == null)
            {
                if (args[0] == "?") return;
                Console.WriteLine($"'{args[0]}' is not a valid controller.  Use '{helpString}' to see available controllers.");
            }

            if (args.Length == 1)
            {
                Console.WriteLine($"Please enter a command.  Use '{helpString}' to see available commands.");
                return;
            }

            var command = GetCommand(controller, args[1]);
            if (command == null)
            {
                if (args[1] == "?") return;
                Console.WriteLine($"'{args[1]}' is not a valid command.  Use '{helpString}' to see available commands.");
            }

            var argsList = args.ToList();
            argsList.RemoveRange(0, 2);

            var commandArguments = SetArguments(argsList);
            var paramList = GetParams(command, commandArguments);

            try
            {
                command.Invoke(null, BindingFlags.Static, null, paramList.ToArray(), null);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
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

        private static Type GetController(Assembly callingAssembly, string name)
        {
            var classList = callingAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace.EndsWith(".Controllers") && Attribute.GetCustomAttributes(t).Any(a => a is CliController));

            if (name == helpString) Console.WriteLine("The possible controllers to use are:");

            foreach (var t in classList)
            {
                var attrs = Attribute.GetCustomAttributes(t);

                foreach (var attr in attrs)
                {
                    if (!(attr is CliController)) continue;
                    var a = (CliController)attr;

                    if (name == helpString)
                    {
                        Console.WriteLine($"{a.Name} - {a.Description}");
                    }
                    else
                    {
                        if (a.Name == name) return t;
                    }
                }
            }

            return null;
        }

        private static MethodInfo GetCommand(Type controller, string commandName)
        {
            var commands = controller.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand));

            if (commandName == helpString)
            {
                Console.WriteLine("The possible commands to use are:");
            }

            foreach (var command in commands)
            {
                var attrs = Attribute.GetCustomAttributes(command);

                foreach (var attr in attrs)
                {
                    if (!(attr is CliCommand)) continue;
                    var a = (CliCommand)attr;

                    if (commandName == helpString)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{a.Name}");
                        Console.WriteLine($"Description: {a.Description}");
                        var commandParams = command.GetParameters();
                        if (commandParams.Length > 0)
                        {
                            Console.WriteLine($"Parameters:");
                            foreach (var cp in commandParams)
                            {
                                var priorityString = cp.HasDefaultValue ? "Optional" : "Required";
                                var temp = Nullable.GetUnderlyingType(cp.ParameterType);
                                var typeName = temp == null ? cp.ParameterType.Name : temp.Name;
                                Console.WriteLine($"-{cp.Name} ({typeName}): This parameter is {priorityString}.");
                            }
                        }
                    }
                    else
                    {
                        if (a.Name == commandName) return command;
                    }
                }
            }

            return null;
        }

        private static object[] GetParams(MethodInfo command, List<CommandLineArgument> args)
        {
            var paramList = new List<object>();

            foreach (var parameter in command.GetParameters())
            {
                var wasFound = false;
                foreach (var argument in args)
                {
                    if (argument.Command.ToLower() != parameter.Name.ToLower()) continue;
                    object paramValue;

                    if (Nullable.GetUnderlyingType(parameter.ParameterType) != null)
                    {
                        var underType = Nullable.GetUnderlyingType(parameter.ParameterType);
                        paramValue = Convert.ChangeType(argument.Value, underType);
                    }
                    else
                    {
                        paramValue = Convert.ChangeType(argument.Value, parameter.ParameterType);
                    }

                    paramList.Add(paramValue);
                    wasFound = true;
                }

                if (!wasFound) paramList.Add(null);
            }

            return paramList.ToArray();
        }
    }
}
