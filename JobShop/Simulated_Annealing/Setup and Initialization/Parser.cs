using System;
using System.Collections.Generic;
using System.Text;

namespace Simulated_Annealing
{
    class Parser
    {
        char[] splitchar = { ' ' };
        public List<string[]> final = new List<string[]>();
        public List<string> data = new List<string>();
        public List<List<int>> parsedTasks = new List<List<int>>();
        public int numberOfTasks = 0;
        public int numberOfMachines = 0;

        public void parseFile()
        {
            foreach (string word in data)
            {
                final.Add(word.Split(splitchar));
            }
            Int32.TryParse(final[0][1], out numberOfMachines);

            for (int i = 0; i < numberOfMachines; i++)
            {
                parsedTasks.Add(new List<int>());
            }

            foreach (string[] parsedWord in final)
            {
                if (final.IndexOf(parsedWord) > 0)
                {
                    int tmp = 0;
                    for (int i = 0; i < parsedWord.Length; i++)
                    {
                        Int32.TryParse(parsedWord[i], out tmp);
                        parsedTasks[i].Add(tmp);
                    }
                }
                else
                {
                    Int32.TryParse(parsedWord[0], out numberOfTasks);
                    Int32.TryParse(parsedWord[1], out numberOfMachines);
                }

            }
        }
    }
}
