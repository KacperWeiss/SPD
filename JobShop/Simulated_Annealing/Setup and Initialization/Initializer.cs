using System;
using System.Collections.Generic;
using System.Text;
using Zad1.BackEnd;

namespace Simulated_Annealing
{
    static class Initializer
    {
        public static List<Machine> machineList = new List<Machine>();
        

        public static void initializeFromFile(int numberOfTasks, int numberOfMachines, List<List<int>> parsedTasks)
        {
            for (int i = 0; i < numberOfMachines; i++)
            {
                machineList.Add(new Machine(new List<Task>()));

                for (int j = 0; j < numberOfTasks; j++)
                {
                    machineList[i].Tasks.Add(new Task(j, parsedTasks[i][j]));
                }
            }

        }
    }
}
