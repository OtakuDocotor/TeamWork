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
        TemperatureFile[] ArrayOf_CSV_Files;
        FileInfo[] ArrayOfAllFiles;
        string patternOf_CSV_File = @"[0-9].[0-9]+\sm_(TR|TDR)_[0-9]+_[0-9]+_[0-9]+.xls",
            Fbd_SelectPath;

        public static TemperatureFile[] Find(FileInfo[] AllFiles, string pattern)
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

            ArrayOf_CSV_Files = Find(ArrayOfAllFiles, patternOf_CSV_File);

        }
        private void ViewingFiles(object sender, RoutedEventArgs e)
        {
            MainTextBox.Text = "";
            foreach (var file in ArrayOf_CSV_Files)
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
