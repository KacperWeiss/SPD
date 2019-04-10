using System.Collections.Generic;
using System.Linq;
using JohnsonAndNEHAlgorithms.BackEnd.Components;
using JohnsonAndNEHAlgorithms.BackEnd.Static_Algorithms;

namespace JohnsonAndNEHAlgorithms.BackEnd.Simulations
{
    public class AlgorithmNEH : Configuration
    {
        private int cMax;
        private List<Task> sortedTasks = new List<Task>();
        public List<Machine> bestMachineConfiguration { get; private set; } = new List<Machine>();

        public override void GenerateConfiguration(List<Machine> rawMachines)
        {
            InitializeCleanConfiguredMachinesList(rawMachines);

            sortedTasks = CreatePrioritizedList(rawMachines);
            AddTaskToConfiguration(rawMachines);

            while (ConfiguredMachines[0].Tasks.Count < rawMachines[0].Tasks.Count)
            {
                AddTaskToConfiguration(rawMachines);
                GetBestConfigurationForCurrentIteration();
            }
            SimulateConfiguration();
        }

        private void InitializeCleanConfiguredMachinesList(List<Machine> rawMachines)
        {
            ConfiguredMachines.Clear();

            foreach (var machine in rawMachines)
            {
                ConfiguredMachines.Add(new Machine((Machine)machine.Clone()));
            }
            
            foreach (var machine in ConfiguredMachines)
            {
                machine.Tasks.Clear();
            }
        }

        private static List<Task> CreatePrioritizedList(List<Machine> rawMachines)
        {
            List<Task> unorderedTasks = new List<Task>();
            for (int i = 0; i < rawMachines[0].Tasks.Count; i++)
            {
                unorderedTasks.Add((Task)rawMachines[0].Tasks[i].Clone());
            }
            for (int i = 1; i < rawMachines.Count; i++)
            {
                for (int j = 0; j < rawMachines[i].Tasks.Count; j++)
                {
                    unorderedTasks[j].TimeSpan += rawMachines[i].Tasks[j].TimeSpan;
                }
            }

            return unorderedTasks.OrderBy(o => o.TimeSpan).Reverse().ToList();
        }

        private void AddTaskToConfiguration(List<Machine> rawMachines)
        {
            int currentID = sortedTasks.First().ID;
            for (int i = 0; i < rawMachines.Count; i++)
            {
                ConfiguredMachines[i].Tasks.Add(new Task(rawMachines[i].Tasks.SingleOrDefault(o => o.ID == currentID)));
            }
            sortedTasks.Remove(sortedTasks.Single(o => o.ID == currentID));
        }

        private void GetBestConfigurationForCurrentIteration()
        {
            SimulateConfiguration();
            cMax = ConfiguredMachines.Last().Tasks.Last().TaskStop;
            bestMachineConfiguration.Clear();
            ConfiguredMachines.ForEach((item) =>
            {
                bestMachineConfiguration.Add(new Machine((Machine)item.Clone()));
            });

            for (int i = 0; i < ConfiguredMachines[0].Tasks.Count - 1; i++)
            {
                foreach (var machine in ConfiguredMachines)
                {
                    CustomSwaps.Swap(machine.Tasks, i, machine.Tasks.Count - 1);
                }

                SimulateConfiguration();
                if (cMax > ConfiguredMachines.Last().Tasks.Last().TaskStop)
                {
                    cMax = ConfiguredMachines.Last().Tasks.Last().TaskStop;
                    bestMachineConfiguration.Clear();
                    ConfiguredMachines.ForEach((item) =>
                    {
                        bestMachineConfiguration.Add(new Machine((Machine)item.Clone()));
                    });
                }

                foreach (var machine in ConfiguredMachines)
                {
                    CustomSwaps.Swap(machine.Tasks, i, machine.Tasks.Count - 1);
                }
            }

            ConfiguredMachines.Clear();
            bestMachineConfiguration.ForEach((item) =>
            {
                ConfiguredMachines.Add(new Machine((Machine)item.Clone()));
            });
        }
    }
}
