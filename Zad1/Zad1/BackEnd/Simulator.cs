using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Zad1.BackEnd {
    static class Simulator {
        static int taskTimeSpan;
        static int taskID;
        static int taskStart;
        static int taskStop = 0;

        //TODO: calculate maximum and minimum timespan for a set of permutations
        //add Johnson's algorithm

        public static void simulateFullSearch(List<List<Task>> firstMachinePermuteResult, List<List<Task>> secondMachinePermuteResult) {
          
            //double numberOfPermutations = SpecialFunctions.Factorial(firstMachinePermuteResult[0].Count());
            int numberOfPermutations = firstMachinePermuteResult.Count();
            int numberOfTasks = firstMachinePermuteResult[0].Count();

            for (int j = 0; j < numberOfPermutations; j++)
            {
                for (int i = 0; i < numberOfTasks; i++)
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
                        
                         if ((firstMachinePermuteResult[j][i].TaskStop < secondMachinePermuteResult[j][i -1].TaskStop))
                         {
                            secondMachinePermuteResult[j][i].TaskStart = secondMachinePermuteResult[j][i - 1].TaskStop;
                            secondMachinePermuteResult[j][i].TaskStop = secondMachinePermuteResult[j][i].TaskStart + secondMachinePermuteResult[j][i].TimeSpan;
                         }
                    }
                }
            }
        }

        public static List<Task> simulateJohnson(List<WorkCenter> workCenters)
        {
            
            int min = 10000;
            int minTaskIndex = 0;
            int minMachineIndex = 0;
            List<Task> allTasks = new List<Task>();
            List<Task> taskSequenceM1 = new List<Task>();
            List<Task> taskSequenceM2 = new List<Task>();

            List<WorkCenter> localWorkCenter = workCenters.ConvertAll(workCenter => new WorkCenter(workCenter.Tasks));     

            bool tasksLeft = true;
            while(tasksLeft)
            {
                tasksLeft = false;
                foreach (WorkCenter machine in localWorkCenter)
                {
                    

                    foreach (Task task in machine.Tasks)
                    {  
                        if (task.TimeSpan <= min) ////////////////////////
                        {
                            min = task.TimeSpan;
                            minTaskIndex = machine.Tasks.IndexOf(task);
                            minMachineIndex = localWorkCenter.IndexOf(machine);
                        }
                        //TODO: what if they are equal
                    }
                }

                if (minMachineIndex == 0 && localWorkCenter[minMachineIndex].Tasks.Any())
                {
                    taskSequenceM1.Add(localWorkCenter[minMachineIndex].Tasks[minTaskIndex]);
                    localWorkCenter[0].Tasks.RemoveAt(minTaskIndex);
                    localWorkCenter[1].Tasks.RemoveAt(minTaskIndex);
                }
                else if (minMachineIndex == 1 && localWorkCenter[minMachineIndex].Tasks.Any())
                {
                    taskSequenceM2.Insert(0, localWorkCenter[minMachineIndex].Tasks[minTaskIndex]);
                    localWorkCenter[0].Tasks.RemoveAt(minTaskIndex);
                    localWorkCenter[1].Tasks.RemoveAt(minTaskIndex);
                }
                if (localWorkCenter[minMachineIndex].Tasks.Any())
                    {
                        tasksLeft = true;
                    }
                    min = 10000;
            }

            taskSequenceM1.AddRange(taskSequenceM2);
            
            return taskSequenceM1;
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
