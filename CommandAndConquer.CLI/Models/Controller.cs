using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Attributes;

namespace CommandAndConquer.CLI.Models
{
    public class Controller
    {
        public string Name { get; set; }
        public Type ClassType { get; set; }
        public List<CommandMethod> Methods { get; set; }

        public Controller(Type type)
        {
            ClassType = type;
            Name = ClassType.Name;
            Methods = ClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand))
                .Select(c => new CommandMethod(c)).ToList();
        }

        public void ExecuteCommand(string commandName, List<CommandLineArgument> args)
        {
            var command = Methods.FirstOrDefault(c => c.Name == commandName);
            if (command == null)
            {
                Console.WriteLine($"'{commandName}' is not a valid command.  Use '?' to see available commands.");
                return;
            }

            command.Invoke(args);
        }

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
    }
}
