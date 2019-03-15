using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1.BackEnd
{
    public class Task
    {
        public Task(int iD, int timeSpan)
        {
            ID = iD;
            TimeSpan = timeSpan;
        }

        public int ID { get; set; }
        public int TimeSpan { get; set; }
    }
}
