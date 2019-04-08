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
using JohnsonAndNEHAlgorithms.BackEnd.Scenario;

namespace JohnsonAndNEHAlgorithms.FrontEnd
{
    /// <summary>
    /// Interaction logic for ApplicationMainWindow.xaml
    /// </summary>
    public partial class ApplicationMainWindow : Window
    {
        public Scenario mainScenario;
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
            DrawComponents(machines);
        }

        private void DrawComponents(List<BackEnd.Components.Machine> machines)
        {
            Grid grid = SetUpGrid(machines);
            CreateTopRow(machines, grid);
            CreateGridContent(machines, grid);
        }

        private Grid SetUpGrid(List<BackEnd.Components.Machine> machines)
        {
            Grid grid = new Grid();
            grid = (Grid)this.FindName("UniformGridForTable");
            CreateRows(machines, grid);
            CreateColumns(machines, grid);

            return grid;
        }

        private static void CreateColumns(List<BackEnd.Components.Machine> machines, Grid grid)
        {
            for (int nrOfColumns = 0; nrOfColumns <= machines.Last().Tasks.Last().TaskStop; nrOfColumns++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private static void CreateRows(List<BackEnd.Components.Machine> machines, Grid grid)
        {
            for (int nrOfRows = 0; nrOfRows <= machines.Count; nrOfRows++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private static void CreateTopRow(List<BackEnd.Components.Machine> machines, Grid grid)
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

        private static void CreateGridContent(List<BackEnd.Components.Machine> machines, Grid grid)
        {
            for (int machineNr = 0; machineNr < machines.Count; machineNr++)
            {
                grid.Children.Add(new TextBlock
                {
                    Text = "Machine" + (machineNr + 1).ToString()
                });
                foreach (var task in machines[machineNr].Tasks)
                {
                    for (int j = 0; j < machines.Last().Tasks.Last().TaskStop; j++)
                    {
                        if (task.TaskStart <= j && task.TaskStop > j)
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
                }
            }
        }

    }
}
