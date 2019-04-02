using JohnsonAndNEHAlgorithms.BackEnd.Scenario;
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

namespace JohnsonAndNEHAlgorithms.FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        char[] splitchar = { ' ' };
        List<string> data = new List<string>();
        List<string[]> final = new List<string[]>();
        List<List<int>> parsedTasks = new List<List<int>>();
        int numberOfTasks = 0;
        int numberOfMachines = 0;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogWindow = new OpenFileDialog();
            openFileDialogWindow.Title = "Select a text file";
            openFileDialogWindow.Filter = "All supported files|*.txt";
            if (openFileDialogWindow.ShowDialog() == true)
            {
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(openFileDialogWindow.FileName))
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
            this.Hide();
            ApplicationMainWindow applicationMainWindow = new ApplicationMainWindow(Initializer.initializeScenario(numberOfTasks, numberOfMachines, parsedTasks));
            applicationMainWindow.ShowDialog();
            this.Show();

        }
    }
}
