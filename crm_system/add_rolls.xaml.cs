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
    /// Логика взаимодействия для add_rolls.xaml
    /// </summary>
    public partial class add_rolls : Window
    {
        SqlConnection connection;
        rulles rulles = new rulles();
        public add_rolls()
        {
            InitializeComponent();
        }
        public static string id_rool = null, rights = null;
        public void load_permisions()
        {
            connection = new SqlConnection(MainWindow.constr);
            connection.Open();
            string rights_name = null;
            string[] permis_array = null;
            List<permision> permisions = new List<permision>();
            SqlCommand sel_permissions = new SqlCommand("select string_agg(t.name,';') as permissions from permissions t where  ';" + rights + ";' like '%;'+ cast(t.id as varchar)+';%'", connection);
            SqlDataReader read_permissions = sel_permissions.ExecuteReader();
            if (read_permissions.Read())
            {
                rights_name = read_permissions["permissions"].ToString();
            }
            read_permissions.Close();
            permis_array = rights_name.Split(';');
            for (int i = 0; i < permis_array.Length; i++)
            {
                permisions.Add(new permision(permis_array[i]));
            }
            permis_grid.ItemsSource = permisions;
            connection.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id_rool != null)
                {
                    connection = new SqlConnection(MainWindow.constr);
                    connection.Open();
                    SqlCommand sel_rulls = new SqlCommand("select string_agg(rights,';') as rights from rols t where t.id = @id", connection);
                    sel_rulls.Parameters.AddWithValue("id", id_rool);
                    SqlDataReader read_ruls = sel_rulls.ExecuteReader();
                    if (read_ruls.Read())
                    {
                        rights = read_ruls["rights"].ToString();
                    }
                    read_ruls.Close();
                    SqlCommand sel_roll_info = new SqlCommand("select t.name from rols t where t.id = @id", connection);
                    sel_roll_info.Parameters.AddWithValue("id", id_rool);
                    SqlDataReader read_roll_info = sel_roll_info.ExecuteReader();
                    if (read_roll_info.Read())
                    {
                        roll_name.Text = read_roll_info["name"].ToString();
                    }
                    read_roll_info.Close();
                    connection.Close();
                    load_permisions();
                }
            }
            catch
            {

            }
        }

        private void add_or_upd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!rulles.IsLoaded)
                {
                    rulles = new rulles();
                    rulles.Owner = this;
                    rulles.Show();
                }
                else
                {
                    rulles.Focus();
                }
            }
            catch
            {

            }
        }
    }
}
