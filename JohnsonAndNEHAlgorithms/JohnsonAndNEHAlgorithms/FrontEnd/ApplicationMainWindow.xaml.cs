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
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using JohnsonAndNEHAlgorithms.BackEnd.Scenario;

namespace JohnsonAndNEHAlgorithms.FrontEnd
{
    /// <summary>
    /// Interaction logic for ApplicationMainWindow.xaml
    /// </summary>
    public partial class ApplicationMainWindow : Window
    {
        public Scenario mainScenario;
        public int Cmax;
        //private List<Scenario> scenariosArchive;

        public ApplicationMainWindow(Scenario loadedScenario)
        {
            InitializeComponent();
            this.DataContext = this;

            mainScenario = loadedScenario;
        }

        private void NEHAlgorithm_OnClick(object sender, RoutedEventArgs e)
        {
            var machines = mainScenario.GetConfiguratedMachinesFor(AlgorithmChoice.NEH);
            Cmax = machines.Last().Tasks.Last().TaskStop;
            DrawComponents(machines);
        }

        private void ShowCmaxCheckmark_Click(object sender, RoutedEventArgs e)
        {
            SetCmaxLabel();
        }

        private void DrawComponents(List<BackEnd.Components.Machine> machines)
        {
            UniformGrid grid = SetUpGrid(machines);
            CreateTopRow(machines, grid);
            CreateGridContent(machines, grid);
            SetCmaxLabel();
        }

        private UniformGrid SetUpGrid(List<BackEnd.Components.Machine> machines)
        {
            UniformGrid uniformGrid = new UniformGrid();
            uniformGrid = (UniformGrid)this.FindName("UniformGridForTable");
            uniformGrid.Children.Clear();
            SetRowsQuantity(machines, uniformGrid);
            SetColumnsQuantity(machines, uniformGrid);

            return uniformGrid;
        }

        private static void SetRowsQuantity(List<BackEnd.Components.Machine> machines, UniformGrid grid)
        {
            grid.Rows = machines.Count + 1;
        }

        private static void SetColumnsQuantity(List<BackEnd.Components.Machine> machines, UniformGrid grid)
        {
            grid.Columns = machines.Last().Tasks.Last().TaskStop + 1;
        }

        private static void CreateTopRow(List<BackEnd.Components.Machine> machines, UniformGrid grid)
        {
            grid.Children.Add(new TextBlock
            {
                Text = "X"
            });

            for (int i = 1; i <= machines.Last().Tasks.Last().TaskStop; i++)
            {
                grid.Children.Add(new TextBlock
                {
                    Text = i.ToString()
                });
            }
        }

        private static void CreateGridContent(List<BackEnd.Components.Machine> machines, UniformGrid grid)
        {
            int totalTime = machines.Last().Tasks.Last().TaskStop;
            List<bool> isWorking = new List<bool>();

            for (int machineNr = 0; machineNr < machines.Count; machineNr++)
            {
                grid.Children.Add(new TextBlock
                {
                    Text = "Machine" + (machineNr + 1).ToString()
                });

                for (int j = 0; j < totalTime; j++)
                {
                    isWorking.Add(false);
                }

                foreach (var task in machines[machineNr].Tasks)
                {
                    for (int j = 0; j < totalTime; j++)
                    {
                        if (task.TaskStart <= j && task.TaskStop > j)
                        {
                            isWorking[j] = true;
                        }
                    }
                }

                for (int j = 0; j < totalTime; j++)
                {
                    if (isWorking[j])
                    {
                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                            Background = new SolidColorBrush(Colors.Red)
                        });
                    }
                    else
                    {
                        grid.Children.Add(new TextBlock
                        {
                            Text = ""
                        });
                    }
                }
                isWorking.Clear();
                //foreach (var task in machines[machineNr].Tasks)
                //{
                //    for (int j = 0; j < machines.Last().Tasks.Last().TaskStop; j++)
                //    {
                //        if (task.TaskStart <= j && task.TaskStop > j)
                //        {
                //            grid.Children.Add(new TextBlock
                //            {
                //                Text = "",
                //                Background = new SolidColorBrush(Colors.Red)
                //            });
                //        }
                //        else
                //        {
                //            grid.Children.Add(new TextBlock
                //            {
                //                Text = ""
                //            });
                //        }
                //    }
                //}
            }
        }

        private void SetCmaxLabel()
        {
            Label label = new Label();
            label = (Label)this.FindName("CmaxLabel");
            MenuItem menuItem = new MenuItem();
            menuItem = (MenuItem)this.FindName("ShowCmaxCheckmark");
            if (menuItem.IsChecked)
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Cmax: " + Cmax.ToString();
            }
            else
            {
                label.Visibility = Visibility.Hidden;
            }
        }
    }
}
