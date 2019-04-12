using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Zad1.BackEnd;

namespace Simulated_Annealing
{
    static class Annealing
    {
        public static List<Machine> machineList = new List<Machine>(Initializer.machineList.Count);
        static float temperature = 30;
        public static int Cmax = 1000000000;
        const float coolingCoefficient = 0.9f;
        public static int totalAlgorithmTime = 0;

        public static void simulatedAnnealing()
        {
            Random random = new Random();
            float currentTemperature = temperature;
            copyMachines();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (currentTemperature > 0.1 * temperature)
            {
                currentTemperature = generateNewNeighbor(random, currentTemperature);
            }

            stopWatch.Stop();
            totalAlgorithmTime = stopWatch.Elapsed.Milliseconds;
        }

        public static void copyMachines()
        {
            Initializer.machineList.ForEach((item) =>
            {
                machineList.Add(new Machine(item));
            });
        }

        private static float generateNewNeighbor(Random random, float currentTemperature)
        {
            int randomNeighbor = random.Next(0, machineList[0].Tasks.Count - 1);
            int randomNeighbor2 = random.Next(0, machineList[0].Tasks.Count - 1);
            swapAndConfigure(randomNeighbor, randomNeighbor2);
            if (!acceptNeighborSolution(machineList[machineList.Count - 1].Tasks.Last().TaskStop))
            {
                swapAndConfigure(randomNeighbor, randomNeighbor2);
            }
            currentTemperature = updateTemperature(currentTemperature);
            return currentTemperature;
        }

        private static void swapAndConfigure(int randomNeighbor, int randomNeighbor2)
        {
            foreach (Machine machine in machineList)
            {
                Swap(machine.Tasks, randomNeighbor, randomNeighbor2);
            }
            for (int i = 0; i < machineList.Count - 1; i++)
            {
                Configuration.configureTwoNeighboringMachines(machineList[i].Tasks, machineList[i + 1].Tasks,
                                              machineList[0].Tasks.Count, machineList[i].Tasks[0].TaskStart
                                            );
            }
        }

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        public static bool acceptNeighborSolution(int newCmax)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1);

            if (calculateAcceptationProbability(newCmax) > randomNumber)
            {
                Cmax = newCmax;
                return true;
            }
            return false;
        }

        public static double calculateAcceptationProbability(int newCmax)
        {
            if (newCmax > Cmax)
            {
                float acceptationCmax = (Cmax - newCmax) / temperature;
                return Math.Pow(Math.E, acceptationCmax);
            }
            return 1d;
        }

        public static float updateTemperature(float currentTemperature)
        {
            return coolingCoefficient * currentTemperature;
        }
    }
}
