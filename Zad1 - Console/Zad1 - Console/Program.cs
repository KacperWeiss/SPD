using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zad1.BackEnd;
using Task = Zad1.BackEnd.Task;

namespace Zad1___Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawSetupString;
            string[] setupString;
            int numberOfTasks;
            int numberOfMachines;
            List<WorkCenter> workCenters = new List<WorkCenter>();

            // TODO: Stworzyć klasę scenario initializer
            ObtainInitialScenarioInformation(out rawSetupString, out setupString, out numberOfTasks, out numberOfMachines); 
            InitializeMachines(numberOfMachines, workCenters);
            ObtainTaskInformationAndAssignThemToWorkCenters(ref rawSetupString, ref setupString, numberOfTasks, numberOfMachines, workCenters);


        }

        private static void ObtainTaskInformationAndAssignThemToWorkCenters(ref string rawSetupString, ref string[] setupString, int numberOfTasks, int numberOfMachines, List<WorkCenter> workCenters)
        {
            for (int i = 0; i < numberOfTasks; i++)
            {
                GetValuesFromString(out rawSetupString, out setupString);
                AssignTasksInformationToWorkCenters(setupString, numberOfMachines, workCenters, i);
            }
        }

        private static void AssignTasksInformationToWorkCenters(string[] setupString, int numberOfMachines, List<WorkCenter> workCenters, int i)
        {
            for (int j = 0; j < numberOfMachines; j++)
            {
                if (IsFirstStringEmpty(setupString))
                {
                    workCenters[j].Tasks.Add(new Task(i, Convert.ToInt32(setupString[i + 1])));
                }
                else
                {
                    workCenters[j].Tasks.Add(new Task(i, Convert.ToInt32(setupString[i + 0])));
                }
            }
        }

        private static void ObtainInitialScenarioInformation(out string rawSetupString, out string[] setupString, out int numberOfTasks, out int numberOfMachines)
        {
            Console.WriteLine("How many tasks and then how many machines (seperated with space)?");

            GetValuesFromString(out rawSetupString, out setupString);
            ProvideInitialInformation(setupString, out numberOfTasks, out numberOfMachines);
        }

        private static void ProvideInitialInformation(string[] setupString, out int numberOfTasks, out int numberOfMachines)
        {
            if (IsFirstStringEmpty(setupString))
            {
                numberOfTasks = Convert.ToInt32(setupString[1]);
                numberOfMachines = Convert.ToInt32(setupString[2]);
            }
            else
            {
                numberOfTasks = Convert.ToInt32(setupString[0]);
                numberOfMachines = Convert.ToInt32(setupString[1]);
            }
        }

        private static void InitializeMachines(int numberOfMachines, List<WorkCenter> workCenters)
        {
            for (int j = 0; j < numberOfMachines; j++)
            {
                workCenters.Add(new WorkCenter(new List<Task>()));
            }
        }

        private static bool IsFirstStringEmpty(string[] setupString)
        {
            return setupString[0] == String.Empty;
        }

        private static void GetValuesFromString(out string rawSetupString, out string[] setupString)
        {
            rawSetupString = Console.ReadLine();
            setupString = System.Text.RegularExpressions.Regex.Split(rawSetupString, @"\s+");
        }
    }
}
