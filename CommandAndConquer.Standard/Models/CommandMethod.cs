using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.Standard.Attributes;
using CommandAndConquer.Standard.Core;

namespace CommandAndConquer.Standard.Models
{
    /// <summary>
    /// Represents a command method.
    /// </summary>
    public class CommandMethod
    {
        /// <summary>
        /// Command name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Command information.
        /// </summary>
        public MethodInfo Info { get; set; }
        /// <summary>
        /// Command parameters.
        /// </summary>
        public MethodParameters Parameters { get; set; }

        /// <summary>
        /// Initializes an instance of a command method.
        /// </summary>
        /// <param name="info">Command information.</param>
        public CommandMethod(MethodInfo info)
        {
            Info = info;
            Name = GetCommandName();
        }

        /// <summary>
        /// Invokes a command.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns><c>true</c> if it the invocation succeeds, <c>false</c> otherwise.</returns>
        public bool Invoke(List<CommandLineArgument> args)
        {
            try
            {
                var paramList = GetParams(args);
                if (paramList == null) return false;

                if (Info.IsStatic)
                {
                    Info.Invoke(null, BindingFlags.Static, null, paramList, null);
                }
                else
                {
                    //Execute as if not static
                    var target = Activator.CreateInstance(Info.DeclaringType);
                    Info.Invoke(target, BindingFlags.Instance, null, paramList, null);
                }

                return true;
            }
            catch (TargetInvocationException e)
            {
                var inner = e.InnerException;
                Console.WriteLine("An error occurred while executing the command.");
                if (inner == null) return false;
                Console.WriteLine($"Message: {inner.Message}");
                Console.WriteLine($"Stack Trace: {inner.StackTrace.Trim()}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while attempting to execute the command.");
                Console.WriteLine("This is most likely due to invalid arguments.");
                Console.WriteLine($"Please verify the command usage with '{Settings.HelpString}' and try again.");
                return false;
            }
        }

        /// <summary>
        /// Sets method arguments.
        /// </summary>
        /// <param name="args">List of arguments.</param>
        public void SetParameters(List<CommandLineArgument> args)
        {
            var methodParams = new MethodParameters();

            foreach (var argument in args.Where(a => Info.GetParameters().All(p => p.Name != a.Command && p.GetCustomAttribute<CliParameter>()?.Alias.ToString() != a.Command)))
            {
                methodParams.Errors.Add($"The parameter '{argument.Command}' is not a valid parameter.");
            }

            foreach (var parameter in Info.GetParameters())
            {
                var wasFound = false;
                foreach (var argument in args)
                {
                    if (argument.Command.ToLower() != parameter.Name.ToLower())
                    {
                        var paramAttribute = parameter.GetCustomAttribute<CliParameter>();
                        if (paramAttribute != null)
                        {
                            if (paramAttribute.Alias.ToString().ToLower() != argument.Command.ToLower()) continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    wasFound = true;

                    var type = parameter.ParameterType;

                    if (typeof(IEnumerable).IsAssignableFrom(type) && type.Name != "String")
                    {
                        if (type.GetGenericArguments().Length <= 0)
                        {
                            var underType = type.GetElementType();
                            var listType = typeof(List<>).MakeGenericType(underType);
                            var list = (IList)Activator.CreateInstance(listType);

                            foreach (var value in argument.Values)
                            {
                                list.Add(GetParamValue(value, underType));
                            }

                            var array = Array.CreateInstance(underType, list.Count);
                            list.CopyTo(array, 0);

                            methodParams.Parameters.Add(array);
                        }
                        else
                        {
                            var underType = type.GenericTypeArguments[0];
                            var listType = typeof(List<>).MakeGenericType(underType);
                            var list = (IList)Activator.CreateInstance(listType);

                            foreach (var value in argument.Values)
                            {
                                list.Add(GetParamValue(value, underType));
                            }

                            methodParams.Parameters.Add(list);
                        }
                    }
                    else if (type.Name == "Boolean")
                    {
                        if (argument.Values.Count <= 0) argument.Values.Add("true");
                        dynamic val = GetParamValue(argument.Values[0], parameter.ParameterType);
                        methodParams.Parameters.Add(val);
                    }
                    else
                    {
                        dynamic val = GetParamValue(argument.Values[0], parameter.ParameterType);
                        methodParams.Parameters.Add(val);
                    }
                }

                if (!wasFound)
                {
                    if (parameter.HasDefaultValue)
                    {
                        methodParams.Parameters.Add(parameter.DefaultValue);
                    }
                    else
                    {
                        methodParams.Errors.Add($"The parameter '{parameter.Name}' must be specified.");
                    }
                }
            }

            Parameters = methodParams;
        }

        /// <summary>
        /// Outputs command documentation.
        /// </summary>
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

        private dynamic GetParamValue(string value, Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                var underType = Nullable.GetUnderlyingType(type);
                dynamic val = Convert.ChangeType(value, underType);
                return val;
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }

            dynamic pVal = Convert.ChangeType(value, type);
            return pVal;
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
            if (cp.HasDefaultValue && Settings.ParamDetail == "detailed") priorityString += $" with a default value of {cp.DefaultValue}";
            var type = cp.ParameterType;
            var isEnumerable = IsEnumerable(type);
            if (isEnumerable)
            {
                if (type.GetGenericArguments().Length <= 0)
                {
                    type = type.GetElementType();
                }
                else
                {
                    type = type.GenericTypeArguments[0];
                }
            }

            type = Nullable.GetUnderlyingType(type) ?? type;

            var typeString = $"{(isEnumerable ? "List of " : "")}{type.Name}";
            var aliasString = "";
            var descripitionString = "";
            if (cp.GetCustomAttribute<CliParameter>() != null)
            {
                if (cp.GetCustomAttribute<CliParameter>().Alias != default(char)) aliasString = $" | {Settings.ArgumentPrefix}{cp.GetCustomAttribute<CliParameter>().Alias}";
                if (cp.GetCustomAttribute<CliParameter>().Description != null) descripitionString = $"Description: {cp.GetCustomAttribute<CliParameter>().Description}";
            }
            var docString = $"{Settings.ArgumentPrefix}{cp.Name}{aliasString} ({typeString}): This parameter is {priorityString}";

            if (type.IsEnum)
            {
                var names = type.GetEnumNames();
                docString += $" and must be {(isEnumerable ? "a collection of " : "")}one of the following ({string.Join(", ", names)}).";
            }
            else
            {
                docString += ".";
            }

            Console.WriteLine(docString);
            if (descripitionString != "") Console.WriteLine(descripitionString);
        }

        private bool IsEnumerable(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type.Name != "String";
        }

        private string GetCommandName()
        {
            var attribute = (CliCommand)Attribute.GetCustomAttributes(Info)
                .FirstOrDefault(a => a is CliCommand);

            return attribute?.Name;
        }
    }
}
