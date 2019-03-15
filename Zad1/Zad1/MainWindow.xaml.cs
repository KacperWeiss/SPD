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
            //TestList.ItemsSource = MyPermute.TaskLists;
        }

        private void InitializeTestList(object sender, RoutedEventArgs e)
        {
            var taskLists = BackEnd.MyPermute.PermuteTasks(new List<BackEnd.Task> { new BackEnd.Task(1, 3), new BackEnd.Task(2, 12), new BackEnd.Task(3, 15), new BackEnd.Task(4, 2) });
            foreach (List<BackEnd.Task> tasks in taskLists)
            {
                List<int> Ids = new List<int>();
                foreach (BackEnd.Task task in tasks)
                {
                    Ids.Add(task.ID);
                }
                TestList.Items.Add(String.Join(", ", Ids));
            }
        }
    }
}
