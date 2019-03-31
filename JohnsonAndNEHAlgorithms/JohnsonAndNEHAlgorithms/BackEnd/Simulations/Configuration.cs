using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnsonAndNEHAlgorithms.BackEnd.Simulations
{
    public abstract class Configuration : IConfiguration
    {
        public abstract void GenerateConfiguration();

        public void SimulateConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
