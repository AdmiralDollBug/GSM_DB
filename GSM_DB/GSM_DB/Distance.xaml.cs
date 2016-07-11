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
    /// Distance.xaml 的交互逻辑
    /// </summary>
    public partial class Distance : Window
    {

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connStr = "Data Source=HUANGTIANSHUO\\SQLEXPRESS;UID=sa;PWD=123;Initial Catalog=GSM_DB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand("getCellIDList", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.comboBox.Items.Add((int)reader[0]);
            }
            reader.Close();
            this.comboBox.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (char ch in this.textBox1.Text)
            {
                if (char.IsDigit(ch))//是否为数字
                { }
                else
                {
                    System.Windows.MessageBox.Show("请输入数字", "错误", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    this.textBox1.Text = "";
                    break;
                }
            }


        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            string CID = comboBox.Text.ToString();
            string dis = textBox1.Text.ToString();
            if (CID == "" || dis == "")
            {
                System.Windows.MessageBox.Show("信息输入不完整，请检查。", "错误", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                int Cid = Convert.ToInt16(CID);
                int Dis = Convert.ToInt16(dis);
                string connStr = "Data Source=HUANGTIANSHUO\\SQLEXPRESS;UID=sa;PWD=123;Initial Catalog=GSM_DB;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connStr);
                SqlCommand command = new SqlCommand("setAdjDist", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlParme;
                sqlParme = command.Parameters.Add("@ACELLID", SqlDbType.Int);
                sqlParme.Direction = ParameterDirection.Input;
                sqlParme.Value = Cid;
                sqlParme = command.Parameters.Add("@Threshold", SqlDbType.Real);
                sqlParme.Direction = ParameterDirection.Input;
                sqlParme.Value = Dis;
                connection.Open();
                command.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter();
                SqlCommand command1 = new SqlCommand("getAdjDist", connection);
                command1.CommandType = System.Data.CommandType.StoredProcedure;
                sda.SelectCommand = command1;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];

                dataGrid1.ColumnWidth = 225;
                dataGrid1.ItemsSource = dt.DefaultView;


            }

        }



        public Distance()
        {
            InitializeComponent();
        }
    }
}
