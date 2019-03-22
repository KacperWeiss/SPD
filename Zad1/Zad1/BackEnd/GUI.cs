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
                        Width = (30 * task.TimeSpan) - 1,
                        Height = 20,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    });
                    permutationPages[permutationPages.Count()-1].canvas.Children.Add(rectangles.Last());

                    Canvas.SetTop(rectangles.Last(), 10);
                    Canvas.SetLeft(rectangles.Last(), (30 * task.TaskStart));
                }
                
            }

            foreach (PermutationPage permPage in permutationPages)
            {
                for (int i = 0; i < permutationPages.Count - 1; i++)
                {
                    permPage.permutationComboBox.Items.Add("permutation " + i);
                }
            }

            foreach (List<Task> taskList in Initializer.secondMachinePermuteResult)
            {
                 int currentIterator = Initializer.secondMachinePermuteResult.IndexOf(taskList);              
 
                foreach (Task task in taskList)
                {
                    rectangles.Add(new Rectangle()
                    {
                        Width = (30 * task.TimeSpan) - 1,
                        Height = 20,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    });
                    permutationPages[currentIterator].canvas.Children.Add(rectangles.Last());

                    //window.canvas.Children.Add(rectangle);
                    Canvas.SetTop(rectangles.Last(), 10 + 50);
                    Canvas.SetLeft(rectangles.Last(), (30 * task.TaskStart));
                }
            }  
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
            rectangles.Clear();
            Initializer.firstMachinePermuteResult.Clear();
            Initializer.secondMachinePermuteResult.Clear();
            Initializer.workCenters.Clear();
            permutationPages.Clear();
        }
    }
}
