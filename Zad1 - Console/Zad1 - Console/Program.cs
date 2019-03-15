using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zad1.BackEnd;

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

            Console.WriteLine("How many tasks and then how many machines (seperated with space)?");

            GetValuesFromString(out rawSetupString, out setupString);

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

            while (true)
            {
                GetValuesFromString(out rawSetupString, out setupString);
                if (rawSetupString == "q")
                {

                }
                
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
