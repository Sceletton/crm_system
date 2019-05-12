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
using MySql.Data.MySqlClient;
using crm_system.DB;


namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для addOrgn.xaml
    /// </summary>
    public partial class addOrgn : Window
    {
        CheckFields check = new CheckFields();
        MySqlConnection connection;
        public static string id = null;
        public addOrgn()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prioriry.Items.Add("Низский");
            prioriry.Items.Add("Средний");
            prioriry.Items.Add("Высокий");
            connection = new MySqlConnection(MainWindow.constr);
            connection.Open();
            List<comboItems> comboItem = new List<comboItems>();
            List<comboItems> comboItem_kur = new List<comboItems>();
            MySqlCommand citys = new MySqlCommand("select name,id from cities", connection);
            MySqlDataReader read_citys = citys.ExecuteReader();
            while (read_citys.Read())
            {
                comboItem.Add(new comboItems(read_citys["id"].ToString(), read_citys["name"].ToString()));
            }
            city.ItemsSource = comboItem;
            read_citys.Close();

            MySqlCommand kurator = new MySqlCommand("select id, name, surname, second_name from users", connection);
            MySqlDataReader kurator_read = kurator.ExecuteReader();
            while (kurator_read.Read())
            {
                comboItem_kur.Add(new comboItems(kurator_read["id"].ToString(), kurator_read["surname"].ToString() + " " + kurator_read["Name"].ToString() + " " + kurator_read["second_name"].ToString()));
            }
            kyrator.ItemsSource = comboItem_kur;
            kurator_read.Close();
            if (id != null)
            {
                try
                {
                    MySqlCommand select_org = new MySqlCommand("select name, city, phone, kurator, code, priority, status from org where id = @id", connection);
                    select_org.Parameters.AddWithValue("id", id);
                    MySqlDataReader read_org = select_org.ExecuteReader();
                    if (read_org.Read())
                    {
                        name.Text = read_org["name"].ToString();

                        phone.Text = read_org["phone"].ToString();
                        kyrator.SelectedValue = int.Parse(read_org["kurator"].ToString());
                        code.Text = read_org["code"].ToString();
                        prioriry.SelectedIndex = int.Parse(read_org["priority"].ToString());
                        //status.selectedindex = int.parse(read_org["status"].tostring());
                        city.SelectedValue = int.Parse(read_org["city"].ToString());
                    }
                }
                catch
                {
                    connection.Close();
                }
            }
            connection.Close();
        }

        private void add_or_upd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                check.CheckNullFields(new[] { name,code,phone });
                if (name.Text != "" && city.Text != "" && phone.Text != "" && kyrator.Text != "" && code.Text != "" && prioriry.Text != "")
                {
                    if (id == null)
                    {
                        connection.Open();
                        MySqlCommand add_org = new MySqlCommand("insert into org (name,city,phone,status,kurator,code,priority) values (@name,@city,@phone,0,@kurator,@code,@priority)", connection);
                        add_org.Parameters.AddWithValue("name", name.Text);
                        add_org.Parameters.AddWithValue("city", city.SelectedValue);
                        add_org.Parameters.AddWithValue("phone", phone.Text);
                        add_org.Parameters.AddWithValue("kurator", kyrator.SelectedValue);
                        add_org.Parameters.AddWithValue("code", code.Text);
                        add_org.Parameters.AddWithValue("priority", prioriry.SelectedIndex);
                        add_org.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        connection.Open();
                        MySqlCommand upd_org = new MySqlCommand("update org set name = @name, city = @city, phone = @phone, kurator = @kurator, code = @code, priority = @priority where id = @id", connection);
                        upd_org.Parameters.AddWithValue("id", id);
                        upd_org.Parameters.AddWithValue("name", name.Text);
                        upd_org.Parameters.AddWithValue("city", city.SelectedValue);
                        upd_org.Parameters.AddWithValue("phone", phone.Text);
                        upd_org.Parameters.AddWithValue("kurator", kyrator.SelectedValue);
                        upd_org.Parameters.AddWithValue("code", code.Text);
                        upd_org.Parameters.AddWithValue("priority", prioriry.SelectedIndex);
                        upd_org.ExecuteNonQuery();
                        connection.Close();
                    }
                    try
                    {
                        ((MainWindow)this.Owner).refresh();
                    }
                    catch
                    {

                    }
                    Close();
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

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            name.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(name);
        }

        private void code_TextChanged(object sender, TextChangedEventArgs e)
        {
            code.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(name);
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            phone.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(name, "number");
        }
    }
}
