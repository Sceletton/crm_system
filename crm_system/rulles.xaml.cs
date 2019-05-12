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
    /// Логика взаимодействия для rulles.xaml
    /// </summary>
    public partial class rulles : Window
    {
        public rulles()
        {
            InitializeComponent();
        }

        List<permision> permisions;
        public class permision
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool is_check { get; set; }
            public permision(string Id, string Name, bool Is_check)
            {
                id = Id;
                name = Name;
                is_check = Is_check;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            permisions = new List<permision>();
            MySqlConnection connection = new MySqlConnection(MainWindow.constr);
            try
            {
                bool cell_check = false;
                connection.Open();
                MySqlCommand sel_permissions = new MySqlCommand("select t.* from permissions t", connection);
                MySqlDataReader read_permissions = sel_permissions.ExecuteReader();
                while (read_permissions.Read())
                {
                    permisions.Add(new permision(read_permissions["id"].ToString(), read_permissions["name"].ToString(), cell_check));
                }
                connection.Close();
                permis_grid.ItemsSource = permisions;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void save_ruls_Click(object sender, RoutedEventArgs e)
        {
            string rulles = "";
            try
            {
                for (int i = 0; i < permis_grid.Items.Count; i++)
                {
                    permision permision = permis_grid.Items[i] as permision;
                    if (permision.is_check)
                    {
                        rulles = rulles + permision.id.ToString() + ";";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            add_rolls.rights = rulles;
            ((add_rolls)this.Owner).load_permisions();
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<permision> permision = new List<permision>();
            foreach (var r in permisions)
            {
                r.is_check = true;
                permision.Add(r);
            }
            permis_grid.ItemsSource = permision;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            List<permision> permision = new List<permision>();
            foreach (var r in permisions)
            {
                r.is_check = false;
                permision.Add(r);
            }

            permis_grid.ItemsSource = permision;
        }
    }
}
