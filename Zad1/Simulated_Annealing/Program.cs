using System;
using System.Collections.Generic;
using System.IO;

namespace Simulated_Annealing
{
    
    class Program
    {
        const string filepath = "C:\\Users\\Krystian Wojakiewicz\\Desktop\\SPD\\Zad1\\Simulated_Annealing\\test3.txt";
        private static void readFile(string[] args, Parser parser)
        {
            try
            {
                using (StreamReader sr = File.OpenText(filepath))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        parser.data.Add(s);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }



        static void Main(string[] args)
        {
            Parser parser = new Parser();

            //if (args.Length < 2)
            //{
            //    System.Console.WriteLine("No arguments were given");
            //    System.Environment.Exit(1);
            //}

            readFile(args, parser);
            parser.parseFile();
            Initializer.initializeFromFile(parser.numberOfTasks, parser.numberOfMachines, parser.parsedTasks);
            System.Console.WriteLine("number of machines: " + parser.numberOfMachines);
            System.Console.WriteLine("number of tasks: " + parser.numberOfTasks);


            Annealing.simulatedAnnealing();
            System.Console.WriteLine("Cmax: " + Annealing.Cmax);
            System.Console.WriteLine("Algorithm Time: " + Annealing.totalAlgorithmTime);
            System.Console.ReadLine();
        }
    }
}
