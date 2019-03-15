using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1.BackEnd
{
    static class MyPermute
    {
        public static List<List<Task>> PermuteTasks(List<Task> tasks)
        {
            int high = tasks.Count - 1;
            return PermuteTasks(tasks, 0, high);
        }

        private static List<List<Task>> PermuteTasks(List<Task> tasks, int lowI, int highIt)
        {

        }

        private static void Swap(Task a, Task b)
        {

        }
    }
}
