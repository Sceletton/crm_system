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

        public bool in_arr(string[] ar, string value)
        {
            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                check.CheckNullFields(new[] { Name, Surname, second_name, Login });
                if (Name.Text != "" && Surname.Text != "" && second_name.Text != "" && Login.Text != "" && Pass.Password != "" && rols.Text != "" && exception.Height == 0)
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
                        int us_cnt = 0;
                        string[] us_id =  null, rols_id = null;
                        MySqlCommand sel_us_cnt = new MySqlCommand("select count(1) as count, REPLACE(GROUP_CONCAT(t.id),',',';') as users_id, REPLACE(GROUP_CONCAT(tt.id),',',';') as rols_id from users t join rols tt on tt.id = t.rol where tt.rights like '%9%' and tt.rights like '%10%'", connection);
                        MySqlDataReader reader = sel_us_cnt.ExecuteReader();
                        while(reader.Read())
                        {
                            us_cnt = int.Parse(reader["count"].ToString());
                            us_id = reader["users_id"].ToString().Split(';');
                            rols_id = reader["rols_id"].ToString().Split(';');
                        }
                        reader.Close();
                        if (us_cnt == 1 && in_arr(us_id, id_user) && !in_arr(rols_id, rols.SelectedValue.ToString()))
                        {
                            MessageBox.Show("В системе должна быть хотя бы однин пользователь, с правами на разделы: [Пользователи] и [Роли]", "Предупреждение");
                            connection.Close();
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
                            if (id_user == MainWindow.user_id.ToString())
                            {
                                MainWindow.rol_id = rols.SelectedValue.ToString();
                            }
                            Close();
                            connection.Close();
                            ((MainWindow)this.Owner).refresh();
                            ((MainWindow)this.Owner).aunt_result();
                            ((MainWindow)this.Owner).exit.Visibility = Visibility.Visible;
                            ((MainWindow)this.Owner).exit.Height = 39;
                            ((MainWindow)this.Owner).re_aunt.Visibility = Visibility.Visible;
                            ((MainWindow)this.Owner).re_aunt.Height = 39;
                        }
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
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //    connection.Close();
            //    MessageBox.Show(ex.Message.ToString());
            //}
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
            try
            {
                Login.BorderBrush = Brushes.Black;
                check.CheckFieldsCaption(Login);
                exception.Height = 0;
                connection.Open();
                MySqlCommand command = new MySqlCommand("select t.id from users t where t.login = @login and (@id_user is null or t.id != @id_user)", connection);
                command.Parameters.AddWithValue("login", Login.Text);
                command.Parameters.AddWithValue("id_user", id_user);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    exception.Height = 15;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
