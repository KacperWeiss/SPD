using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;

namespace JohnsonAndNEHAlgorithms.BackEnd.Scenario
{
    static class Initializer
    {
        public static Scenario initializeScenario(int numberOfTasks, int numberOfMachines, List<List<int>> parsedTasks)
        {
            List<Machine> machines = new List<Machine>();

            for (int i = 0; i < numberOfMachines; i++)
            {
                machines.Add(new Machine(new List<Task>()));

                for (int j = 0; j < numberOfTasks; j++)
                {
                    machines[i].Tasks.Add(new Task(j, parsedTasks[i][j]));
                }
            }
            return new Scenario(machines); ;
        }
    }
}
