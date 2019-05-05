using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Core;
using CommandAndConquer.CLI.Models;
using CommandAndConquer.Tests.Controllers;
using Xunit;

namespace CommandAndConquer.Tests.HelpersTests
{
    public class ProcessorTests
    {
        [Fact]
        public void GetAllControllersCorectlyReturnsAllControllers()
        {
            var actualControllers = Processor.GetAllControllers(Assembly.GetExecutingAssembly()).ToList();
            var expectedControllers = new List<ControllerVm>
            {
                new ControllerVm(typeof(DefaultCommandController)),
                new ControllerVm(typeof(DocumentationController)),
                new ControllerVm(typeof(ExecutionController))
            };

            Assert.Equal(expectedControllers.Count, actualControllers.Count);

            for (var i = 0; i < expectedControllers.Count; i++)
            {
                var expectedController = expectedControllers[i];
                var actualController = actualControllers[i];

                Assert.Equal(expectedController.Name, actualController.Name);

                var expectedMethods = expectedController.Methods;
                var actualMethods = actualController.Methods;

                Assert.Equal(expectedMethods.Count, actualMethods.Count);

                for (var j = 0; j < expectedMethods.Count; j++)
                {
                    var expectedMethod = expectedMethods[j];
                    var actualMethod = actualMethods[j];

                    Assert.Equal(expectedMethod.Name, actualMethod.Name);
                }
            }
        }
    }
}
