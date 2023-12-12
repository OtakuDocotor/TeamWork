using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempLib_V2;

namespace Граф_интерфейс
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        TemperatureFile[] ArrayOf_CSV_Files_TR;
        TemperatureAndDephFile[] ArrayOf_CSV_Files_TDR;

        FileInfo[] ArrayOfAllFiles;

        bool ISLoadFolder = false;

        string patternOf_CSV_File_TR = @"[0-9].[0-9]+\sm_(TR)_[0-9]+_[0-9]+_[0-9]+.csv",
               patternOf_CSV_File_TDR = @"[0-9].[0-9]+\sm_(TDR)_[0-9]+_[0-9]+_[0-9]+.csv",
               Fbd_SelectPath;

        int Number_of_first_mesure, Number_of_last_mesure;

        public static TemperatureFile[] FindTR(FileInfo[] AllFiles, string pattern)
        {
            List<TemperatureFile> findingFiles = new List<TemperatureFile>();
            int i = 0;
            MatchCollection Matches = null;
            Regex r = new Regex(pattern);
            foreach (FileInfo q in AllFiles)
            {
                Matches = r.Matches(q.Name);
                foreach (Match m in Matches)
                {
                   findingFiles.Add(new TemperatureFile(q));
                }
            }

            return findingFiles.ToArray();
        }

        public static TemperatureAndDephFile[] FindTDR(FileInfo[] AllFiles, string pattern)
        {
            List<TemperatureAndDephFile> findingFiles = new List<TemperatureAndDephFile>();
            int i = 0;
            MatchCollection Matches = null;
            Regex r = new Regex(pattern);
            foreach (FileInfo q in AllFiles)
            {
                Matches = r.Matches(q.Name);
                foreach (Match m in Matches)
                {
                    findingFiles.Add(new TemperatureAndDephFile(q));
                }
            }

            return findingFiles.ToArray();
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadingFiles(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Fbd_SelectPath = FBD.SelectedPath;
                DirectoryInfo directory = new DirectoryInfo(FBD.SelectedPath);
                ArrayOfAllFiles = directory.GetFiles();

                ArrayOf_CSV_Files_TR = FindTR(ArrayOfAllFiles, patternOf_CSV_File_TR);
                ArrayOf_CSV_Files_TDR = FindTDR(ArrayOfAllFiles, patternOf_CSV_File_TDR);

                ISLoadFolder = true;

                System.Windows.MessageBox.Show(
                    $"{ArrayOf_CSV_Files_TR.Length + ArrayOf_CSV_Files_TDR.Length} file(s) upload successfully!");
            }

            
        }
        private void ViewingFiles(object sender, RoutedEventArgs e)
        {
            if (ISLoadFolder)
            {
                MainTextBox.Text = "";
                MainTextBox.Text += "TR_Files: \n";
                foreach (var file in ArrayOf_CSV_Files_TR)
                {
                    MainTextBox.Text += file + "\n";
                }

                MainTextBox.Text += "\nTDR_Files: \n";
                foreach (var file in ArrayOf_CSV_Files_TDR)
                {
                    MainTextBox.Text += file + "\n";
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please, download the folder!");
            }
           
        }

        private void CuttingData(object sender, RoutedEventArgs e)
        {
            if (ISLoadFolder)
            {
                DirectoryInfo CuttedDir = new DirectoryInfo(Fbd_SelectPath + @"\CuttedFiles");
                if (CuttedDir.Exists)
                {
                    FileInfo[] files = CuttedDir.GetFiles();
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
                else
                    CuttedDir.Create();

                
                double deepest_sensor = 0;
                TemperatureFile deepest_sensor_file = new TemperatureFile();

                for(int i = 0; i < ArrayOf_CSV_Files_TR.Length; i++)
                {
                    if (ArrayOf_CSV_Files_TR[i].DepthOfImmersion > deepest_sensor)
                    {
                        deepest_sensor = ArrayOf_CSV_Files_TR[i].DepthOfImmersion;
                        deepest_sensor_file = ArrayOf_CSV_Files_TR[i];
                    }
                }



                deepest_sensor_file.Cutting_TR_Files();
                Number_of_first_mesure = deepest_sensor_file.number_of_first_mesure;
                Number_of_last_mesure = deepest_sensor_file.number_of_last_mesure;

                for (int i = 0; i < ArrayOf_CSV_Files_TR.Length; i++)
                {
                    ArrayOf_CSV_Files_TR[i].Cutting_TR_Files(Number_of_first_mesure, Number_of_last_mesure);
                    ArrayOf_CSV_Files_TR[i].CountAverage();
                   
                    using (StreamWriter sw = new StreamWriter(File.Create(System.IO.Path.Combine(CuttedDir.FullName, "Cutted_" + ArrayOf_CSV_Files_TR[i].MainFile.Name))))
                    {
                        for (int j = 0; j < ArrayOf_CSV_Files_TR[i].HatOfFile.Length; j++)
                        {
                            sw.WriteLine(ArrayOf_CSV_Files_TR[i].HatOfFile[j]);
                        }
                        for (int k = 0; k < ArrayOf_CSV_Files_TR[i].StrMesures.Count(); k++)
                        {
                            sw.WriteLine(ArrayOf_CSV_Files_TR[i].StrMesures[k]);
                        }
                    }
                }

                for (int i = 0; i < ArrayOf_CSV_Files_TDR.Length; i++)
                {
                    ArrayOf_CSV_Files_TDR[i].Cutting_TDR_Files(Number_of_first_mesure, Number_of_last_mesure);
                    ArrayOf_CSV_Files_TDR[i].CountAverageTDR();
                    
                    using (StreamWriter sw = new StreamWriter(File.Create(System.IO.Path.Combine(CuttedDir.FullName, "Cutted_" + ArrayOf_CSV_Files_TDR[i].MainFile.Name))))
                    {
                        for (int j = 0; j < ArrayOf_CSV_Files_TDR[i].HatOfFile.Length; j++)
                        {
                            sw.WriteLine(ArrayOf_CSV_Files_TDR[i].HatOfFile[j]);
                        }
                        for (int k = 0; k < ArrayOf_CSV_Files_TDR[i].StrMesures.Count(); k++)
                        {
                            sw.WriteLine(ArrayOf_CSV_Files_TDR[i].StrMesures[k]);
                        }
                    }
                }

                System.Windows.MessageBox.Show("Cutting was successfully!");
            }
            else
            {
                System.Windows.MessageBox.Show("Please, download the folder!");
            }
        }

        private void Calculating(object sender, RoutedEventArgs e)
        {
            if (ISLoadFolder)
            {
                //тут должны быть рассчеты
            }
            else
            {
                System.Windows.MessageBox.Show("Please, download the folder!");
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
