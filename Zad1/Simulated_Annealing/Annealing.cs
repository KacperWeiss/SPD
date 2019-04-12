using System;
using System.Collections.Generic;
using System.Text;
using Zad1.BackEnd;

namespace Simulated_Annealing
{
    static class Annealing
    {
        public static List<Machine> machineList = new List<Machine>(Initializer.machineList.Count);
        static int temperature;
        static int Cmax = 1000000000;
        const float coolingCoefficient = 0.9f;
        
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }
        //public static void func()
        //{
        //    Initializer.machineList.ForEach((item) =>
        //    {
        //        machineList.Add(new Machine(item));
        //    });
        //}

        public static float updateTemperature()
        {
            return coolingCoefficient * temperature;
        }    

        public static int setInitialTemperature(int initialTemp)
        {
            return initialTemp;
        }

        public static float setCoolingCoefficient(float coolingCoefficient)
        {
            return coolingCoefficient;
        }


        public static bool acceptNeighborSolution(int currentCmax)
        {
            if(currentCmax > Cmax)
            {
                //tutaj funkcja z instrukcji
            }
            return true;
        }

        public static void generateNeighbor()
        {
            Random random = new Random();
            int randomNeighbor = random.Next(0, machineList[0].Tasks.Count -1);
            int randomNeighbor2 = random.Next(0, machineList[0].Tasks.Count - 1);

            foreach (Machine machine in machineList)
            {
                Swap(machine.Tasks, randomNeighbor, randomNeighbor2);
            }
            for(int i = 0; i < machineList.Count -1; i++)
            {
                Configuration.configureTwoNeighboringMachines(machineList[i].Tasks, machineList[i + 1].Tasks,
                                              machineList[0].Tasks.Count, machineList[i].Tasks[0].TaskStart
                                            );

            }
        }

    }

}
