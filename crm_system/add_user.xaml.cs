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
    /// Логика взаимодействия для add_user.xaml
    /// </summary>
    public partial class add_user : Window
    {
        SqlConnection connection;
        public static string id_user = null;
        public add_user()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Name.Text != "" && Name.Text != "" && second_name.Text != "" && Login.Text != "" && Pass.Password != "" && rols.Text != "")
                {
                    if (id_user == null)
                    {
                        connection.Open();
                        SqlCommand add_user = new SqlCommand("insert into users (name, surname, second_name, login, password, rol) values (@name, @surname, @second_name, @login, @password, @rol)", connection);
                        add_user.Parameters.AddWithValue("name", Name.Text);
                        add_user.Parameters.AddWithValue("surname", Name.Text);
                        add_user.Parameters.AddWithValue("second_name", second_name.Text);
                        add_user.Parameters.AddWithValue("login", Login.Text);
                        add_user.Parameters.AddWithValue("password", Pass.Password);
                        add_user.Parameters.AddWithValue("rol", rols.SelectedValue);
                        add_user.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        connection.Open();
                        SqlCommand upd_user = new SqlCommand("update users set name = @name , surname = @surname , second_name = @second_name, login = @login, password = @password, rol = @rol where id = @id", connection);
                        upd_user.Parameters.AddWithValue("name", Name.Text);
                        upd_user.Parameters.AddWithValue("surname", Surname.Text);
                        upd_user.Parameters.AddWithValue("second_name", second_name.Text);
                        upd_user.Parameters.AddWithValue("login", Login.Text);
                        upd_user.Parameters.AddWithValue("password", Pass.Password);
                        upd_user.Parameters.AddWithValue("rol", rols.SelectedValue);
                        upd_user.Parameters.AddWithValue("id", id_user);
                        upd_user.ExecuteNonQuery();
                        connection.Close();
                    }
                    Close();
                    ((MainWindow)this.Owner).refresh();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Предупреждение!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<comboItems> comboItems = new List<comboItems>();
                connection = new SqlConnection(MainWindow.constr);
                connection.Open();
                SqlCommand sel_rols = new SqlCommand("select t.* from rols t", connection);
                SqlDataReader read_rols = sel_rols.ExecuteReader();
                while (read_rols.Read())
                {
                    comboItems.Add(new comboItems(read_rols["id"].ToString(), read_rols["name"].ToString()));
                }
                read_rols.Close();
                rols.ItemsSource = comboItems;
                if (id_user != null)
                {
                    SqlCommand sel_user_info = new SqlCommand("select t.* from users t where t.id = @id", connection);
                    sel_user_info.Parameters.AddWithValue("id", id_user);
                    SqlDataReader read_user_info = sel_user_info.ExecuteReader();
                    if (read_user_info.Read())
                    {
                        Name.Text = read_user_info["name"].ToString();
                        Surname.Text = read_user_info["surname"].ToString();
                        second_name.Text = read_user_info["second_name"].ToString();
                        Login.Text = read_user_info["login"].ToString();
                        Pass.Password = read_user_info["password"].ToString();
                        rols.SelectedValue = read_user_info["rol"].ToString();
                    }
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
