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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using crm_system.DB;

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        addOrgn addOrgn = new addOrgn();
        add_sotr add_Sotr = new add_sotr();
        add_call add_Call = new add_call();
        add_user add_User = new add_user();
        add_rolls add_rolles = new add_rolls();
        auntif auntt = new auntif();
        public static bool auntif = false;
        public static string rol_id = null;
        int roll_grid_id = 0;
        //для фильтра по оргранизациям
        string org_id = null;
        public static string constr = @"Data Source=DESKTOP-BEHL3UV\SQLEXPRESS;Initial Catalog=crmSystem;Integrated Security=True;MultipleActiveResultSets=True";
        public MainWindow()
        {
            InitializeComponent();
            no_visible();
            stat_filt.Items.Add("Назначен");
            stat_filt.Items.Add("Закончен");
            connection = new SqlConnection(constr);
        }

        public void no_visible()
        {
            Thickness no_margin = new Thickness(0, 0, 0, 0);

            cals.Visibility = Visibility.Hidden;
            cals.Margin = no_margin;
            cals.Height = 0;

            users.Visibility = Visibility.Hidden;
            users.Margin = no_margin;
            users.Height = 0;

            rols.Visibility = Visibility.Hidden;
            rols.Margin = no_margin;
            rols.Height = 0;

            handbooks.Visibility = Visibility.Hidden;
            handbooks.Margin = no_margin;
            handbooks.Height = 0;

            sotrs.Visibility = Visibility.Hidden;
            sotrs.Margin = no_margin;
            sotrs.Height = 0;

            //

            del__org.Visibility = Visibility.Hidden;
            del__org.Height = 0;
            add__org.Visibility = Visibility.Hidden;
            add__org.Height = 0;
            upd__org.Visibility = Visibility.Hidden;
            upd__org.Height = 0;
            add__sotr.Visibility = Visibility.Hidden;
            add__sotr.Height = 0;
            view__sotr.Visibility = Visibility.Hidden;
            view__sotr.Height = 0;
            add__call.Visibility = Visibility.Hidden;
            add__call.Height = 0;
            org_grid_popup.Visibility = Visibility.Hidden;
            //

            re_aunt.Visibility = Visibility.Hidden;
            re_aunt.Height = 0;
            exit.Visibility = Visibility.Hidden;
            exit.Height = 0;

        }

        public void aunt_result()
        {
            string[] ruls = null;
            no_visible();
            if (auntif)
            {
                org_grid_popup.Visibility = Visibility.Visible;
                connection.Open();
                SqlCommand sel_rights = new SqlCommand("select rights, name from rols where id = @id", connection);
                sel_rights.Parameters.AddWithValue("id", rol_id);
                SqlDataReader read_rights = sel_rights.ExecuteReader();
                if (read_rights.Read())
                {
                    org_grid_popup.Visibility = Visibility.Visible;
                    ruls = read_rights["rights"].ToString().Split(';');
                    for (int i = 0; i < ruls.Length; i++)
                    {
                        Thickness defalut_margin = new Thickness(0, 0, 10, 0);
                        switch (ruls[i])
                        {
                            case "1":
                                //Добавление организаций
                                add__org.Visibility = Visibility.Visible;
                                add__org.Height = 20;
                                break;
                            case "2":
                                //Редактирование организаций
                                upd__org.Visibility = Visibility.Visible;
                                upd__org.Height = 20;
                                break;
                            case "3":
                                //Удаление организаций
                                del__org.Visibility = Visibility.Visible;
                                del__org.Height = 20;
                                break;
                            case "4":
                                //Назначение звонков
                                add__call.Visibility = Visibility.Visible;
                                add__call.Height = 20;
                                break;
                            case "5":
                                //Просмотр Звонков
                                cals.Visibility = Visibility.Visible;
                                cals.Height = 40;
                                break;
                            case "6":
                                //Добавление сотрудников организаций
                                add__sotr.Visibility = Visibility.Visible;
                                add__sotr.Height = 20;
                                break;
                            case "7":
                                //Просмотр сотрудников организаций
                                sotrs.Visibility = Visibility.Visible;
                                sotrs.Height = 40;
                                break;
                            case "8":
                                //Пользователи
                                users.Visibility = Visibility.Visible;
                                users.Height = 40;
                                break;
                            case "9":
                                //Роли
                                rols.Visibility = Visibility.Visible;
                                rols.Height = 40;
                                break;
                            case "10":
                                //Справочники
                                handbooks.Visibility = Visibility.Visible;
                                handbooks.Height = 40;
                                break;
                        }
                    }
                }
                connection.Close();
            }
            else
            {

            }
        }

        

        public void refresh()
        {
            try
            {
                //orgs
                List<org> orgs = new List<org>();
                connection = new SqlConnection(constr);
                connection.Open();
                SqlCommand command = new SqlCommand("select id, code, name, (select name from cities where id = city) as city, phone, (case status when 0 then 'Добавлен'  when 1 then 'Назначен звонок' when 2 then 'Перезвон' end) as status, (select CONCAT(surname,' ',name) from users where id = kurator) as kurator, (case priority when 0 then 'Низкий' when 1 then 'Средний' when 2 then 'Высокий' end) as priority from org", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orgs.Add(new org(reader["id"].ToString(), reader["code"].ToString(), reader["name"].ToString(), reader["city"].ToString(), reader["status"].ToString(), reader["kurator"].ToString(), reader["phone"].ToString(), reader["priority"].ToString()));
                }
                org_grid.ItemsSource = orgs;
                //calls
                List<calls> callses = new List<calls>();
                if (org_id != null)
                {
                    SqlCommand sel_calls = new SqlCommand("select t.id, t.date_cal, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call from calls t where t.id_org = @org_id", connection);
                    sel_calls.Parameters.AddWithValue("org_id", org_id);
                    SqlDataReader reader_calls = sel_calls.ExecuteReader();
                    while (reader_calls.Read())
                    {
                        callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString()));
                    }
                }
                else
                {
                    SqlCommand sel_calls = new SqlCommand("select t.id, t.date_cal, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call from calls t", connection);
                    SqlDataReader reader_calls = sel_calls.ExecuteReader();
                    while (reader_calls.Read())
                    {
                        callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString()));
                    }
                }

                calls_grid.ItemsSource = callses;
                //users
                List<user> users = new List<user>();
                SqlCommand sel_users = new SqlCommand("select t.id, t.login, t.password, (select tt.name from rols tt where tt.id = t.rol) as roll from users t", connection);
                SqlDataReader read_users = sel_users.ExecuteReader();
                while (read_users.Read())
                {
                    users.Add(new user(read_users["id"].ToString(), read_users["login"].ToString(), read_users["password"].ToString(), read_users["roll"].ToString()));
                }
                user_grid.ItemsSource = users;
                //rols
                List<roll> rolls = new List<roll>();
                SqlCommand sel_ruls = new SqlCommand("select t.id, t.name from rols t", connection);
                SqlDataReader read_ruls = sel_ruls.ExecuteReader();
                while (read_ruls.Read())
                {
                    rolls.Add(new roll(read_ruls["id"].ToString(), read_ruls["name"].ToString()));
                }
                roll_grid.ItemsSource = rolls;
                //handbooks
                List<grid_items> jobs = new List<grid_items>();
                List<grid_items> cities = new List<grid_items>();
                List<grid_items> rulls = new List<grid_items>();
                //Должности
                try
                {
                    SqlCommand sel_jobs = new SqlCommand("select t.* from posts t", connection);
                    SqlDataReader read_jobs = sel_jobs.ExecuteReader();
                    while (read_jobs.Read())
                    {
                        jobs.Add(new grid_items(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                    }
                    read_jobs.Close();
                    post_grid.ItemsSource = jobs;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении должностей!");
                    connection.Close();
                }

                //Города
                try
                {
                    SqlCommand sel_cities = new SqlCommand("select t.* from cities t", connection);
                    SqlDataReader cities_jobs = sel_cities.ExecuteReader();
                    while (cities_jobs.Read())
                    {
                        cities.Add(new grid_items(cities_jobs["id"].ToString(), cities_jobs["name"].ToString()));
                    }
                    cities_jobs.Close();
                    cities_grid.ItemsSource = cities;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении Городов!");
                    connection.Close();
                }

                //"Права ролей"
                try
                {
                    SqlCommand sel_rols = new SqlCommand("select t.* from permissions t", connection);
                    SqlDataReader read_rols = sel_rols.ExecuteReader();
                    while (read_rols.Read())
                    {
                        rulls.Add(new grid_items(read_rols["id"].ToString(), read_rols["name"].ToString()));
                    }
                    rol_rulls_grid.ItemsSource = rulls;
                    read_rols.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении прав ролей!");
                    connection.Close();
                }
                //sotrs
                string query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers";
                List<worker> workers = new List<worker>();
                if (org_id != null)
                {
                    query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers where id_org=" + org_id;
                }
                SqlCommand sel_sotrs = new SqlCommand(query, connection);
                SqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
                while (read_sotrs.Read())
                {
                    workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
                }
                sotr_grid.ItemsSource = workers;
                //
                //
                //
                List<comboItems> Orgs = new List<comboItems>();
                List<comboItems> Opers = new List<comboItems>();

                SqlCommand sel_orgs = new SqlCommand("select id, name from org order by name ", connection);
                SqlDataReader orgs_read = sel_orgs.ExecuteReader();
                while (orgs_read.Read())
                {
                    Orgs.Add(new comboItems(orgs_read["id"].ToString(), orgs_read["name"].ToString()));
                }
                orgs_read.Close();
                SqlCommand sel_opers = new SqlCommand("select id, name, surname, second_name from users", connection);
                SqlDataReader opers_read = sel_opers.ExecuteReader();
                while (opers_read.Read())
                {
                    Opers.Add(new comboItems(opers_read["id"].ToString(), opers_read["surname"].ToString() + " " + opers_read["Name"].ToString() + " " + opers_read["second_name"].ToString()));
                }
                opers_read.Close();


                org_filt.ItemsSource = Orgs;
                org_filt_.ItemsSource = Orgs;
                oper_filt.ItemsSource = Opers;
                job_filt.ItemsSource = jobs;
                prioryty_org_filt.Items.Add("Низский");
                prioryty_org_filt.Items.Add("Средний");
                prioryty_org_filt.Items.Add("Высокий");
                //
                org_status_filt.Items.Add("Добавлен");
                org_status_filt.Items.Add("Назначен звонок");
                org_status_filt.Items.Add("Перезвон");
                List<comboItems> comboItem = new List<comboItems>();
                List<comboItems> comboItem_kur = new List<comboItems>();
                SqlCommand citys = new SqlCommand("select name,id from cities", connection);
                SqlDataReader read_citys = citys.ExecuteReader();
                while (read_citys.Read())
                {
                    comboItem.Add(new comboItems(read_citys["id"].ToString(), read_citys["name"].ToString()));
                }
                city_org_filt.ItemsSource = comboItem;
                read_citys.Close();

                SqlCommand kurator = new SqlCommand("select id, name, surname, second_name from users", connection);
                SqlDataReader kurator_read = kurator.ExecuteReader();
                while (kurator_read.Read())
                {
                    comboItem_kur.Add(new comboItems(kurator_read["id"].ToString(), kurator_read["surname"].ToString() + " " + kurator_read["Name"].ToString() + " " + kurator_read["second_name"].ToString()));
                }
                kurator_org_filt.ItemsSource = comboItem_kur;
                kurator_read.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void del__org_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                org table = org_grid.SelectedItem as org;
                int result = (int)MessageBox.Show("Удалить организацию " + table.Name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                switch (result)
                {
                    case (int)MessageBoxResult.Yes:
                        connection.Open();
                        SqlCommand command = new SqlCommand("delete from org where id=@id", connection);
                        command.Parameters.AddWithValue("id", table.Id);
                        command.ExecuteNonQuery();
                        refresh();
                        connection.Close();
                        break;
                }
            }
            catch
            {

            }
        }

        private void add__org_Click(object sender, RoutedEventArgs e)
        {
            if (!addOrgn.IsLoaded)
            {
                addOrgn = new addOrgn();
                addOrgn.Owner = this;
                addOrgn.Show();
            }
            else
            {
                addOrgn.Focus();
            }
        }

        private void upd__org_Click(object sender, RoutedEventArgs e)
        {
            if (!addOrgn.IsLoaded)
            {
                try
                {
                    org table = org_grid.SelectedItem as org;
                    addOrgn.id = table.Id;
                    addOrgn = new addOrgn();
                    addOrgn.Owner = this;
                    addOrgn.Show();
                }
                catch
                {

                }
            }
            else
            {
                addOrgn.Focus();
            }
        }

        private void add__sotr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!add_Sotr.IsLoaded)
                {
                    org table = org_grid.SelectedItem as org;
                    add_sotr.id_org = table.Id;
                    add_Sotr = new add_sotr();
                    add_Sotr.Show();
                }
                else
                {
                    add_Sotr.Focus();
                }
            }
            catch
            {

            }
        }

        private void view__sotr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add__call_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Call.IsLoaded)
            {
                org table = org_grid.SelectedItem as org;
                add_Call = new add_call();
                try
                {
                    add_call.id_org = table.Id;
                    add_call.id_call = null;
                }
                catch
                {

                }
                add_Call.Show();
            }
            else
            {
                add_Call.Focus();
            }
        }
        private void del_call__Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                calls calls = calls_grid.SelectedValue as calls;
                SqlCommand command = new SqlCommand("delete from calls where id = @id", connection);
                command.Parameters.AddWithValue("id", calls.id);
                command.ExecuteNonQuery();
                connection.Close();
                refresh();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void add_call__Click(object sender, RoutedEventArgs e)
        {
            if (!add_Call.IsLoaded)
            {
                add_Call = new add_call();
                add_call.id_call = null;
                add_Call.Show();
            }
            else
            {
                add_Call.Focus();
            }
        }

        private void upd_call__Click(object sender, RoutedEventArgs e)
        {
            if (!add_Call.IsLoaded)
            {
                calls table = calls_grid.SelectedItem as calls;
                add_Call = new add_call();
                try
                {
                    add_call.id_call = table.id;
                }
                catch
                {

                }
                add_Call.Show();
            }
            else
            {
                add_Call.Focus();
            }
        }

        private void del_us_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_us_Click(object sender, RoutedEventArgs e)
        {
            if (!add_User.IsLoaded)
            {
                add_User = new add_user();
                add_User.Owner = this;
                add_User.Show();
            }
            else
            {
                add_User.Focus();
            }
        }

        private void upd_us_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_roll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_roll_Click(object sender, RoutedEventArgs e)
        {
            if (!add_rolles.IsLoaded)
            {
                add_rolles = new add_rolls();
                add_rolls.id_rool = null;
                add_rolles.Owner = this;
                add_rolles.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void upd_roll_Click(object sender, RoutedEventArgs e)
        {
            if (!add_rolles.IsLoaded)
            {
                roll table = roll_grid.SelectedItem as roll;
                add_rolls.id_rool = table.id;
                add_rolles = new add_rolls();
                add_rolles.Owner = this;
                add_rolles.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void rulls_rolls_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_post_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upd_post_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_post_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_citi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upd_citi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_cities_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upd_rull_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_st_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                worker table = sotr_grid.SelectedItem as worker;
                int result = (int)MessageBox.Show("Удалить сотрудника " + table.Name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                switch (result)
                {
                    case (int)MessageBoxResult.Yes:
                        connection.Open();
                        SqlCommand command = new SqlCommand("delete from workers where id=@id", connection);
                        command.Parameters.AddWithValue("id", table.id);
                        command.ExecuteNonQuery();
                        refresh();
                        connection.Close();
                        break;
                }
            }
            catch
            {
                connection.Close();
            }
        }

        private void add_st_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Sotr.IsLoaded)
            {
                add_Sotr = new add_sotr();
                add_Sotr.Show();
                add_Sotr.Owner = this;
            }
            else
            {
                add_Sotr.Focus();
            }
        }

        private void aunt_Click(object sender, RoutedEventArgs e)
        {
            if (!auntt.IsLoaded)
            {
                auntt = new auntif();
                auntt.Owner = this;
                auntt.Show();
            }
            else
            {
                auntt.Focus();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            auntif = false;
            no_visible();
        }

        public void sel_change()
        {
            try
            {
                string query = "select t.id, t.date_cal, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call from calls t";
                string filt = "";
                if (org_filt.SelectedValue != null)
                {
                    filt = filt + " and id_org = " + org_filt.SelectedValue;
                }
                if (dat_filt.Text != null && dat_filt.Text != "")
                {
                    filt = filt + " and date_cal like '%" + dat_filt.Text + "%'";
                }
                if (stat_filt.SelectedValue != null)
                {
                    filt = filt + " and status_call = " + stat_filt.SelectedValue;
                }
                if (oper_filt.SelectedValue != null)
                {
                    filt = filt + " and id_oper = " + oper_filt.SelectedValue;
                }
                filt = filt.Remove(1, 3);
                query = query + " where " + filt;
                connection.Open();
                List<calls> callses = new List<calls>();
                SqlCommand sel_calls = new SqlCommand(query, connection);
                SqlDataReader reader_calls = sel_calls.ExecuteReader();
                while (reader_calls.Read())
                {
                    callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString()));
                }
                calls_grid.ItemsSource = callses;
                connection.Close();
            }
            catch
            {

            }
        }

        private void org_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change();
        }

        private void dat_filt_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            sel_change();
        }

        private void stat_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change();
        }

        private void oper_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            org_filt.Text = "";
            dat_filt.Text = "";
            stat_filt.Text = "";
            oper_filt.Text = "";
            string query = "select t.id, t.date_cal, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call from calls t";
            connection.Open();
            List<calls> callses = new List<calls>();
            SqlCommand sel_calls = new SqlCommand(query, connection);
            SqlDataReader reader_calls = sel_calls.ExecuteReader();
            while (reader_calls.Read())
            {
                callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString()));
            }
            calls_grid.ItemsSource = callses;
            connection.Close();
        }
        public void sel_change_org()
        {
            try
            {
                List<org> orgs = new List<org>();
                string query = "select id, code, name, (select name from cities where id = city) as city, phone, (case status when 0 then 'Добавлен'  when 1 then 'Назначен звонок' when 2 then 'Перезвон' end) as status, (select CONCAT(surname,' ',name) from users where id = kurator) as kurator, (case priority when 0 then 'Низкий' when 1 then 'Средний' when 2 then 'Высокий' end) as priority from org";
                string filt = "";
                if (org_name_filt.Text != null)
                {
                    filt = filt + " and name like '%" + org_name_filt.Text + "%'";
                }
                if (city_org_filt.SelectedValue != null)
                {
                    filt = filt + " and city = " + city_org_filt.SelectedValue;
                }
                if (prioryty_org_filt.SelectedValue != null)
                {
                    filt = filt + " and priority ='" + prioryty_org_filt.SelectedIndex;
                }
                if (org_status_filt.SelectedValue != null)
                {
                    filt = filt + " and status = " + org_status_filt.SelectedIndex;
                }
                if (kurator_org_filt.SelectedValue != null)
                {
                    filt = filt + " and kurator = " + kurator_org_filt.SelectedValue;
                }
                if (phone_org_filt.Text != null)
                {
                    filt = filt + " and phone like '%" + phone_org_filt.Text + "%'";
                }
                filt = filt.Remove(1, 3);
                query = query + " where " + filt;
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orgs.Add(new org(reader["id"].ToString(), reader["code"].ToString(), reader["name"].ToString(), reader["city"].ToString(), reader["status"].ToString(), reader["kurator"].ToString(), reader["phone"].ToString(), reader["priority"].ToString()));
                }
                org_grid.ItemsSource = orgs;
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void sel_change_sot()
        {
            try
            {
                List<worker> workers = new List<worker>();
                string query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers";
                string filt = "";
                if (org_filt_.SelectedValue != null)
                {
                    filt = filt + " and id_org = " + org_filt_.SelectedValue;
                }
                if (job_filt.SelectedValue != null)
                {
                    filt = filt + " and id_post =" + job_filt.SelectedValue;
                }
                if (otch_filt.Text != null && otch_filt.Text != "")
                {
                    filt = filt + " and second_name like '%" + otch_filt.Text + "%'";
                }
                if (name_filt.Text != null && name_filt.Text != "")
                {
                    filt = filt + " and surname like '%" + name_filt.Text + "%'";
                }
                if (fam_filt.Text != null && fam_filt.Text != "")
                {
                    filt = filt + " and name like '%" + fam_filt.Text + "%'";
                }
                filt = filt.Remove(1, 3);
                query = query + " where " + filt;
                connection.Open();

                SqlCommand sel_sotrs = new SqlCommand(query, connection);
                SqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
                while (read_sotrs.Read())
                {
                    workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
                }
                sotr_grid.ItemsSource = workers;
                connection.Close();
            }
            catch
            {

            }
        }

        private void name_filt_TextChanged(object sender, TextChangedEventArgs e)
        {
            new CheckFields().CheckFieldsCaption(name_filt);
            sel_change_sot();
        }

        private void fam_filt_TextChanged(object sender, TextChangedEventArgs e)
        {
            new CheckFields().CheckFieldsCaption(fam_filt);
            sel_change_sot();
        }

        private void otch_filt_TextChanged(object sender, TextChangedEventArgs e)
        {
            new CheckFields().CheckFieldsCaption(otch_filt);
            sel_change_sot();
        }

        private void org_filt__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_sot();
        }

        private void job_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_sot();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            name_filt.Text = "";
            fam_filt.Text = "";
            otch_filt.Text = "";
            org_filt_.Text = "";
            job_filt.Text = "";
            connection.Open();
            string query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers";
            List<worker> workers = new List<worker>();
            if (org_id != null)
            {
                query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers where id_org=" + org_id;
            }
            SqlCommand sel_sotrs = new SqlCommand(query, connection);
            SqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
            while (read_sotrs.Read())
            {
                workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
            }
            sotr_grid.ItemsSource = workers;
            connection.Close();

        }

        private void city_org_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_org();
        }

        private void prioryty_org_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_org();
        }

        private void org_status_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_org();
        }

        private void kurator_org_filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change_org();
        }

        private void phone_org_filt_TextChanged(object sender, TextChangedEventArgs e)
        {
            new CheckFields().CheckFieldsCaption(phone_org_filt, "number");
            sel_change_org();
        }

        private void roll_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<permision> permisions = new List<permision>();
            var roll = roll_grid.SelectedValue as roll;
            connection.Open();
            SqlCommand command = new SqlCommand("select t.rights from rols t where t.id = @id_rol", connection);
            command.Parameters.AddWithValue("id_rol", roll.id);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string[] permis = reader["rights"].ToString().Split(';');
                for(int i = 0; i< permis.Length; i++)
                {
                    SqlCommand sel_permis = new SqlCommand("select t.name from permissions t where t.id = @id_per", connection);
                    sel_permis.Parameters.AddWithValue("id_per", permis[i]);
                    SqlDataReader read_permis = sel_permis.ExecuteReader();
                    while (read_permis.Read())
                    {
                        permisions.Add(new permision(read_permis["name"].ToString()));
                    }
                }
            }
            permis_grid.ItemsSource = permisions;
            connection.Close();
        }

        private void upd_st_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Sotr.IsLoaded)
            {
                var table = sotr_grid.SelectedValue as worker;
                add_sotr.id_sotr = table.id;
                add_Sotr = new add_sotr();
                add_Sotr.Show();
                add_Sotr.Owner = this;
            }
            else
            {
                add_Sotr.Focus();
            }
        }

        private void org_name_filt_TextChanged(object sender, TextChangedEventArgs e)
        {
            new CheckFields().CheckFieldsCaption(org_name_filt);
            sel_change_org();
        }
    }
}
