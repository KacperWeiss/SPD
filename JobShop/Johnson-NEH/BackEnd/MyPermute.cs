using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1.BackEnd
{
    static class MyPermute
    {
        public static List<List<Task>> taskLists = new List<List<Task>>();

        public static List<List<Task>> TaskLists { get => taskLists; set => taskLists = value; }

        public static List<List<Task>> PermuteTasks(List<Task> tasks)
        {
            int high = tasks.Count - 1;
            PermuteTasks(tasks, 0, high);

            return TaskLists;
        }

        public static void Swap<T>( this List<T> list, int index1, int index2)
        {
            T tmp =list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        private static void PermuteTasks(List<Task> tasks, int lowIt, int highIt)
        {
            if (lowIt == highIt)
            {
                List<Task> newList = new List<Task>(tasks.Count);

                tasks.ForEach((item) =>
                {
                    newList.Add(new Task(item));
                });
                TaskLists.Add(new List<Task>(newList));
            }
            else
            {
                for (int i = lowIt; i <= highIt; i++)
                {
                    Swap(tasks, lowIt, i);   
                    PermuteTasks(tasks, lowIt + 1, highIt);
                    Swap(tasks, lowIt, i);
                }
            }

        }
    }
}
