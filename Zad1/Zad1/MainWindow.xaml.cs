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
        char[] splitchar = { ' ' };
        List<string[]> final = new List<string[]>();
        List<string> data = new List<string>();
        List<List<int>> parsedTasks = new List<List<int>>();
        int numberOfTasks = 0;
        int numberOfMachines = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            //Initializer.initialize();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Initializer.initializeFromFile(numberOfTasks, numberOfMachines, parsedTasks);
            Initializer.simulation();

            GUI.drawGUI(window);
            GUI.drawNEH();
            GUI.switchPage(1);
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
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            data.Add(s);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

        private void PermutationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {     
                window.Content = GUI.permutationPages[permutationComboBox.SelectedIndex];
        }
    }
}
