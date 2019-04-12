using Microsoft.VisualStudio.TestTools.UnitTesting;
using JohnsonAndNEHAlgorithms.BackEnd.Simulations;
using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;
using System.Linq;
using UnitTestProject.Mocks;

namespace UnitTestProject
{
    [TestClass]
    public class ConfigurationTests
    {
        //Setup
        private Configuration algorithm;

        public ConfigurationTests()
        {
            algorithm = new ConfigurationMock();
            algorithm.GenerateConfiguration(new List<Machine>
            {
                new Machine(new List<Task>
                {
                    new Task(0, 1), // 1
                    new Task(1, 2), // 3
                    new Task(2, 3), // 6
                    new Task(3, 8) // 14
                }),
                new Machine(new List<Task>
                {
                    new Task(0, 3), // 4
                    new Task(1, 1), // 5
                    new Task(2, 2), // 8
                    new Task(3, 1) // 15
                }),
                new Machine(new List<Task>
                {
                    new Task(0, 1), // 5
                    new Task(1, 2), // 7
                    new Task(2, 1), // 9
                    new Task(3, 2) // 17
                })
            });
        }

        [TestMethod]
        public void ConfigurationGeneratesCorrectConfiguration()
        {
            //Expectation
            int firstMachineCmaxValue = 14;
            int secondMachineCmaxValue = 15;
            int lastMachineCmaxValue = 17;

            //Test
            Assert.AreEqual(firstMachineCmaxValue, algorithm.ConfiguredMachines[0].Tasks.Last().TaskStop);
            Assert.AreEqual(secondMachineCmaxValue, algorithm.ConfiguredMachines[1].Tasks.Last().TaskStop);
            Assert.AreEqual(lastMachineCmaxValue, algorithm.ConfiguredMachines[2].Tasks.Last().TaskStop);
        }
    }
}
