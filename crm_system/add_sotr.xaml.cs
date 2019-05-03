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
    /// Логика взаимодействия для add_sotr.xaml
    /// </summary>
    public partial class add_sotr : Window
    {
        SqlConnection connection;
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
            try
            {
                if (name.Text != "" && surname.Text != "" && lastname.Text != "" && orgs.Text != "" && job_title.Text != "")
                {
                    if (id_sotr == null)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("exec p_add_or_upd_workers @name = @name_sotr, @surname = @surname_w, @second_name = @lastname, @id_org = @id_organisat, @id_post = @id_job ", connection);
                        command.Parameters.AddWithValue("name_sotr", name.Text);
                        command.Parameters.AddWithValue("surname_w", surname.Text);
                        command.Parameters.AddWithValue("lastname", lastname.Text);
                        command.Parameters.AddWithValue("id_organisat", orgs.SelectedValue);
                        command.Parameters.AddWithValue("id_job", job_title.SelectedValue);
                        command.ExecuteNonQuery();
                        connection.Close();
                        //} else {
                        //    connection.Open();
                        //    SqlCommand command = new SqlCommand("exec p_add_or_upd_workers @id = id_sotr, @name = @name_sotr, @surname = @surname_w, @second_name = @lastname, @id_org = @id_organisat, @id_post = @id_job ", connection);
                        //    command.Parameters.AddWithValue("id_sotr", id);
                        //    command.Parameters.AddWithValue("name_sotr", name.Text);
                        //    command.Parameters.AddWithValue("surname_w", surname.Text);
                        //    command.Parameters.AddWithValue("lastname", lastname.Text);
                        //    command.Parameters.AddWithValue("id_organisat", orgs.SelectedIndex);
                        //    command.Parameters.AddWithValue("id_job", job_title.SelectedIndex);
                        //    command.ExecuteNonQuery();
                        //    connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Предупреждение!");
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
            List<comboItems> jobs = new List<comboItems>();
            List<comboItems> Orgs = new List<comboItems>();
            try
            {
                connection = new SqlConnection(MainWindow.constr);
                connection.Open();
                SqlCommand sel_orgs = new SqlCommand("select id, name from org order by name ", connection);
                SqlDataReader orgs_read = sel_orgs.ExecuteReader();
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

                SqlCommand sel_jobs = new SqlCommand("select id, name from posts order by name ", connection);
                SqlDataReader read_jobs = sel_jobs.ExecuteReader();
                while (read_jobs.Read())
                {
                    jobs.Add(new comboItems(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                }
                read_jobs.Close();
                job_title.ItemsSource = jobs;

                if (id_sotr != null)
                {
                    SqlCommand get_workers = new SqlCommand("select name, surname, second_name, id_org, id_post from workers where id = @id", connection);
                    get_workers.Parameters.AddWithValue("id", id_sotr);
                    SqlDataReader read_workers = get_workers.ExecuteReader();
                    if (read_workers.Read())
                    {
                        orgs.SelectedIndex = int.Parse(read_workers["id_org"].ToString());
                        name.Text = read_workers["name"].ToString();
                        surname.Text = read_workers["surname"].ToString();
                        lastname.Text = read_workers["second_name"].ToString();
                        job_title.SelectedIndex = int.Parse(read_workers["id_post"].ToString());
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
    }
}
