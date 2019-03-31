using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Zad1.BackEnd
{
    static class GUI
    {
        public static List<PermutationPage> permutationPages = new List<PermutationPage>();
        public static MainWindow mainWindow;

        public static List<Rectangle> rectangles = new List<Rectangle>();
        public static List<Rectangle> rectanglesJohnson = new List<Rectangle>();
        public static List<Rectangle> rectanglesNEH = new List<Rectangle>();

        public static List<Task> firstMachineJohnson = new List<Task>();
        public static List<Task> secondMachineJohnson = new List<Task>();


        public static int indexOfTask(int IDvalue, List<Task> tasks)
        {
            foreach(Task task in tasks)
            {
                if(task.ID == IDvalue)
                {
                    return tasks.IndexOf(task);
                }
            }
            return -1;
        }


        public static void drawNEH()
        {
            permutationPages.Add(new PermutationPage());
            for(int i = 0; i < Simulator.machineNEH.Count; i++)
            {
                foreach (Task task in Simulator.machineNEH[i])
                {

                    rectanglesNEH.Add(new Rectangle()
                    {
                        Width = (25 * task.TimeSpan) - 1,
                        Height = 20,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    });
                    permutationPages[permutationPages.Count() - 1].canvasNEH.Children.Add(rectanglesNEH.Last());

                    Canvas.SetTop(rectanglesNEH.Last(), 10 + i *70);
                    Canvas.SetLeft(rectanglesNEH.Last(), (25 * task.TaskStart));
      
                }
                
            }
            foreach(PermutationPage page in permutationPages)
            {
                page.CMaxValueNEH.Content = Simulator.machineNEH.Last().Last().TaskStop;
                page.CompTimeNEH.Content = Simulator.compTime;
                foreach (NEHtask NehTask in Simulator.solutionList)
                {
                    page.NEHsequenceTextBlock.Text += NehTask.taskNumber.ToString() + " , ";
                }
            }

            foreach (PermutationPage permPage in permutationPages)
            {
                permPage.permutationComboBox.Items.Add("NEH");
                permPage.permutationComboBox2.Items.Add("NEH");
                permPage.permutationComboBoxNEH.Items.Add("NEH");
            }
        }





        public static void drawJohnon(MainWindow window)
        {
            mainWindow = window;
            int taskTimeSpan;
            int taskID;
            int taskStart;
            int taskStop = 0;
           
            List<Task> johnsonOutput = new List<Task>(Simulator.simulateJohnson(Initializer.workCenters)); 

            foreach (Task task in johnsonOutput)
            {
                int timespan = Initializer.workCenters[0].Tasks[indexOfTask(task.ID, Initializer.workCenters[0].Tasks)].TimeSpan;
                int timespan2 = Initializer.workCenters[1].Tasks[indexOfTask(task.ID, Initializer.workCenters[1].Tasks)].TimeSpan;
                firstMachineJohnson.Add(new Task(task.ID, timespan));
                secondMachineJohnson.Add(new Task(task.ID, timespan2));
            }

            
            for (int i = 0; i < johnsonOutput.Count ; i++)
            {

                if (i == 0)
                {
                    taskTimeSpan = firstMachineJohnson[i].TimeSpan;
                    taskID = firstMachineJohnson[i].ID;
                    taskStart = 0;
                    taskStop = taskTimeSpan + taskStart;
                    firstMachineJohnson[i].TaskStop = taskStop;
                    secondMachineJohnson[i].TaskStart = taskStop;
                    secondMachineJohnson[i].TaskStop = taskStop + secondMachineJohnson[i].TimeSpan;
                }
                else
                {
                    taskStart = taskStop;
                    firstMachineJohnson[i].TaskStart = taskStart;
                    taskStop = taskStart + firstMachineJohnson[i].TimeSpan;
                    firstMachineJohnson[i].TaskStop = taskStop;

                    secondMachineJohnson[i].TaskStart = firstMachineJohnson[i].TaskStop;
                    secondMachineJohnson[i].TaskStop = secondMachineJohnson[i].TaskStart + secondMachineJohnson[i].TimeSpan;

                    if ((firstMachineJohnson[i].TaskStop < secondMachineJohnson[i - 1].TaskStop))
                    {
                        secondMachineJohnson[i].TaskStart = secondMachineJohnson[i - 1].TaskStop;
                        secondMachineJohnson[i].TaskStop = secondMachineJohnson[i].TaskStart + secondMachineJohnson[i].TimeSpan;
                    }
                }


            }
            permutationPages.Add(new PermutationPage());
            foreach (Task task in firstMachineJohnson)
            {
                
                rectanglesJohnson.Add(new Rectangle()
                {
                    Width = (25 * task.TimeSpan) - 1,
                    Height = 20,
                    Fill = Brushes.Green,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                });
                permutationPages[permutationPages.Count() - 1].canvas2.Children.Add(rectanglesJohnson.Last());

                Canvas.SetTop(rectanglesJohnson.Last(), 10);
                Canvas.SetLeft(rectanglesJohnson.Last(), (25 * task.TaskStart));
                
            }

            foreach (Task task in secondMachineJohnson)
            {
                rectanglesJohnson.Add(new Rectangle()
                {
                    Width = (25 * task.TimeSpan) - 1,
                    Height = 20,
                    Fill = Brushes.Green,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                });
                permutationPages[permutationPages.Count() -1].canvas2.Children.Add(rectanglesJohnson.Last());

                Canvas.SetTop(rectanglesJohnson.Last(), 10 +60);
                Canvas.SetLeft(rectanglesJohnson.Last(), (25 * task.TaskStart));
            }

            foreach (PermutationPage permPage in permutationPages)
            {      
                permPage.permutationComboBox.Items.Add("JOHNSON");
                permPage.permutationComboBox2.Items.Add("JOHNSON");
                permPage.permutationComboBoxNEH.Items.Add("JOHNSON");
            }

        }


        public static void drawGUI(MainWindow window)
        {
            mainWindow = window;
            foreach (List<Task> taskList in Initializer.firstMachinePermuteResult)
            {
               // int currentIterator = Initializer.firstMachinePermuteResult.IndexOf(taskList);              
               // String comboBoxItemName = "permutation" + currentIterator;

               // window.permutationComboBox.Items.Add(new ComboBoxItem());
               // window.permutationComboBox.Items[currentIterator] = comboBoxItemName;

                permutationPages.Add(new PermutationPage());
 
                foreach (Task task in taskList)
                {
                    rectangles.Add(new Rectangle()
                    {
                        Width = (25 * task.TimeSpan) - 1,
                        Height = 20,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    });
                    permutationPages[permutationPages.Count()-1].canvas.Children.Add(rectangles.Last());

                    Canvas.SetTop(rectangles.Last(), 10);
                    Canvas.SetLeft(rectangles.Last(), (25 * task.TaskStart));
                }
                
            }

            foreach (PermutationPage permPage in permutationPages)
            {
                for (int i = 0; i < permutationPages.Count /*- 1*/; i++)
                {
                    permPage.permutationComboBox.Items.Add("permutation " + i);
                    permPage.permutationComboBox2.Items.Add("permutation " + i);
                }
            }

            foreach (List<Task> taskList in Initializer.secondMachinePermuteResult)
            {
                 int currentIterator = Initializer.secondMachinePermuteResult.IndexOf(taskList);              
 
                foreach (Task task in taskList)
                {
                    rectangles.Add(new Rectangle()
                    {
                        Width = (25 * task.TimeSpan) - 1,
                        Height = 20,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    });
                    permutationPages[currentIterator].canvas.Children.Add(rectangles.Last());

                    //window.canvas.Children.Add(rectangle);
                    Canvas.SetTop(rectangles.Last(), 10 + 60);
                    Canvas.SetLeft(rectangles.Last(), (25 * task.TaskStart));
                    permutationPages[currentIterator].CMaxValue.Content = Initializer.secondMachinePermuteResult[currentIterator].Last().TaskStop;
                }
            }
            drawJohnon(window);
        }

        public static void switchPage(int selectedIndex, int newDefaultTab)
        {
            mainWindow.Content = permutationPages[selectedIndex];
            permutationPages[selectedIndex].tabs.SelectedItem = permutationPages[selectedIndex].tabs.Items[newDefaultTab];
        }

        public static void switchPage(int newDefaultTab)
        {
            TabControl tabControl = permutationPages[0].tabs;

            mainWindow.Content = permutationPages[0];
            tabControl.SelectedItem = tabControl.Items[newDefaultTab];
        }

        public static void clearSimulation()
        {
            //clearing johnson
            rectangles.Clear();
            rectanglesJohnson.Clear();
            firstMachineJohnson.Clear();
            secondMachineJohnson.Clear();

            //clearing Full Search
            Initializer.firstMachinePermuteResult.Clear();
            Initializer.secondMachinePermuteResult.Clear();
            Initializer.workCenters.Clear();
            foreach(PermutationPage page in permutationPages)
            {
                page.permutationComboBox.Items.Clear();
                page.permutationComboBox2.Items.Clear();
                page.permutationComboBoxNEH.Items.Clear();
                page.CMaxValue.Content = 0;
                page.CMaxValueNEH.Content = 0;
                page.CompTimeNEH.Content = 0;
            }

            //clearing NEH
            foreach(List<Task> taskList in Simulator.machineNEH)
            {
                foreach(Task task in taskList)
                {
                    task.destroy();
                }
            }
            Simulator.machineNEH.Clear();
            Simulator.solutionList.Clear();

            permutationPages.Clear();   
        }
    }
}
