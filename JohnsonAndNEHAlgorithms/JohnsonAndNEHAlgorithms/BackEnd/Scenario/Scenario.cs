using System;
using System.Collections.Generic;
using JohnsonAndNEHAlgorithms.BackEnd.Components;
using JohnsonAndNEHAlgorithms.BackEnd.Simulations;

namespace JohnsonAndNEHAlgorithms.BackEnd.Scenario
{
    public class Scenario
    {
        private AlgorithmChoice currentAlgorithm;
        private Configuration configuration;
        private readonly List<Machine> scenarioMachines = new List<Machine>();

        public Scenario(List<Machine> scenarioMachines)
        {
            this.scenarioMachines = scenarioMachines;
        }

        public List<Machine> getConfiguratedMachinesFor(AlgorithmChoice algorithm)
        {
            if (currentAlgorithm == algorithm)
            {
                return configuration.ConfiguredMachines;
            }
            switch (algorithm)
            {
                case AlgorithmChoice.Johnson:
                    throw new NotImplementedException();

                case AlgorithmChoice.NEH:
                    configuration = new AlgorithmNEH();
                    configuration.GenerateConfiguration(scenarioMachines);
                    break;
            }
            return configuration.ConfiguredMachines;
        }

        // Dodać enuma i metode która w switchu tworzy konfiguracje dla algorytmu i wysyła do GUI
    }
}