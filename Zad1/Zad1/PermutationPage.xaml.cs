using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PermutationPage.xaml
    /// </summary>
    public partial class PermutationPage : Page
    {
        public PermutationPage()
        {
            InitializeComponent();
        }

        private void PermutationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(permutationComboBox.SelectedItem != null)
            {
                GUI.switchPage(permutationComboBox.SelectedIndex, 1);
                //Dispatcher.BeginInvoke((Action)(() => permutationComboBox.SelectedItem = null));
                permutationComboBox.SelectedItem = null;
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
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Initializer.initialize();
            Initializer.simulation();
            GUI.drawGUI(GUI.mainWindow);
            GUI.switchPage(1);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (PermutationPage page in GUI.permutationPages)
            {
                page.canvas.Children.Clear();
            }
            GUI.clearSimulation();
        }
    }
}
