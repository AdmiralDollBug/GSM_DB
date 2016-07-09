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
using Microsoft.Win32;

namespace GSM_DB
{
    /// <summary>
    /// Import.xaml 的交互逻辑
    /// </summary>
    public partial class Import : Window
    {
        public Import() {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openDileDialog = new OpenFileDialog();
            openDileDialog.FileName = "v1";
            openDileDialog.DefaultExt = ".xls";
            openDileDialog.Filter = "Excel File|*.xls";
            openDileDialog.Multiselect = false;
            Nullable<bool> result = openDileDialog.ShowDialog();
            if(result == true) {
                string fileName = openDileDialog.FileName;
                Console.WriteLine(fileName);
            }
        }
    }
}
