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
    /// Логика взаимодействия для add_rolls.xaml
    /// </summary>
    public partial class add_rolls : Window
    {
        MySqlConnection connection;
        rulles rulles = new rulles();
        CheckFields check = new CheckFields();
        List<permision> permisions;
        public add_rolls()
        {
            InitializeComponent();
        }
        public static string id_rool = null, rights = null;
        public void load_permisions()
        {
            connection = new MySqlConnection(MainWindow.constr);
            connection.Open();
            string rights_name = null;
            string[] permis_array = null;
            permisions = new List<permision>();
            MySqlCommand sel_permissions = new MySqlCommand("select REPLACE(GROUP_CONCAT(t.name,';'),',','') as permissions from permissions t where  ';" + rights + "' like CONCAT('%;',convert(t.id,char),';%')  ", connection);
            MySqlDataReader read_permissions = sel_permissions.ExecuteReader();
            if (read_permissions.Read())
            {
                rights_name = read_permissions["permissions"].ToString();
            }
            read_permissions.Close();
            rights_name = rights_name.Remove(rights_name.Length-1,1);
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
                    connection = new MySqlConnection(MainWindow.constr);
                    connection.Open();
                    MySqlCommand sel_rulls = new MySqlCommand("select REPLACE(GROUP_CONCAT(rights,';'),',','') as rights from rols t where t.id = @id", connection);
                    sel_rulls.Parameters.AddWithValue("id", id_rool);
                    MySqlDataReader read_ruls = sel_rulls.ExecuteReader();
                    if (read_ruls.Read())
                    {
                        rights = read_ruls["rights"].ToString();
                    }
                    read_ruls.Close();
                    MySqlCommand sel_roll_info = new MySqlCommand("select t.name from rols t where t.id = @id", connection);
                    sel_roll_info.Parameters.AddWithValue("id", id_rool);
                    MySqlDataReader read_roll_info = sel_roll_info.ExecuteReader();
                    if (read_roll_info.Read())
                    {
                        roll_name.Text = read_roll_info["name"].ToString();
                    }
                    read_roll_info.Close();
                    connection.Close();
                    load_permisions();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void add_or_upd_Click(object sender, RoutedEventArgs e)
        {
            string rightss = null;
            for (int i = 0; i< permis_grid.Items.Count; i++)
            {
                var col = permis_grid.Items[i] as permision;
                rightss = rightss + col.caption.ToString() + ";";
            }
            try
            {
                if (roll_name.Text != "")
                {
                    if (id_rool == null)
                    {
                        connection.Open();
                        MySqlCommand ins_in_users = new MySqlCommand("insert into rols (rights, name) values (@rights, @name)", connection);
                        ins_in_users.Parameters.AddWithValue("rights", rights);
                        ins_in_users.Parameters.AddWithValue("name", roll_name.Text);
                        ins_in_users.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        connection.Open();
                        MySqlCommand ins_in_users = new MySqlCommand("update rols set rights = @rights, name = @name where id = @id", connection);
                        ins_in_users.Parameters.AddWithValue("rights", rights);
                        ins_in_users.Parameters.AddWithValue("name", roll_name.Text);
                        ins_in_users.Parameters.AddWithValue("id", id_rool);
                        ins_in_users.ExecuteNonQuery();
                        connection.Close();
                    }
                    ((MainWindow)this.Owner).refresh();
                    Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            permisions.Remove(permis_grid.SelectedItem as permision);
            permis_grid.Items.Refresh();
            permis_grid.ItemsSource = permisions;
        }

        private void roll_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            roll_name.BorderBrush = Brushes.Black;
            check.CheckFieldsCaption(roll_name);
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
