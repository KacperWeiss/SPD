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

        public Machine(List<Task> taskList)
        {
            Tasks = new List<Task>(taskList.Count);

            taskList.ForEach((item) =>
            {
                Tasks.Add(new Task((Task)item.Clone()));
            });
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
