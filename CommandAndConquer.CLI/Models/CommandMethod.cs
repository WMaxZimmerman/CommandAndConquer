using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.CLI.Models
{
    public class CommandMethod
    {
        public string Name { get; set; }
        public MethodInfo Info { get; set; }
        public MethodParameters Parameters { get; set; }

        public CommandMethod(MethodInfo info)
        {
            Info = info;
            Name = GetCommandName();
        }

        public void Invoke(List<CommandLineArgument> args)
        {
            var paramList = GetParams(args);
            if (paramList == null) return;

            try
            {
                Info.Invoke(null, BindingFlags.Static, null, paramList, null);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public void SetParameters(List<CommandLineArgument> args)
        {
            var methodParams = new MethodParameters();

            foreach (var argument in args.Where(a => Info.GetParameters().All(p => p.Name != a.Command)))
            {
                methodParams.Errors.Add($"The parameter {argument.Command} is not a valid parameter.");
            }

            foreach (var parameter in Info.GetParameters())
            {
                var wasFound = false;
                foreach (var argument in args)
                {
                    if (argument.Command.ToLower() != parameter.Name.ToLower()) continue;
                    wasFound = true;
                    object paramValue;

                    if (Nullable.GetUnderlyingType(parameter.ParameterType) != null)
                    {
                        var underType = Nullable.GetUnderlyingType(parameter.ParameterType);
                        paramValue = Convert.ChangeType(argument.Value, underType);
                    }
                    else if (parameter.ParameterType.IsEnum)
                    {
                        var t = parameter.ParameterType;
                        var options = t.GetEnumNames().ToList();

                        if (!options.Contains(argument.Value))
                        {
                            methodParams.Errors.Add($"The specified value of '{argument.Value}' is not valid for the parameter '{argument.Command}'.");
                            continue;
                        }
                        paramValue = Enum.Parse(t, argument.Value);
                    }
                    else
                    {
                        paramValue = Convert.ChangeType(argument.Value, parameter.ParameterType);
                    }

                    methodParams.Parameters.Add(paramValue);
                }

                if (!wasFound)
                {
                    if (parameter.HasDefaultValue)
                    {
                        methodParams.Parameters.Add(parameter.DefaultValue);
                    }
                    else
                    {
                        methodParams.Errors.Add($"The parameter {parameter.Name} must be specified.");
                    }
                }
            }

            Parameters = methodParams;
        }

        public void OutputDocumentation()
        {
            var attrs = Attribute.GetCustomAttributes(Info);
            
            foreach (var attr in attrs)
            {
                if (!(attr is CliCommand)) continue;
                var a = (CliCommand)attr;
            
                Console.WriteLine();
                Console.WriteLine($"{a.Name}");
                Console.WriteLine($"Description: {a.Description}");
                var commandParams = Info.GetParameters();
                if (commandParams.Length > 0)
                {
                    Console.WriteLine($"Parameters:");
                    foreach (var cp in commandParams)
                    {
                        OutputParameterDocumentation(cp);
                    }
                }
            }
        }

        private object[] GetParams(List<CommandLineArgument> args)
        {
            SetParameters(args);

            foreach (var error in Parameters.Errors)
            {
                Console.WriteLine(error);
            }

            return Parameters.Errors.Any() ? null : Parameters.Parameters.ToArray();
        }

        private void OutputParameterDocumentation(ParameterInfo cp)
        {
            var priorityString = cp.HasDefaultValue ? "Optional" : "Required";
            var type = Nullable.GetUnderlyingType(cp.ParameterType) ?? cp.ParameterType;
        
            if (type.IsEnum)
            {
                var names = type.GetEnumNames();
                Console.WriteLine($"-{cp.Name} (string): This parameter is {priorityString} and must be one of these following ({string.Join(",", names)}).");
            }
            else
            {
                var typeName = type.Name;
                Console.WriteLine($"-{cp.Name} ({typeName}): This parameter is {priorityString}.");
            }
        }

        private string GetCommandName()
        {
            var attribute = (CliCommand)Attribute.GetCustomAttributes(Info)
                .FirstOrDefault(a => a is CliCommand);

            return attribute?.Name;
        }
    }
}
