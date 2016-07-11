using System;
using System.Collections.Generic;
using System.Data;
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
    /// InqueryｍMinuteTrafficInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class InqueryｍMinuteTrafficInfoPage : Window
    {
        public InqueryｍMinuteTrafficInfoPage() {
            InitializeComponent();
            dataPicker.SelectedDate = new DateTime(2007, 10, 1);
            dataPicker.DisplayDateStart = new DateTime(2007, 9, 1);
            dataPicker.DisplayDateEnd = new DateTime(2007, 11, 1);
            timePickerPrev.AllowTextInput = false;
            timePickerPrev.Value = new DateTime(2007, 11, 1, 0, 0, 0);
            timePickerNext.AllowTextInput = false;
            timePickerNext.Value = new DateTime(2007, 11, 1, 0, 0, 0);
            string connStr = "Data Source=" + DatabaseInfo.dataSource + ";UID=" + DatabaseInfo.uid + ";PWD=" + DatabaseInfo.pwd + ";Initial Catalog=GSM_DB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connStr)) {
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
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e) {
            DateTime prevTime = (DateTime)timePickerPrev.Value;
            DateTime nextTime = (DateTime)timePickerNext.Value;
            DateTime date = (DateTime)dataPicker.SelectedDate;
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, prevTime.Hour, prevTime.Minute, prevTime.Second);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, nextTime.Hour, nextTime.Minute, nextTime.Second);
            if (startTime.CompareTo(endTime) >= 0) {
                System.Windows.MessageBox.Show("起始日期时间必须早于终止日期时间。", "日期填写错误",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
            string connStr = "Data Source=" + DatabaseInfo.dataSource + ";UID=" + DatabaseInfo.uid + ";PWD=" + DatabaseInfo.pwd + ";Initial Catalog=GSM_DB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connStr)) {
                SqlCommand command = new SqlCommand("setHourlyPhonecallDetail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter paramCellID = new SqlParameter("@ACELLID", SqlDbType.Int);
                paramCellID.Value = (int)comboBoxCellID.SelectedItem;
                command.Parameters.Add(paramCellID);
                SqlParameter paramDate = new SqlParameter("@ADATE", SqlDbType.Int);
                paramDate.Value = (date.Year - 2000) * 10000 + date.Month * 100 + date.Day;
                command.Parameters.Add(paramDate);
                SqlParameter paramSTime = new SqlParameter("@ASTIME", SqlDbType.Int);
                paramSTime.Value = startTime.Hour * 10000 + startTime.Minute * 100 + startTime.Second;
                command.Parameters.Add(paramSTime);
                SqlParameter paramETime = new SqlParameter("@AETIME", SqlDbType.Int);
                paramETime.Value = endTime.Hour * 10000 + endTime.Minute * 100 + endTime.Second;
                command.Parameters.Add(paramETime);
                connection.Open();
                command.ExecuteNonQuery();
            }
            using (SqlConnection connection = new SqlConnection(connStr)) {
                SqlCommand command = new SqlCommand("getHourlyPhonecallDetail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable("HourlyPhoneCallDetail");
                adapter.Fill(table);
                series.IndependentValuePath = "HTime";
                switch (comboBoxType.SelectedIndex) {
                    case 0:
                        series.DependentValuePath = "HAvgTraff";
                        chart.Title = "小时级平均话务量";
                        break;
                    case 1:
                        series.DependentValuePath = "HTraffPerLine";
                        chart.Title = "小时级每线话务量";
                        break;
                    case 2:
                        series.DependentValuePath = "HAvgCongsnum";
                        chart.Title = "小时级拥塞率";
                        break;
                    case 3:
                        series.DependentValuePath = "HThTraffRate";
                        chart.Title = "小时级半速率话务比例";
                        break;
                }
                series.Title = comboBoxCellID.Text;
                series.DataContext = table.DefaultView;
                grid.Visibility = Visibility.Visible;
            }
        }

        private void hideButton_Click(object sender, RoutedEventArgs e) {
            grid.Visibility = Visibility.Hidden;
        }
    }
}
