using System;
using System.Collections.Generic;
using System.Text;
using Zad1.BackEnd;

namespace Simulated_Annealing
{
    static class Configuration
    {
        //finds best partial permutation
        public static void configureTwoNeighboringMachines(List<Task> firstMachine, List<Task> secondMachine, int sizeOfTasks, int taskStart)
        {
            //machines 0 and 1
            if (taskStart == 0)
            {
                for (int i = 0; i < sizeOfTasks; i++)
                {

                    if (i == 0)
                    {
                        firstMachine[i].TaskStop = firstMachine[i].TimeSpan + taskStart;

                        secondMachine[i].TaskStart = firstMachine[i].TimeSpan + taskStart;
                        secondMachine[i].TaskStop = firstMachine[i].TimeSpan + taskStart + secondMachine[i].TimeSpan; 
                    }
                    else
                    {
                        firstMachine[i].TaskStart = firstMachine[i].TimeSpan + taskStart;
                        firstMachine[i].TaskStop = firstMachine[i].TimeSpan + taskStart + firstMachine[i].TimeSpan;
  
                        if ((firstMachine[i].TaskStop < secondMachine[i - 1].TaskStop))
                        {
                            secondMachine[i].TaskStart = secondMachine[i - 1].TaskStop;
                            secondMachine[i].TaskStop = secondMachine[i].TaskStart + secondMachine[i].TimeSpan;
                        }

                        secondMachine[i].TaskStart = firstMachine[i].TaskStop;
                        secondMachine[i].TaskStop = secondMachine[i].TaskStart + secondMachine[i].TimeSpan;
                    }
                }
            }
            //machines 1 and forth
            else
            {
                for (int i = 0; i < sizeOfTasks; i++)
                {

                    if (i == 0)
                    {
                        secondMachine[i].TaskStart = firstMachine[i].TaskStop;
                        secondMachine[i].TaskStop = firstMachine[i].TimeSpan + taskStart + secondMachine[i].TimeSpan;

                    }
                    else
                    {
                        secondMachine[i].TaskStart = firstMachine[i].TaskStop;
                        secondMachine[i].TaskStop = secondMachine[i].TaskStart + secondMachine[i].TimeSpan;

                        if ((firstMachine[i].TaskStop < secondMachine[i - 1].TaskStop))
                        {
                            secondMachine[i].TaskStart = secondMachine[i - 1].TaskStop;
                            secondMachine[i].TaskStop = secondMachine[i].TaskStart + secondMachine[i].TimeSpan;
                        }
                    }
                }
            }

        }
    }
}
