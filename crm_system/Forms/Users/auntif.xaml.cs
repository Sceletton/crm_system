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

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для auntif.xaml
    /// </summary>
    public partial class auntif : Window
    {
        public auntif()
        {
            InitializeComponent();
        }

        public void auntification()
        {
            if (MainWindow.CheckForInternetConnection())
            {
                MySqlConnection connection = new MySqlConnection(MainWindow.constr);
                //try
                //{
                    string pass = null;
                    connection.Open();
                    MySqlCommand get_pass = new MySqlCommand("select t.id as user_id, t.password, t.rol, tt.name as rol_name from users t join rols tt on tt.id = t.rol where t.login = @login", connection);
                    get_pass.Parameters.AddWithValue("login", Login.Text);
                    MySqlDataReader reader_pass = get_pass.ExecuteReader();
                    if (reader_pass.Read())
                    {
                        MainWindow.user_id = int.Parse(reader_pass["user_id"].ToString());
                        pass = reader_pass["password"].ToString();
                        MainWindow.rol_id = reader_pass["rol"].ToString();
                    }
                    if (Pass.Password == pass)
                    {
                        MainWindow.auntif = true;

                        ((MainWindow)this.Owner).aunt_result();
                        ((MainWindow)this.Owner).exit.Visibility = Visibility.Visible;
                        ((MainWindow)this.Owner).exit.Height = 39;
                        ((MainWindow)this.Owner).re_aunt.Visibility = Visibility.Visible;
                        ((MainWindow)this.Owner).re_aunt.Height = 39;
                        ((MainWindow)this.Owner).aunt.Height = 0;
                        ((MainWindow)this.Owner).refresh();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("не верынй логин или пароль");
                    }
                    connection.Close();
                //}
                //catch (Exception ex)
                //{
                //    connection.Close();
                //    MessageBox.Show(ex.Message.ToString());
                //}
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            auntification();
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                auntification();
            }
        }

    }
}
