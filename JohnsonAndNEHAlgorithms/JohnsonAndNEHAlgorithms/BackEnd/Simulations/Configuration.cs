using System;
using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;

namespace JohnsonAndNEHAlgorithms.BackEnd.Simulations
{
    public abstract class Configuration : IConfiguration
    {
        public List<Machine> ConfiguredMachines { get; protected set; } = new List<Machine>();

        public abstract void GenerateConfiguration(List<Machine> rawMachines);

        public void SimulateConfiguration()
        {
            SetTimeStartsToZero();
            for (int i = 0; i < ConfiguredMachines.Count - 1; i++)
            {
                Machine firstMachine = ConfiguredMachines[i];
                Machine secondMachine = ConfiguredMachines[i + 1];
                int taskListSize = firstMachine.Tasks.Count;
                int firstTaskStart = firstMachine.Tasks[0].TaskStart;

                if (firstTaskStart == 0)
                {
                    SimulateForFirstMachinePair(firstMachine, secondMachine, taskListSize);
                }
                else
                {
                    SimulateForNotFirstMachinePairs(firstMachine, secondMachine, taskListSize, firstTaskStart);
                }
            }
            return;
        }

        private void SetTimeStartsToZero()
        {
            foreach (var machine in ConfiguredMachines)
            {
                foreach (var task in machine.Tasks)
                {
                    task.TaskStart = 0;
                }
            }
        }

        private static void SimulateForFirstMachinePair(Machine firstMachine, Machine secondMachine, int taskListSize)
        {
            for (int j = 0; j < taskListSize; j++)
            {
                if (j == 0)
                {
                    secondMachine.Tasks[j].TaskStart = firstMachine.Tasks[j].TimeSpan;
                }
                else
                {
                    firstMachine.Tasks[j].TaskStart = firstMachine.Tasks[j - 1].TaskStop;
                    if (firstMachine.Tasks[j].TaskStop >= secondMachine.Tasks[j - 1].TaskStop)
                    {
                        secondMachine.Tasks[j].TaskStart = firstMachine.Tasks[j].TaskStop;
                    }
                    else
                    {
                        secondMachine.Tasks[j].TaskStart = secondMachine.Tasks[j - 1].TaskStop;
                    }
                }
            }
        }

        private static void SimulateForNotFirstMachinePairs(Machine firstMachine, Machine secondMachine, int taskListSize, int firstTaskStart)
        {
            for (int j = 0; j < taskListSize; j++)
            {
                if (j == 0)
                {
                    secondMachine.Tasks[j].TaskStart = firstMachine.Tasks[j].TimeSpan + firstTaskStart;
                }
                else
                {
                    if (firstMachine.Tasks[j].TaskStop >= secondMachine.Tasks[j - 1].TaskStop)
                    {
                        secondMachine.Tasks[j].TaskStart = firstMachine.Tasks[j].TaskStop;
                    }
                    else
                    {
                        secondMachine.Tasks[j].TaskStart = secondMachine.Tasks[j - 1].TaskStop;
                    }
                }
            }
        }
    }
}
