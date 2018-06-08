using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandAndConquer.CLI.Core;
using CommandAndConquer.CLI.Models;
using CommandAndConquer.Tests.Controllers;
using NUnit.Framework;

namespace CommandAndConquer.Tests.HelpersTests
{
    [TestFixture]
    public class ProcessorTests
    {
        [Test]
        public void GetAllControllersCorectlyReturnsAllControllers()
        {
            var actualControllers = Processor.GetAllControllers(Assembly.GetExecutingAssembly()).ToList();
            var expectedControllers = new List<ControllerVm>
            {
                new ControllerVm(typeof(DocumentationController)),
                new ControllerVm(typeof(ExecutionController))
            };

            Assert.AreEqual(expectedControllers.Count, actualControllers.Count);

            for (var i = 0; i < expectedControllers.Count; i++)
            {
                var expectedController = expectedControllers[i];
                var actualController = actualControllers[i];

                Assert.AreEqual(expectedController.Name, actualController.Name);

                var expectedMethods = expectedController.Methods;
                var actualMethods = actualController.Methods;

                Assert.AreEqual(expectedMethods.Count, actualMethods.Count);

                for (var j = 0; j < expectedMethods.Count; j++)
                {
                    var expectedMethod = expectedMethods[j];
                    var actualMethod = actualMethods[j];

                    Assert.AreEqual(expectedMethod.Name, actualMethod.Name);
                }
            }
        }
    }
}
