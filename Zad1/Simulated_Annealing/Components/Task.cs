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
            TaskStart = 0;
        }
        public Task(Task task) {
            ID = task.ID;
            TimeSpan = task.TimeSpan;
            TaskStart = task.TaskStart;
        }
        public void destroy()
        {
            ID = 0;
            TimeSpan = 0;
            TaskStart = 0;
            TaskStop = 0;
        } 

        public int ID { get; set; }
        public int TimeSpan { get; set; }
        public int TaskStart { get; set; }
        public int TaskStop { get; set; }
    }
}
