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
        string patternOf_CSV_File_TR = @"[0-9].[0-9]+\sm_(TR)_[0-9]+_[0-9]+_[0-9]+.csv",
               patternOf_CSV_File_TDR = @"[0-9].[0-9]+\sm_(TDR)_[0-9]+_[0-9]+_[0-9]+.csv",
            Fbd_SelectPath;

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
            }

            ArrayOf_CSV_Files_TR = FindTR(ArrayOfAllFiles, patternOf_CSV_File_TR);
            ArrayOf_CSV_Files_TDR = FindTDR(ArrayOfAllFiles, patternOf_CSV_File_TDR);


        }
        private void ViewingFiles(object sender, RoutedEventArgs e)
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

        private void CuttingData(object sender, RoutedEventArgs e)
        {
            DirectoryInfo CuttedDir = new DirectoryInfo(Fbd_SelectPath + @"\CuttedFiles");
            if (CuttedDir.Exists)
            {
                FileInfo[] files = CuttedDir.GetFiles();
                foreach(var file in files)
                {
                    file.Delete();
                }
            }
            else
                CuttedDir.Create();

            for(int i = 0; i < ArrayOf_CSV_Files_TR.Length; i++)
            {
                ArrayOf_CSV_Files_TR[i].Cutting_TR_Files();
                ArrayOf_CSV_Files_TR[i].CountAverage();
                var file = File.Create(System.IO.Path.Combine(CuttedDir.FullName, "Cutted_" + ArrayOf_CSV_Files_TR[i].MainFile.Name));
                string Name = file.Name;
                file.Close();
                using (StreamWriter sw = new StreamWriter(Name))
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
        }


        private void Drawing(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
