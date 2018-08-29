using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Attributes;
using CommandAndConquer.CLI.Core;

namespace CommandAndConquer.CLI.Models
{
    /// <summary>
    /// Represents a controller.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Controller's class.
        /// </summary>
        public Type ClassType { get; set; }
        /// <summary>
        /// List of methods in the controller.
        /// </summary>
        public List<CommandMethod> Methods { get; set; }
        public CommandMethod DefaultMethod { get; set; }

        /// <summary>
        /// Initializates an instance of the controller.
        /// </summary>
        /// <param name="type">Controller's type.</param>
        public Controller(Type type)
        {
            ClassType = type;
            Name = GetControllerName();
            Methods = ClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand))
                .Select(c => new CommandMethod(c)).ToList();

            Methods.AddRange(ClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand))
                .Select(c => new CommandMethod(c)).ToList());

            DefaultMethod = ClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                      .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliDefaultCommand))
                                      .Select(c => new CommandMethod(c)).FirstOrDefault();

            Methods.Add(DefaultMethod);
        }

        /// <summary>
        /// Executes a command in the controller.
        /// </summary>
        /// <param name="commandName">Command name.</param>
        /// <param name="args">Command parameters.</param>
        /// <returns><c>true</c> if command executed correctly, <c>false</c> otherwise.</returns>
        public bool ExecuteCommand(string commandName, List<CommandLineArgument> args)
        {
            var command = Methods.FirstOrDefault(c => c.Name == commandName);
            if (command == null)
            {
                if(DefaultMethod != null) return DefaultMethod.Invoke(args);

                Console.WriteLine($"'{commandName}' is not a valid command.  Use '{Settings.HelpString}' to see available commands.");
                return false;
            }

            return command.Invoke(args);
        }

        /// <summary>
        /// Prints controler documentation.
        /// </summary>
        public void OutputDocumentation()
        {
            var attrs = Attribute.GetCustomAttributes(ClassType);
            foreach (var attr in attrs)
            {
                if (!(attr is CliController)) continue;
                var a = (CliController)attr;

                Console.WriteLine($"{a.Name} - {a.Description}");
            }
        }

        /// <summary>
        /// Prints command documentation.
        /// </summary>
        /// <param name="commandName">Command name.</param>
        public void DocumentCommand(string commandName)
        {
            var command = Methods.FirstOrDefault(c => c.Name == commandName);
            if (command == null)
            {
                foreach (var method in Methods)
                {
                    method.OutputDocumentation();
                }
                return;
            }

            command.OutputDocumentation();
        }

        private string GetControllerName()
        {
            var attribute = (CliController)Attribute.GetCustomAttributes(ClassType)
                .FirstOrDefault(a => a is CliController);

            return attribute?.Name;
        }
    }
}
