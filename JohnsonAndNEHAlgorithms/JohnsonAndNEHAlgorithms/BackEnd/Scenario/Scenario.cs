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
            currentAlgorithm = AlgorithmChoice.NotSpecified;
        }

        public List<Machine> GetConfiguratedMachinesFor(AlgorithmChoice algorithm)
        {
            if (currentAlgorithm == algorithm)
            {
                return configuration.ConfiguredMachines;
            }
            switch (algorithm)
            {
                case AlgorithmChoice.NotSpecified:
                    throw new Exception("You have to specify algorithm type to simulate it's scenario!");

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