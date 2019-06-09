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
using System.IO;

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для add_user.xaml
    /// </summary>
    public partial class add_user : Window
    {
        CheckFields check = new CheckFields();
        MySqlConnection connection;
        public static string id_user = null;
        public add_user()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                check.CheckNullFields(new[] { Name, Surname, second_name, Login });
                if (Name.Text != "" && Surname.Text != "" && second_name.Text != "" && Login.Text != "" && Pass.Password != "" && rols.Text != "")
                {
                    connection.Open();
                    if (id_user == null)
                    {
                        MySqlCommand add_user = new MySqlCommand("insert into users (name, surname, second_name, login, password, rol) values (@name, @surname, @second_name, @login, @password, @rol)", connection);
                        add_user.Parameters.AddWithValue("name", Name.Text);
                        add_user.Parameters.AddWithValue("surname", Name.Text);
                        add_user.Parameters.AddWithValue("second_name", second_name.Text);
                        add_user.Parameters.AddWithValue("login", Login.Text);
                        add_user.Parameters.AddWithValue("password", Pass.Password);
                        add_user.Parameters.AddWithValue("rol", rols.SelectedValue);
                        add_user.ExecuteNonQuery();
                        //добавим запись в таблицу настроек.
                        MySqlCommand add_ini = new MySqlCommand("insert into settings (id_user, save_path) values (@id_user, @save_path)", connection);
                        add_ini.Parameters.AddWithValue("id_user", add_user.LastInsertedId);
                        add_ini.Parameters.AddWithValue("save_path", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                        add_ini.ExecuteNonQuery();
                    }
                    else
                    {
                        MySqlCommand upd_user = new MySqlCommand("update users set name = @name , surname = @surname , second_name = @second_name, login = @login, password = @password, rol = @rol where id = @id", connection);
                        upd_user.Parameters.AddWithValue("name", Name.Text);
                        upd_user.Parameters.AddWithValue("surname", Surname.Text);
                        upd_user.Parameters.AddWithValue("second_name", second_name.Text);
                        upd_user.Parameters.AddWithValue("login", Login.Text);
                        upd_user.Parameters.AddWithValue("password", Pass.Password);
                        upd_user.Parameters.AddWithValue("rol", rols.SelectedValue);
                        upd_user.Parameters.AddWithValue("id", id_user);
                        upd_user.ExecuteNonQuery();
                    }
                    Close();
                    connection.Close();
                    ((MainWindow)this.Owner).refresh();
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
            try
            {
                List<comboItems> comboItems = new List<comboItems>();
                connection = new MySqlConnection(MainWindow.constr);
                connection.Open();
                MySqlCommand sel_rols = new MySqlCommand("select t.* from rols t", connection);
                MySqlDataReader read_rols = sel_rols.ExecuteReader();
                while (read_rols.Read())
                {
                    comboItems.Add(new comboItems(read_rols["id"].ToString(), read_rols["name"].ToString()));
                }
                read_rols.Close();
                rols.ItemsSource = comboItems;
                
                if (id_user != null)
                {
                    MySqlCommand sel_user_info = new MySqlCommand("select t.* from users t where t.id = @id", connection);
                    sel_user_info.Parameters.AddWithValue("id", id_user);
                    MySqlDataReader read_user_info = sel_user_info.ExecuteReader();
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

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Name.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(Name, "alpha");
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Surname.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(Surname, "alpha");
        }

        private void second_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            second_name.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(second_name, "alpha");
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Login.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(Login);
        }
    }
}
