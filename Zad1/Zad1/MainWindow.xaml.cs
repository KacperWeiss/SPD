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

namespace Zad1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
                Canvas.SetTop(rectangle, 20);
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
                Canvas.SetTop(rectangle, 20 + 40);
                Canvas.SetLeft(rectangle, (15 * task.TaskStart));
            }
        }

        

    }
}
