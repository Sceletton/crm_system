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
using MySql.Data.MySqlClient;

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для a_or_u_hanboxes.xaml
    /// </summary>
    public partial class a_or_u_hanboxes : Window
    {
        public static int hanbox_id = -1;
        public static string type = null;
        MySqlConnection connection;
        public a_or_u_hanboxes()
        {
            InitializeComponent();
            try
            {
                connection = new MySqlConnection(MainWindow.constr);
                if (hanbox_id != -1)
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select t.name from " + type + " t where t.id = @id", connection);
                    command.Parameters.AddWithValue("id", hanbox_id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        value.Text = reader["name"].ToString();
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch
            {
                connection.Close();
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "";
                if (hanbox_id == -1)
                {
                    query = "insert into " + type + " (name) values ('" + value.Text + "')";
                }
                else
                {
                    query = "update " + type + " set name = '" + value.Text + "' where id = " + hanbox_id;
                }
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                ((MainWindow)this.Owner).refresh("handbooks");
                Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
