using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnsonAndNEHAlgorithms.BackEnd.Components
{
    public class Machine : ICloneable
    {
        public List<Task> Tasks { get; set; }
        public int randomNumber;

        public Machine(List<Task> taskList)
        {
            Tasks = new List<Task>(taskList.Count);

            taskList.ForEach((item) =>
            {
                Tasks.Add(new Task((Task)item.Clone()));
            });
        }

        public Machine(Machine machineToCopy)
        {
            Tasks = new List<Task>();
            foreach (var task in machineToCopy.Tasks)
            {
                Tasks.Add(new Task((Task)task.Clone()));
            }
            Random random = new Random();
            randomNumber = random.Next(1, 50000);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
