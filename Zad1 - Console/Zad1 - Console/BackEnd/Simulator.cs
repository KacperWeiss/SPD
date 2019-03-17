using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Zad1.BackEnd {
    static class Simulator {
        static int taskTimeSpan;
        static int taskID;
        static int taskStart;
        static int taskStop = 0;

        //TODO: enable simulations with a given number of machines
        //also calculate maximum and minimum timespan for a set of permutations
        //add Johnson's algorithm
        public static void simulate(List<List<Task>> firstMachinePermuteResult, List<List<Task>> secondMachinePermuteResult) {

            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 3; i++)
                {

                    if (i == 0)
                    {
                        taskTimeSpan = firstMachinePermuteResult[j][i].TimeSpan;
                        taskID = firstMachinePermuteResult[j][i].ID;
                        taskStart = 0;
                        taskStop = taskTimeSpan + taskStart;
                        firstMachinePermuteResult[j][i].TaskStop = taskStop;
                        secondMachinePermuteResult[j][i].TaskStart = taskStop;
                        secondMachinePermuteResult[j][i].TaskStop = taskStop + secondMachinePermuteResult[j][i].TimeSpan;
                    }
                    else
                    {
                        taskStart = taskStop;
                        firstMachinePermuteResult[j][i].TaskStart = taskStart;
                        taskStop = taskStart + firstMachinePermuteResult[j][i].TimeSpan;
                        firstMachinePermuteResult[j][i].TaskStop = taskStop;
                        secondMachinePermuteResult[j][i].TaskStart = firstMachinePermuteResult[j][i].TaskStop;
                        secondMachinePermuteResult[j][i].TaskStop = secondMachinePermuteResult[j][i].TaskStart + secondMachinePermuteResult[j][i].TimeSpan;

                    }


                }
            }
        }

        public static void checkResults(List<List<Task>> firstMachinePermuteResult, List<List<Task>> secondMachinePermuteResult) {
            Console.WriteLine("MACHINE ONE RESULTS :");
            foreach (List<Task> taskList in firstMachinePermuteResult)
            {
                Console.Write("[");
                foreach (Task task in taskList)
                {
                    Console.Write(task.TaskStart + " - " + task.TaskStop + ", ");
                }
                Console.WriteLine("]");
            }

            Console.WriteLine("\n MACHINE TWO RESULTS :");
            foreach (List<Task> taskList in secondMachinePermuteResult)
            {
                Console.Write("[");
                foreach (Task task in taskList)
                {
                    Console.Write(task.TaskStart + " - " + task.TaskStop + ", ");
                }
                Console.WriteLine("]");
            }

            Console.WriteLine("\n TASK PERMUTATIONS:");
            foreach (List<Task> taskList in firstMachinePermuteResult)
            {
                Console.Write("[");
                foreach (Task task in taskList)
                {
                    Console.Write(task.ID);
                }
                Console.WriteLine("]");
            }
            Console.ReadLine();
        }
    }
}
