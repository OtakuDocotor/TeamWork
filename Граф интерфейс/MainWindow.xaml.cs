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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Граф_интерфейс
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo[] filesArray;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadingFiles(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo directory = new DirectoryInfo(FBD.SelectedPath);
                filesArray = directory.GetFiles();
            }
        }
        private void ViewingFiles(object sender, RoutedEventArgs e)
        {
            MainTextBox.Text = "";
            foreach (var file in filesArray)
            {
                MainTextBox.Text += file + "\n";
            }
        }

        private void CuttingData(object sender, RoutedEventArgs e)
        {

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
