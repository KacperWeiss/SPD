using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1.BackEnd
{
    static class MyPermute
    {
        static List<List<Task>> taskLists;

        public static List<List<Task>> PermuteTasks(List<Task> tasks)
        {
            int high = tasks.Count - 1;
            PermuteTasks(tasks, 0, high);

            return taskLists;
        }

        private static void Swap(Task a, Task b)
        {
            Task temporaryTask = a;
            a = b;
            b = temporaryTask;
        }

        private static void PermuteTasks(List<Task> tasks, int lowIt, int highIt)
        {
            if (lowIt == highIt)
            {
                taskLists.Add(tasks);
            }
            else
            {
                for (int i = lowIt; i <= highIt; i++)
                {
                    Swap(tasks[lowIt], tasks[i]);
                    PermuteTasks(tasks, lowIt + 1, highIt);
                    Swap(tasks[lowIt], tasks[i]);
                }
            }
        }
    }
}
