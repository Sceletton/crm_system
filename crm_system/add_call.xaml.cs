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
using System.Data;
using System.Data.SqlClient;
using crm_system.DB;

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для add_call.xaml
    /// </summary>
    public partial class add_call : Window
    {
        SqlConnection connection;
        public static string id_org = null;
        public static string id_call = null;
        CheckFields check = new CheckFields();
        public add_call()
        {
            InitializeComponent();
        }

        private void add_cal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (org.Text != "" && call_traget.Text != "" && call_date.Text != "")
                {
                    if (id_call == null)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("insert into calls (date_cal, id_org, call_target,status_call) values (@date_cal, @id_org, @call_target, 0)", connection);
                        command.Parameters.AddWithValue("date_cal", Convert.ToDateTime(call_date.SelectedDate.ToString()).ToShortDateString());
                        command.Parameters.AddWithValue("id_org", org.SelectedValue);
                        command.Parameters.AddWithValue("call_target", call_traget.Text);
                        command.ExecuteNonQuery();
                        connection.Close();
                        Close();
                    }
                    else
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("update calls set date_cal = @date_cal, id_org = @id_org, call_target = @call_target where id = @id", connection);
                        command.Parameters.AddWithValue("date_cal", Convert.ToDateTime(call_date.SelectedDate.ToString()).ToShortDateString());
                        command.Parameters.AddWithValue("id_org", org.SelectedValue);
                        command.Parameters.AddWithValue("call_target", call_traget.Text);
                        command.Parameters.AddWithValue("id", id_call);
                        command.ExecuteNonQuery();
                        connection.Close();
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<comboItems> Orgs = new List<comboItems>();
            connection = new SqlConnection(MainWindow.constr);
            connection.Open();
            SqlCommand sel_orgs = new SqlCommand("select id, name from org order by name ", connection);
            SqlDataReader orgs_read = sel_orgs.ExecuteReader();
            while (orgs_read.Read())
            {
                Orgs.Add(new comboItems(orgs_read["id"].ToString(), orgs_read["name"].ToString()));
            }
            orgs_read.Close();
            org.ItemsSource = Orgs;
            if (id_org != null)
            {
                org.SelectedValue = id_org;

            }
            if (id_call != null)
            {
                SqlCommand sel_calls = new SqlCommand("select t.id_org, t.date_cal, t.call_target from calls t where t.id = @id_call", connection);
                sel_calls.Parameters.AddWithValue("id_call",id_call);
                SqlDataReader calls_read = sel_calls.ExecuteReader();
                if (calls_read.Read())
                {
                    org.SelectedValue = int.Parse(calls_read["id_org"].ToString());
                    call_date.Text = calls_read["date_cal"].ToString();
                    call_traget.Text = calls_read["call_target"].ToString();
                }
                add_cal.Content = "Сохранеть";
            }
            connection.Close();
        }

        private void call_traget_TextChanged(object sender, TextChangedEventArgs e)
        {
            call_traget.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(call_traget);
        }
    }
}
