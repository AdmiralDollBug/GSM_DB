﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GSM_DB
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ImportMenuItem_Click(object sender, RoutedEventArgs e) {
            ImportPage importPage = new ImportPage();
            importPage.ShowDialog();
        }

        private void ExportMenuItem_Click(object sender, RoutedEventArgs e) {
            ExportPage exportPage = new ExportPage();
            exportPage.ShowDialog();
        }

        private void InqueryBTSMenuItem_Click(object sender, RoutedEventArgs e) {
            InqueryBTSPage inqueryPage = new InqueryBTSPage();
            inqueryPage.ShowDialog();
        }

        private void inqueryCellIDMenuItem_Click(object sender, RoutedEventArgs e) {
            InqueryCellPage inqueryPage = new InqueryCellPage();
            inqueryPage.ShowDialog();
        }

        private void inqueryTrafficeInfoMenuItem_Click(object sender, RoutedEventArgs e) {
            ImqueryTrafficInfoPage inqueryTrafficInfoPage = new ImqueryTrafficInfoPage();
            inqueryTrafficInfoPage.ShowDialog();
        }
    }
}
