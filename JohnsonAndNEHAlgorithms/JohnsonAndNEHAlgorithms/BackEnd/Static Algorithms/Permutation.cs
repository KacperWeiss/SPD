using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;

namespace JohnsonAndNEHAlgorithms.BackEnd.Static_Algorithms
{
    static class Permutation
    {
        public static List<List<Task>> TaskLists { get; set; } = new List<List<Task>>();

        public static List<List<Task>> PermuteTasks(List<Task> tasks)
        {
            int high = tasks.Count - 1;
            PermuteTasks(tasks, 0, high);

            return TaskLists;
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
                    CustomSwaps.Swap(tasks, lowIt, i);
                    PermuteTasks(tasks, lowIt + 1, highIt);
                    CustomSwaps.Swap(tasks, lowIt, i);
                }
            }
        }
    }
}
