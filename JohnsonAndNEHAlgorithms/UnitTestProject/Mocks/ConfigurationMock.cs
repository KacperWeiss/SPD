using JohnsonAndNEHAlgorithms.BackEnd.Components;
using JohnsonAndNEHAlgorithms.BackEnd.Simulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Mocks
{
    class ConfigurationMock : Configuration
    {
        public override void GenerateConfiguration(List<Machine> rawMachines)
        {
            ConfiguredMachines = rawMachines;
            SimulateConfiguration();
        }
    }
}
