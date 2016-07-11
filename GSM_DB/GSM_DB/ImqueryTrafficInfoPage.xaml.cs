using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace GSM_DB
{
    /// <summary>
    /// ImqueryTrafficInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImqueryTrafficInfoPage : Window
    {
        public ImqueryTrafficInfoPage() {
            InitializeComponent();
            dataPicker.SelectedDate = new DateTime(2007, 10, 1);
            dataPicker.DisplayDateStart = new DateTime(2007, 9, 1);
            dataPicker.DisplayDateEnd = new DateTime(2007, 11, 1);
            timePickerPrev.AllowTextInput = false;
            timePickerPrev.Value = new DateTime(2007, 11, 1, 0, 0, 0);
            timePickerNext.AllowTextInput = false;
            timePickerNext.Value = new DateTime(2007, 11, 1, 0, 0, 0);
            string connStr = "Data Source=" + DatabaseInfo.dataSource + ";UID=" + DatabaseInfo.uid + ";PWD=" + DatabaseInfo.pwd + ";Initial Catalog=GSM_DB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand("getCellIDList", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                comboBoxCellID.Items.Add((int)reader[0]);
            }
            reader.Close();
            comboBoxCellID.SelectedIndex = 0;

        }

        private void buttonOk_Click(object sender, RoutedEventArgs e) {
            DateTime prevTime = (DateTime)timePickerPrev.Value;
            DateTime nextTime = (DateTime)timePickerNext.Value;
            DateTime date = (DateTime)dataPicker.SelectedDate;
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, prevTime.Hour, prevTime.Minute, prevTime.Second);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, nextTime.Hour, nextTime.Minute, nextTime.Second);
            if(startTime.CompareTo(endTime) >= 0) {
                System.Windows.MessageBox.Show("起始日期时间必须早于终止日期时间。", "日期填写错误",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }

        }
    }
}
