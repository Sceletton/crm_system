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
    /// Логика взаимодействия для add_sotr.xaml
    /// </summary>
    public partial class add_sotr : Window
    {
        CheckFields check = new CheckFields();
        MySqlConnection connection;
        public static string id_org = null;
        public static string id_sotr = null;
        public add_sotr()
        {
            InitializeComponent();
        }

        public class orgs_data
        {
            public int id { get; set; }
            public string name { get; set; }


            public orgs_data(int Id, string Name)
            {
                id = Id;
                name = Name;
            }

        }
        private void add_or_upd_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CheckForInternetConnection())
            {
                try
                {
                    check.CheckNullFields(new[] { name, surname, lastname });
                    if (name.Text != "" && surname.Text != "" && lastname.Text != "" && orgs.Text != "" && job_title.Text != "")
                    {
                        if (id_sotr == null)
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("insert into workers (name, surname, second_name, id_org, id_post) values (@name, @surname, @second_name, @id_org, @id_post)", connection);
                            command.Parameters.AddWithValue("name", name.Text);
                            command.Parameters.AddWithValue("surname", surname.Text);
                            command.Parameters.AddWithValue("second_name", lastname.Text);
                            command.Parameters.AddWithValue("id_org", orgs.SelectedValue);
                            command.Parameters.AddWithValue("id_post", job_title.SelectedValue);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("update workers set name = @name, surname = @surname, second_name = @second_name, id_org = @id_org, id_post = @id_post where id = @id_emp", connection);
                            command.Parameters.AddWithValue("id_emp", id_sotr);
                            command.Parameters.AddWithValue("name", name.Text);
                            command.Parameters.AddWithValue("surname", surname.Text);
                            command.Parameters.AddWithValue("second_name", lastname.Text);
                            command.Parameters.AddWithValue("id_org", orgs.SelectedValue);
                            command.Parameters.AddWithValue("id_post", job_title.SelectedValue);
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                        ((MainWindow)this.Owner).refresh("emps");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CheckForInternetConnection())
            { 
            List<comboItems> jobs = new List<comboItems>();
            List<comboItems> Orgs = new List<comboItems>();
                try
                {
                    connection = new MySqlConnection(MainWindow.constr);
                    connection.Open();
                    MySqlCommand sel_orgs = new MySqlCommand("select id, name from org order by name ", connection);
                    MySqlDataReader orgs_read = sel_orgs.ExecuteReader();
                    while (orgs_read.Read())
                    {
                        Orgs.Add(new comboItems(orgs_read["id"].ToString(), orgs_read["name"].ToString()));
                    }
                    orgs_read.Close();
                    orgs.ItemsSource = Orgs;
                    if (id_org != null)
                    {
                        orgs.SelectedValue = id_org;

                    }

                    MySqlCommand sel_jobs = new MySqlCommand("select id, name from posts order by name ", connection);
                    MySqlDataReader read_jobs = sel_jobs.ExecuteReader();
                    while (read_jobs.Read())
                    {
                        jobs.Add(new comboItems(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                    }
                    read_jobs.Close();
                    job_title.ItemsSource = jobs;

                    if (id_sotr != null)
                    {
                        MySqlCommand get_workers = new MySqlCommand("select name, surname, second_name, id_org, id_post from workers where id = @id", connection);
                        get_workers.Parameters.AddWithValue("id", id_sotr);
                        MySqlDataReader read_workers = get_workers.ExecuteReader();
                        if (read_workers.Read())
                        {
                            orgs.SelectedValue = int.Parse(read_workers["id_org"].ToString());
                            name.Text = read_workers["name"].ToString();
                            surname.Text = read_workers["surname"].ToString();
                            lastname.Text = read_workers["second_name"].ToString();
                            job_title.SelectedValue = int.Parse(read_workers["id_post"].ToString());
                        }
                        read_workers.Close();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            name.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(name, "alpha");
        }

        private void surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(surname, "alpha");
        }

        private void lastname_TextChanged(object sender, TextChangedEventArgs e)
        {
            lastname.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(lastname, "alpha");
        }
    }
}
