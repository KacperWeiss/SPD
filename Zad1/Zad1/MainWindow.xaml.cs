using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zad1.BackEnd;
using Task = Zad1.BackEnd.Task;
using System;
using System.IO;
using Microsoft.Win32;

namespace Zad1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Initializer.initialize();
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Initializer.simulation();

            foreach (Task task in Initializer.firstMachinePermuteResult[3])
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = (15 * task.TimeSpan) - 1,
                    Height = 20,
                    Fill = Brushes.Green,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                };

                canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, 10);
                Canvas.SetLeft(rectangle, (15 * task.TaskStart));
            }
           
            foreach (Task task in Initializer.secondMachinePermuteResult[3])
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = (15 * task.TimeSpan) - 1,
                    Height = 20,
                    Fill = Brushes.Green,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                };

                canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, 20 + 50);
                Canvas.SetLeft(rectangle, (15 * task.TaskStart));
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Primitives.Popup popup = sender as System.Windows.Controls.Primitives.Popup;
            if(popup != null)
            {
                popup.IsOpen = false;
            }
        }
        private void LoadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a text file";
            open.Filter = "All supported files|*.txt";
            if (open.ShowDialog() == true)
            {
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(open.FileName))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
    }
}
