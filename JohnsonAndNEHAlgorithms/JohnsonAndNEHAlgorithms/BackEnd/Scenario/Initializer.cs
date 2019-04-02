using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnsonAndNEHAlgorithms.BackEnd.Scenario
{
    static class Initializer
    {
        public static Scenario initializeScenario(int numberOfTasks, int numberOfMachines, List<List<int>> parsedTasks)
        {
            for (int i = 0; i < numberOfMachines; i++)
            {
                workCenters.Add(new WorkCenter(new List<Task>()));

                for (int j = 0; j < numberOfTasks; j++)
                {
                    workCenters[i].Tasks.Add(new Task(j, parsedTasks[i][j]));
                }
            }
        }
    }
}
