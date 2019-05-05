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
            SqlConnection connection = new SqlConnection(MainWindow.constr);
            try
            {
                string pass = null;
                connection.Open();
                SqlCommand get_pass = new SqlCommand("select t.password, t.rol from users t where t.login = @login", connection);
                get_pass.Parameters.AddWithValue("login", Login.Text);
                SqlDataReader reader_pass = get_pass.ExecuteReader();
                if (reader_pass.Read())
                {
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
                    Close();
                }
                else
                {
                    MessageBox.Show("не верынй логин или пароль");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message.ToString());
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
