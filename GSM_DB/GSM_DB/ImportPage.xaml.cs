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
using Microsoft.Win32;
using System.IO;

namespace GSM_DB
{
    /// <summary>
    /// ImportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImportPage : Page
    {
        public ImportPage() {
            InitializeComponent();
        }

        private void explorerButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openDileDialog = new OpenFileDialog();
            openDileDialog.FileName = "v1";
            openDileDialog.DefaultExt = ".xls";
            openDileDialog.Filter = "Excel File|*.xls";
            openDileDialog.Multiselect = false;
            Nullable<bool> result = openDileDialog.ShowDialog();
            if (result == true) {
                filePathTextBox.Text = openDileDialog.FileName;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(filePathTextBox.Text == "") {
                System.Windows.MessageBox.Show("请选定文件。", "文件未选定",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
            if (File.Exists(filePathTextBox.Text)) {
                textBlockProgress.Text = "导入中...";
                ExcelSupport.ImportFromExcel(filePathTextBox.Text);
                textBlockProgress.Text = "导入完成";
            }
            else {
                System.Windows.MessageBox.Show("文件不存在。", "请检查",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
        }
    }
}
