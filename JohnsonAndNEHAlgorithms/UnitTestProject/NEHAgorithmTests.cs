using Microsoft.VisualStudio.TestTools.UnitTesting;
using JohnsonAndNEHAlgorithms.BackEnd.Simulations;
using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class NEHAgorithmTests
    {
        //Setup
        AlgorithmNEH algorithm;
        readonly List<Machine> rawMachines;

        public NEHAgorithmTests()
        {
            algorithm = new AlgorithmNEH();
            rawMachines = rawMachines = new List<Machine>
            {
                new Machine(new List<Task>
                {
                    new Task(0, 1),
                    new Task(1, 9),
                    new Task(2, 7),
                    new Task(3, 4)
                }),
                new Machine(new List<Task>
                {
                    new Task(0, 3),
                    new Task(1, 3),
                    new Task(2, 8),
                    new Task(3, 8)
                }),
                new Machine(new List<Task>
                {
                    new Task(0, 8),
                    new Task(1, 5),
                    new Task(2, 6),
                    new Task(3, 7)
                })
            };

            //Action
            algorithm.GenerateConfiguration(rawMachines);
        }

        [TestMethod]
        public void MachinesAreNotTheSameReference()
        {
            //Test
            for (int i = 0; i < algorithm.ConfiguredMachines.Count; i++)
            {
                Assert.AreNotSame(algorithm.ConfiguredMachines[i], algorithm.bestMachineConfiguration[i]);
            }
        }

        [TestMethod]
        public void TasksAreNotTheSameReference()
        {
            //Test
            for (int i = 0; i < algorithm.ConfiguredMachines.Count; i++)
            {
                for (int j = 0; j < algorithm.ConfiguredMachines[i].Tasks.Count; j++)
                {
                    Assert.AreNotSame(algorithm.ConfiguredMachines[i].Tasks[j], algorithm.bestMachineConfiguration[i].Tasks[j]);
                }
            }
        }

        [TestMethod]
        public void NEHGeneratesFastestPossibleConfiguration()
        {
            //Expectation
            int expectedCmaxValue = 32; 

            //Test
            Assert.AreEqual(expectedCmaxValue, algorithm.ConfiguredMachines.Last().Tasks.Last().TaskStop);
        }
    }
}
