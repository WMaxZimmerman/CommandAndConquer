using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.Standard.Attributes;

namespace CommandAndConquer.Standard.Models
{
    public class ControllerVm
    {
        public string Name { get; set; }
        public List<CommandMethodVm> Methods { get; set; }
        private Type ClassType { get; set; }

        public ControllerVm(Type type)
        {
            ClassType = type;
            Name = GetControllerName();
            Methods = ClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand))
                .Select(c => new CommandMethodVm(c)).ToList();

            Methods.AddRange(ClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand))
                .Select(c => new CommandMethodVm(c)).ToList());
        }

        private string GetControllerName()
        {
            var attribute = (CliController)Attribute.GetCustomAttributes(ClassType)
                .FirstOrDefault(a => a is CliController);

            return attribute?.Name;
        }
    }
}
