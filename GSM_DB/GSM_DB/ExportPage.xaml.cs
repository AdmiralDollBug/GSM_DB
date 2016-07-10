using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;
using System.IO;

namespace GSM_DB
{
    /// <summary>
    /// ExportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ExportPage : Page
    {
        string[] infoType = { "MS", "MSC", "BSC", "BTS", "CELL", "CELLFREQ", "ANTENNA", "PHONECALL", "RTEST", "IDLEINFO" };
        public ExportPage() {
            InitializeComponent();
            for(int i = 0; i < infoType.Length; i++) {
                comboBox.Items.Add(infoType[i]);
            }
        }

        private void explorerButton_Click(object sender, RoutedEventArgs e) {
            if (comboBox.SelectedIndex == 0) {
                FolderBrowserDialog folderBrowseDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowseDialog.ShowDialog();
                if(result == DialogResult.OK) {
                    filePathTextBox.Text = folderBrowseDialog.SelectedPath;
                }
            }
            else {
                Microsoft.Win32.SaveFileDialog saveDileDialog = new Microsoft.Win32.SaveFileDialog();
                saveDileDialog.FileName = infoType[comboBox.SelectedIndex - 1];
                saveDileDialog.DefaultExt = ".xls";
                saveDileDialog.Filter = "Excel File|*.xls";
                Nullable<bool> result = saveDileDialog.ShowDialog();
                if (result == true) {
                    filePathTextBox.Text = saveDileDialog.FileName;
                }
            }
            
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            if(filePathTextBox.Text == "") {
                System.Windows.MessageBox.Show("请选定文件。", "文件未选定",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
            if(comboBox.SelectedIndex == 0) {
                for(int i = 0; i < infoType.Length; i++) {
                    ExcelSupport.ExportToTable(infoType[i], filePathTextBox.Text + "\\" + infoType[i]);
                }
            }
            else {
                if (Directory.Exists(filePathTextBox.Text)) {
                    System.Windows.MessageBox.Show("请选定文件而不是文件夹。", "文件未选定",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
                ExcelSupport.ExportToTable(infoType[comboBox.SelectedIndex - 1], filePathTextBox.Text);
            }
        }

    }
}
