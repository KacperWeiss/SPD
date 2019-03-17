using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1.BackEnd
{
    public class WorkCenter
    {
        public List<Task> Tasks { get; set; }

        public WorkCenter(List<Task> tasks)
        {
            Tasks = tasks;
        }
    }
}
