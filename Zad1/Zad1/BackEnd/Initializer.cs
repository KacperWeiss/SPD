using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkCenter = Zad1.BackEnd.WorkCenter;

namespace Zad1.BackEnd
{
    public static class Initializer 
    {
         public static List<WorkCenter> workCenters = new List<WorkCenter>();
        public static List<List<Task>> firstMachinePermuteResult = new List<List<Task>>();
        public static List<List<Task>> secondMachinePermuteResult = new List<List<Task>>();
        public static void initialize()
        {
            workCenters.Add(new WorkCenter(new List<Task>()));
            workCenters.Add(new WorkCenter(new List<Task>()));
            workCenters[0].Tasks.AddRange(new List<Task> {
                new Task(1,2),
                new Task(2,3),
                new Task(3,8)
                });
            workCenters[1].Tasks.AddRange(new List<Task> {
                new Task(1,1),
                new Task(2,2),
                new Task(3,5)
                });
        }

        public static void simulation()
        {
            firstMachinePermuteResult = new List<List<Task>>(MyPermute.PermuteTasks(Initializer.workCenters[0].Tasks));
            MyPermute.TaskLists.Clear();
            secondMachinePermuteResult = new List<List<Task>>(MyPermute.PermuteTasks(Initializer.workCenters[1].Tasks));
            
            Simulator.simulate(firstMachinePermuteResult, secondMachinePermuteResult);
        }

    }
}
