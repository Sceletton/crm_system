using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data;
using System.Text.RegularExpressions;
using crm_system.DB;
using Microsoft.Office.Interop.Excel;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;
using CheckBox = System.Windows.Controls.CheckBox;
using LiveCharts;
using LiveCharts.Configurations;

namespace crm_system
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        MySqlConnection connection;
        addOrgn addOrgn = new addOrgn();
        add_sotr add_Sotr = new add_sotr();
        add_call add_Call = new add_call();
        add_user add_User = new add_user();
        add_rolls add_rolles = new add_rolls();
        auntif auntt = new auntif();
        a_or_u_hanboxes hanboxes_cities = new a_or_u_hanboxes();
        a_or_u_hanboxes hanboxes_posts = new a_or_u_hanboxes();
        public static bool auntif = false;
        public static string rol_id = null;
        public static int user_id = -1;
        //для фильтра по оргранизациям
        string org_id = null;
        public static string constr = null;
        public MainWindow()
        {
            InitializeComponent();
            no_visible();
            try
            {
                var sr = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory()), "conf.txt",SearchOption.AllDirectories);
                string[] settings = File.ReadAllText(sr[0]).Split(';');
                string server = settings[Find(settings, "SERVER")], db = settings[Find(settings, "DATABASE")], uid = settings[Find(settings, "UID")], pwd = settings[Find(settings, "Pwd")];
                constr = server + ";" + db + ";" + uid + ";" + pwd + ";";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Крнфигурационный файл не найден");
            }
            connection = new MySqlConnection(constr);
        }

       
        public int Find(string[] ar, string word)
        {
            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i].ToLower().Split('=')[0].Trim() == word.ToLower())
                {
                    return i;
                }
            }
            return -1;
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
            settings.Visibility = Visibility.Hidden;
            settings.Margin = no_margin;
            settings.Height = 0;
            //
            analityc.Visibility = Visibility.Hidden;
            analityc.Margin = no_margin;
            analityc.Height = 0;
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
                MySqlCommand sel_rights = new MySqlCommand("select rights, name from rols where id = @id", connection);
                sel_rights.Parameters.AddWithValue("id", rol_id);
                MySqlDataReader read_rights = sel_rights.ExecuteReader();
                if (read_rights.Read())
                {
                    roll_name.Text = "Вы вошли под  ролью "+read_rights["name"];
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
                            case "11":
                                //Аналитика
                                analityc.Visibility = Visibility.Visible;
                                analityc.Height = 40;
                                break;
                            case "12":
                                //Настройка
                                settings.Visibility = Visibility.Visible;
                                settings.Height = 40;
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

        

        public void refresh(string tab = null)
        {
            connection.Open();
            try
            {
                if (tab == "orgs" || tab == null)
                {
                    //orgs
                    List<org> orgs = new List<org>();
                    MySqlCommand command = new MySqlCommand("select id, code, name, (select name from cities where id = city) as city, phone, (case status when 0 then 'Добавлен'  when 1 then 'Назначен звонок' when 2 then 'Перезвон' end) as status, (select CONCAT(surname,' ',name) from users where id = kurator) as kurator, (case priority when 0 then 'Низкий' when 1 then 'Средний' when 2 then 'Высокий' end) as priority from org", connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        orgs.Add(new org(reader["id"].ToString(), reader["code"].ToString(), reader["name"].ToString(), reader["city"].ToString(), reader["status"].ToString(), reader["kurator"].ToString(), reader["phone"].ToString(), reader["priority"].ToString()));
                    }
                    org_grid.ItemsSource = orgs;
                    reader.Close();
                }
                if (tab == "calls" || tab == null)
                {
                    //calls
                    List<calls> callses = new List<calls>();
                    if (org_id != null)
                    {
                        MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name  from calls t" +
                            "join users tt on tt.id = t.id_oper " +
                            "where t.id_org = @org_id", connection);
                        sel_calls.Parameters.AddWithValue("org_id", org_id);
                        MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                        while (reader_calls.Read())
                        {
                            callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString()));
                        }
                        reader_calls.Close();
                    }
                    else
                    {
                        MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call, tt.name, tt.surname, tt.second_name from calls t " +
                            "join users tt on tt.id = t.id_oper", connection);
                        MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                        while (reader_calls.Read())
                        {
                            callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString()));
                        }
                        reader_calls.Close();
                    }
                    calls_grid.ItemsSource = callses;
                }
                if (tab == "users" || tab == null)
                {
                    //users
                    List<user> users = new List<user>();
                    MySqlCommand sel_users = new MySqlCommand("select t.id, t.login, t.password, (select tt.name from rols tt where tt.id = t.rol) as roll from users t", connection);
                    MySqlDataReader read_users = sel_users.ExecuteReader();
                    while (read_users.Read())
                    {
                        users.Add(new user(read_users["id"].ToString(), read_users["login"].ToString(), read_users["password"].ToString(), read_users["roll"].ToString()));
                    }
                    read_users.Close();
                    user_grid.ItemsSource = users;
                }
                if (tab == "rols" || tab == null)
                {
                    //rols
                    List<roll> rolls = new List<roll>();
                    MySqlCommand sel_ruls = new MySqlCommand("select t.id, t.name from rols t", connection);
                    MySqlDataReader read_ruls = sel_ruls.ExecuteReader();
                    while (read_ruls.Read())
                    {
                        rolls.Add(new roll(read_ruls["id"].ToString(), read_ruls["name"].ToString()));
                    }
                    read_ruls.Close();
                    roll_grid.ItemsSource = rolls;
                }
                //handbooks
                if (tab == "handbooks" || tab == null)
                {
                    List<grid_items> jobs = new List<grid_items>();
                    List<grid_items> cities = new List<grid_items>();
                    List<grid_items> rulls = new List<grid_items>();
                    //Должности
                    try
                    {
                        MySqlCommand sel_jobs = new MySqlCommand("select t.* from posts t", connection);
                        MySqlDataReader read_jobs = sel_jobs.ExecuteReader();
                        while (read_jobs.Read())
                        {
                            jobs.Add(new grid_items(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                        }
                        read_jobs.Close();
                        post_grid.ItemsSource = jobs;
                    }
                    catch (MySqlException sqlEx)
                    {
                        MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении должностей!");
                        connection.Close();
                    }

                    //Города
                    try
                    {
                        MySqlCommand sel_cities = new MySqlCommand("select t.* from cities t", connection);
                        MySqlDataReader cities_jobs = sel_cities.ExecuteReader();
                        while (cities_jobs.Read())
                        {
                            cities.Add(new grid_items(cities_jobs["id"].ToString(), cities_jobs["name"].ToString()));
                        }
                        cities_jobs.Close();
                        cities_grid.ItemsSource = cities;
                    }
                    catch (MySqlException sqlEx)
                    {
                        MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении Городов!");
                        connection.Close();
                    }

                    //"Права ролей"
                    try
                    {
                        MySqlCommand sel_rols = new MySqlCommand("select t.* from permissions t", connection);
                        MySqlDataReader read_rols = sel_rols.ExecuteReader();
                        while (read_rols.Read())
                        {
                            rulls.Add(new grid_items(read_rols["id"].ToString(), read_rols["name"].ToString()));
                        }
                        rol_rulls_grid.ItemsSource = rulls;
                        read_rols.Close();
                    }
                    catch (MySqlException sqlEx)
                    {
                        MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении прав ролей!");
                        connection.Close();
                    }
                }
                if (tab == "emps" || tab == null)
                {
                    //sotrs
                    string query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers t ";
                    List<worker> workers = new List<worker>();
                    if (org_id != null)
                    {
                        query = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post(select t1.name from posts t1 where t1.id = id_post) as post from workers t  where id_org=" + org_id;
                    }
                    MySqlCommand sel_sotrs = new MySqlCommand(query, connection);
                    MySqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
                    while (read_sotrs.Read())
                    {
                        workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
                    }
                    read_sotrs.Close();
                    sotr_grid.ItemsSource = workers;
                }
                //
                //
                //
                List<comboItems> Orgs = new List<comboItems>();
                List<comboItems> Opers = new List<comboItems>();
                List<grid_items> jobes = new List<grid_items>();
                MySqlCommand sel_orgs = new MySqlCommand("select id, name from org order by name ", connection);
                MySqlDataReader orgs_read = sel_orgs.ExecuteReader();
                while (orgs_read.Read())
                {
                    Orgs.Add(new comboItems(orgs_read["id"].ToString(), orgs_read["name"].ToString()));
                }
                orgs_read.Close();
                MySqlCommand sel_opers = new MySqlCommand("select id, name, surname, second_name from users", connection);
                MySqlDataReader opers_read = sel_opers.ExecuteReader();
                while (opers_read.Read())
                {
                    Opers.Add(new comboItems(opers_read["id"].ToString(), opers_read["surname"].ToString() + " " + opers_read["Name"].ToString() + " " + opers_read["second_name"].ToString()));
                }
                emploers.ItemsSource = Opers;
                opers_read.Close();
                try
                {
                    MySqlCommand sel_jobs = new MySqlCommand("select t.* from posts t", connection);
                    MySqlDataReader read_jobs = sel_jobs.ExecuteReader();
                    while (read_jobs.Read())
                    {
                        jobes.Add(new grid_items(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                    }
                    read_jobs.Close();
                    post_grid.ItemsSource = jobes;
                }
                catch (MySqlException sqlEx)
                {
                    connection.Close();
                    MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении должностей!");
                }
                org_filt.ItemsSource = Orgs;
                org_filt_.ItemsSource = Orgs;
                oper_filt.ItemsSource = Opers;
                job_filt.ItemsSource = jobes;
                org.ItemsSource = Orgs;

                stat_filt.Items.Clear();
                stat_filt.Items.Add("Назначен");
                stat_filt.Items.Add("Закончен");

                prioryty_org_filt.Items.Clear();
                prioryty_org_filt.Items.Add("Низский");
                prioryty_org_filt.Items.Add("Средний");
                prioryty_org_filt.Items.Add("Высокий");

                org_status_filt.Items.Clear();
                org_status_filt.Items.Add("Добавлен");
                org_status_filt.Items.Add("Назначен звонок");
                org_status_filt.Items.Add("Перезвон");
                List<comboItems> comboItem = new List<comboItems>();
                List<comboItems> comboItem_kur = new List<comboItems>();
                MySqlCommand citys = new MySqlCommand("select name,id from cities", connection);
                MySqlDataReader read_citys = citys.ExecuteReader();
                while (read_citys.Read())
                {
                    comboItem.Add(new comboItems(read_citys["id"].ToString(), read_citys["name"].ToString()));
                }
                city_org_filt.ItemsSource = comboItem;
                read_citys.Close();

                MySqlCommand kurator = new MySqlCommand("select id, name, surname, second_name from users", connection);
                MySqlDataReader kurator_read = kurator.ExecuteReader();
                while (kurator_read.Read())
                {
                    comboItem_kur.Add(new comboItems(kurator_read["id"].ToString(), kurator_read["surname"].ToString() + " " + kurator_read["Name"].ToString() + " " + kurator_read["second_name"].ToString()));
                }
                kurator_org_filt.ItemsSource = comboItem_kur;
                kurator_read.Close();
                if (tab == "settings" || tab == null)
                {
                    //настройки
                    MySqlCommand settings = new MySqlCommand("select t.* from settings t where t.id_user = @id_user", connection);
                    settings.Parameters.AddWithValue("id_user", user_id);
                    MySqlDataReader set_reader = settings.ExecuteReader();
                    if (set_reader.Read())
                    {
                        dir_path.Text = set_reader["save_path"].ToString();
                        setChecBoxValue(orgs_fiter, int.Parse(set_reader["orgs_search"].ToString()));
                        setChecBoxValue(calls_fiter, int.Parse(set_reader["emps_search"].ToString()));
                        setChecBoxValue(emps_fiter, int.Parse(set_reader["call_search"].ToString()));
                        setFiltersVisible(organiz_filter, int.Parse(set_reader["orgs_search"].ToString()));
                        setFiltersVisible(emps_filter, int.Parse(set_reader["emps_search"].ToString()));
                        setFiltersVisible(calls_filter, int.Parse(set_reader["call_search"].ToString()));

                    }
                    set_reader.Close();
                }

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
                        MySqlCommand command = new MySqlCommand("delete from org where id=@id", connection);
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
                    addOrgn.id = table.Id.ToString();
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
                    add_sotr.id_org = table.Id.ToString();
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
                    add_call.id_org = table.Id.ToString();
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

        public void dell_call(int status)
        {
            try
            {
                connection.Open();
                calls calls = calls_grid.SelectedValue as calls;
                MySqlCommand command = new MySqlCommand("delete from calls where id = @id", connection);
                command.Parameters.AddWithValue("id", calls.id);
                command.ExecuteNonQuery();
                MySqlCommand ancalytic = new MySqlCommand("insert into calls_analytics  (id_org, id_oper, call_status) values (@id_org, @user_id, @status)", connection);
                ancalytic.Parameters.AddWithValue("id_org", calls.id_org);
                ancalytic.Parameters.AddWithValue("status", status);
                ancalytic.Parameters.AddWithValue("user_id", MainWindow.user_id);
                connection.Close();
                connection.Close();
                refresh();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void del_call__Click(object sender, RoutedEventArgs e)
        {
            dell_call(2);
        }

        private void add_call__Click(object sender, RoutedEventArgs e)
        {
            if (!add_Call.IsLoaded)
            {
                add_Call = new add_call();
                add_call.id_call = null;
                add_Call.Owner = this;
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
                    add_call.id_call = table.id.ToString();
                }
                catch
                {

                }
                add_Call.Owner = this;
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
            if (!add_User.IsLoaded)
            {
                add_User = new add_user();
                add_User.Owner = this;
                add_user.id_user = (user_grid.SelectedItem as user).id.ToString();
                add_User.Show();
            }
            else
            {
                add_User.Focus();
            }
        }

        private void del_roll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                MySqlCommand del_roll = new MySqlCommand("delete from rols where id = @rol_id", connection);
                del_roll.Parameters.AddWithValue("rol_id", (roll_grid.SelectedItem as roll).id);
                del_roll.ExecuteNonQuery();
                connection.Close();
                refresh("rols");
                permis_grid.ItemsSource= "";
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
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
                add_rolls.id_rool = table.id.ToString();
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
            if (!hanboxes_posts.IsLoaded)
            {
                var table = post_grid.SelectedItem as grid_items;
                a_or_u_hanboxes.type = "posts";
                a_or_u_hanboxes.hanbox_id = -1;
                hanboxes_posts = new a_or_u_hanboxes();
                hanboxes_posts.Owner = this;
                hanboxes_posts.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void upd_post_Click(object sender, RoutedEventArgs e)
        {
            if (!hanboxes_posts.IsLoaded)
            {
                var table = post_grid.SelectedItem as grid_items;
                a_or_u_hanboxes.hanbox_id = int.Parse(table.id.ToString());
                a_or_u_hanboxes.type = "posts";
                hanboxes_posts = new a_or_u_hanboxes();
                hanboxes_posts.Owner = this;
                hanboxes_posts.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void del_post_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var table = post_grid.SelectedItem as grid_items;
                int result = (int)MessageBox.Show("Удалить должность " + table.name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                switch (result)
                {
                    case (int)MessageBoxResult.Yes:
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("delete from posts where id=@id", connection);
                        command.Parameters.AddWithValue("id", table.id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        refresh("handbooks");
                        break;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void add_citi_Click(object sender, RoutedEventArgs e)
        {
            if (!hanboxes_cities.IsLoaded)
            {
                var table = cities_grid.SelectedItem as grid_items;
                a_or_u_hanboxes.type = "cities";
                a_or_u_hanboxes.hanbox_id = -1;
                hanboxes_cities = new a_or_u_hanboxes();
                hanboxes_cities.Owner = this;
                hanboxes_cities.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void upd_citi_Click(object sender, RoutedEventArgs e)
        {
            if (!hanboxes_cities.IsLoaded)
            {
                var table = cities_grid.SelectedItem as grid_items;
                a_or_u_hanboxes.hanbox_id = int.Parse(table.id.ToString());
                a_or_u_hanboxes.type = "cities";
                hanboxes_cities = new a_or_u_hanboxes();
                hanboxes_cities.Owner = this;
                hanboxes_cities.Show();
            }
            else
            {
                add_rolles.Focus();
            }
        }

        private void del_cities_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var table = cities_grid.SelectedItem as grid_items;
                int result = (int)MessageBox.Show("Удалить город " + table.name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                switch (result)
                {
                    case (int)MessageBoxResult.Yes:
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("delete from cities where id=@id", connection);
                        command.Parameters.AddWithValue("id", table.id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        refresh("handbooks");
                        break;
                }
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
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
                        MySqlCommand command = new MySqlCommand("delete from workers where id=@id", connection);
                        command.Parameters.AddWithValue("id", table.id);
                        command.ExecuteNonQuery();
                        refresh();
                        connection.Close();
                        break;
                }
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void add_st_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Sotr.IsLoaded)
            {
                add_Sotr = new add_sotr();
                add_sotr.id_sotr = null;
                add_Sotr.Owner = this;
                add_Sotr.Show();
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
            aunt.Height = 39;
            no_visible();
        }

        public void sel_change()
        {
            try
            {
                string query = "select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name  from calls t " +
                    "join users tt on tt.id = t.id_oper";
                string filt = "";
                if (org_filt.SelectedValue != null)
                {
                    filt = filt + " and id_org = " + org_filt.SelectedValue;
                }
                if (dat_filt.Text != null && dat_filt.Text != "")
                {
                    filt = filt + " and date_cal like '%" + dat_filt.Text + "%'";
                }
                if (stat_filt.SelectedIndex != -1)
                {
                    filt = filt + " and status_call = " + stat_filt.SelectedIndex;
                }
                if (oper_filt.SelectedValue != null)
                {
                    filt = filt + " and id_oper = " + oper_filt.SelectedValue;
                }
                filt = filt.Remove(1, 3);
                query = query + " where " + filt;
                connection.Open();
                List<calls> callses = new List<calls>();
                MySqlCommand sel_calls = new MySqlCommand(query, connection);
                MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                while (reader_calls.Read())
                {
                    callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString()));
                }
                calls_grid.ItemsSource = callses;
                connection.Close();
            }
            catch
            {
                connection.Close();
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
            string query = "select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name  from calls t " +
                "join users tt on tt.id = t.id_oper";
            connection.Open();
            List<calls> callses = new List<calls>();
            MySqlCommand sel_calls = new MySqlCommand(query, connection);
            MySqlDataReader reader_calls = sel_calls.ExecuteReader();
            while (reader_calls.Read())
            {
                callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString()));
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
                    filt = filt + " and priority = " + prioryty_org_filt.SelectedIndex;
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
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orgs.Add(new org(reader["id"].ToString(), reader["code"].ToString(), reader["name"].ToString(), reader["city"].ToString(), reader["status"].ToString(), reader["kurator"].ToString(), reader["phone"].ToString(), reader["priority"].ToString()));
                }
                org_grid.ItemsSource = orgs;
                connection.Close();
            }
            catch
            {
                connection.Close();
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

                MySqlCommand sel_sotrs = new MySqlCommand(query, connection);
                MySqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
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
            try
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
                MySqlCommand sel_sotrs = new MySqlCommand(query, connection);
                MySqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
                while (read_sotrs.Read())
                {
                    workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
                }
                sotr_grid.ItemsSource = workers;
                connection.Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
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
            try
            {
                List<permision> permisions = new List<permision>();
                var roll = roll_grid.SelectedValue as roll;
                connection.Open();
                MySqlCommand command = new MySqlCommand("select t.rights from rols t where t.id = @id_rol", connection);
                command.Parameters.AddWithValue("id_rol", roll.id);
                MySqlDataReader reader = command.ExecuteReader();
                string[] permis = null;
                while (reader.Read())
                {
                    permis = reader["rights"].ToString().Split(';');
                }
                reader.Close();
                for (int i = 0; i < permis.Length; i++)
                {
                    MySqlCommand sel_permis = new MySqlCommand("select t.id, t.name from permissions t where t.id = @id_per", connection);
                    sel_permis.Parameters.AddWithValue("id_per", permis[i]);
                    MySqlDataReader read_permis = sel_permis.ExecuteReader();
                    while (read_permis.Read())
                    {
                        permisions.Add(new permision(read_permis["name"].ToString()));
                    }
                    read_permis.Close();
                }
                permis_grid.ItemsSource = permisions;
                connection.Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
}

        private void upd_st_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Sotr.IsLoaded)
            {
                var table = sotr_grid.SelectedValue as worker;
                add_sotr.id_sotr = table.id.ToString();
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
        private void create_file_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void date_call_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //List<calls> calls = new List<calls>();
            //string query = "";
            //connection.Open();
            //MySqlCommand command = new MySqlCommand(query,connection);
            //MySqlDataReader reader = command.ExecuteReader();
            //while(reader.Read())
            //{
            //    calls.add
            //}
            //connection.Close();
        }
        private Excel.Workbooks excelappworkbooks;
        private Excel.Workbook excelappworkbook;
        private void create_report_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = null;
                connection.Open();
                MySqlCommand command = new MySqlCommand("select t.save_path from settings t where t.id_user = @id_user", connection);
                command.Parameters.AddWithValue("id_user", user_id);
                MySqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    path = reader1["save_path"].ToString();
                }
                reader1.Close();
                connection.Close();

                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 15;
                ExcelApp.Cells[1, 1] = "Организация";
                ExcelApp.Cells[1, 2] = "Дата звонка";
                ExcelApp.Cells[1, 3] = "Статус звонка";
                ExcelApp.Cells[1, 4] = "Цель звонка";
                ExcelApp.Cells[1, 5] = "Оператор";
                for (int i = 0; i < calls_grid.Items.Count; i++)
                {
                    var items = calls_grid.Items[i] as calls;
                    ExcelApp.Cells[i + 2, 1] = items.org.ToString();
                    ExcelApp.Cells[i + 2, 2] = items.date_cal.ToString();
                    ExcelApp.Cells[i + 2, 3] = items.status_call.ToString();
                    ExcelApp.Cells[i + 2, 4] = items.call_target.ToString();
                    ExcelApp.Cells[i + 2, 5] = "Костыль";//items..ToString();
                }
                ExcelApp.Height = 800;
                ExcelApp.Width = 800;
                excelappworkbooks = ExcelApp.Workbooks;
                excelappworkbook = excelappworkbooks[1];
                excelappworkbook.SaveAs(path + @"\calls " + DateTime.Now.ToString().Replace(":", ".") + ".xlsx");
                excelappworkbook.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void to_excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path= null;
                connection.Open();
                MySqlCommand command = new MySqlCommand("select t.save_path from settings t where t.id_user = @id_user", connection);
                command.Parameters.AddWithValue("id_user",user_id);
                MySqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    path = reader1["save_path"].ToString();
                }
                reader1.Close();
                connection.Close();

                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 15;
                ExcelApp.Cells[1, 1] = "Наименование";
                ExcelApp.Cells[1, 2] = "Город";
                ExcelApp.Cells[1, 3] = "Приоритет";
                ExcelApp.Cells[1, 4] = "Статус клиента";
                ExcelApp.Cells[1, 5] = "Куратор";
                ExcelApp.Cells[1, 6] = "Телефон";
                for (int i = 0; i < org_grid.Items.Count; i++)
                {
                    var items = org_grid.Items[i] as org;
                    ExcelApp.Cells[i + 2, 1] = items.Name.ToString();
                    ExcelApp.Cells[i + 2, 2] = items.City.ToString();
                    ExcelApp.Cells[i + 2, 3] = items.Prioriry.ToString();
                    ExcelApp.Cells[i + 2, 4] = items.Status.ToString();
                    ExcelApp.Cells[i + 2, 5] = items.Kurator.ToString();
                    ExcelApp.Cells[i + 2, 6] = items.Phone.ToString();
                }
                ExcelApp.Height = 800;
                ExcelApp.Width = 800;
                excelappworkbooks = ExcelApp.Workbooks;
                excelappworkbook = excelappworkbooks[1];
                excelappworkbook.SaveAs(path + @"\organizations " + DateTime.Now.ToString().Replace(":", ".") + ".xlsx");
                excelappworkbook.Close();

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void to_excel_workers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = null;
                connection.Open();
                MySqlCommand command = new MySqlCommand("select t.save_path from settings t where id_user = @id_user", connection);
                command.Parameters.AddWithValue("id_user", user_id);
                MySqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                {
                    path = reader1["save_path"].ToString();
                }
                reader1.Close();
                connection.Close();
                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 15;
                ExcelApp.Cells[1, 1] = "Имя";
                ExcelApp.Cells[1, 2] = "Фамилия";
                ExcelApp.Cells[1, 3] = "Отчество";
                ExcelApp.Cells[1, 4] = "Организация";
                ExcelApp.Cells[1, 5] = "Должность";
                for (int i = 0; i < sotr_grid.Items.Count; i++)
                {
                    var items = sotr_grid.Items[i] as worker;
                    ExcelApp.Cells[i + 2, 1] = items.Name.ToString();
                    ExcelApp.Cells[i + 2, 2] = items.Surname.ToString();
                    ExcelApp.Cells[i + 2, 3] = items.Second_name.ToString();
                    ExcelApp.Cells[i + 2, 4] = items.Org.ToString();
                    ExcelApp.Cells[i + 2, 5] = items.Job.ToString();
                }
                ExcelApp.Height = 800;
                ExcelApp.Width = 800;
                excelappworkbooks = ExcelApp.Workbooks;
                excelappworkbook = excelappworkbooks[1];
                excelappworkbook.SaveAs(path+@"\workers " + DateTime.Now.ToString().Replace(":", ".") + ".xlsx");
                excelappworkbook.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void edit_path_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fileDialog = new FolderBrowserDialog();
                fileDialog.ShowDialog();
                if (fileDialog.SelectedPath != null && fileDialog.SelectedPath != string.Empty)
                {
                    dir_path.Text = fileDialog.SelectedPath;
                }
            }
            catch
            {

            }
        }

        public void setChecBoxValue (CheckBox cb, int val)
        {
            switch(val)
            {
                case 0:
                    cb.IsChecked = false;
                    break;
                case 1:
                    cb.IsChecked = true;
                    break;
            }
        }

        int org_filtt = 0, calls_filt = 0, emps_filt = 0;

        private void orgs_fiter_Checked(object sender, RoutedEventArgs e)
        {
            org_filtt = 1;
        }

        private void orgs_fiter_Unchecked(object sender, RoutedEventArgs e)
        {
            org_filtt = 0;
        }

        private void calls_fiter_Checked(object sender, RoutedEventArgs e)
        {
            calls_filt = 1;
        }

        private void calls_fiter_Unchecked(object sender, RoutedEventArgs e)
        {
            calls_filt = 0;
        }

        private void emps_fiter_Checked(object sender, RoutedEventArgs e)
        {
            emps_filt = 1;
        }

        private void emps_fiter_Unchecked(object sender, RoutedEventArgs e)
        {
            emps_filt = 0;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                MySqlCommand setting = new MySqlCommand("update settings set save_path = @save_path, orgs_search = @orgs_search, emps_search = @emps_search, call_search = @call_search where id_user = @id_user", connection);
                setting.Parameters.AddWithValue("id_user", user_id);
                setting.Parameters.AddWithValue("save_path", dir_path.Text);
                setting.Parameters.AddWithValue("orgs_search", org_filtt);
                setting.Parameters.AddWithValue("emps_search", emps_filt);
                setting.Parameters.AddWithValue("call_search", calls_filt);
                setting.ExecuteNonQuery();
                connection.Close();
                refresh("settings");
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public ChartValues<opertator> analitycs { get; set; }
        public string[] Labels { get; set; }
        private void emploers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int all_calls = 0, add_cals = 0, callback = 0;
                connection.Open();
                MySqlCommand analyze = new MySqlCommand("select count(1) as all_calls from calls_analytics t where t.id_oper = @id", connection); // положительный ответ
                analyze.Parameters.AddWithValue("id", int.Parse(emploers.SelectedValue.ToString()));
                MySqlDataReader reader = analyze.ExecuteReader();
                while (reader.Read())
                {
                    all_calls = int.Parse(reader["all_calls"].ToString());
                }
                reader.Close();
                //
                MySqlCommand analyze1 = new MySqlCommand("select count(1) as add_calls from calls_analytics t where t.id_oper = @id and call_status = 0", connection);
                analyze1.Parameters.AddWithValue("id", int.Parse(emploers.SelectedValue.ToString()));
                MySqlDataReader reader1 = analyze1.ExecuteReader();
                while (reader1.Read())
                {
                    add_cals = int.Parse(reader1["add_calls"].ToString());
                }
                reader1.Close();
                //
                MySqlCommand analyze2 = new MySqlCommand("select count(1) as callback from calls_analytics t where t.id_oper = @id and call_status = 1", connection); // положительный ответ
                analyze2.Parameters.AddWithValue("id", int.Parse(emploers.SelectedValue.ToString()));
                MySqlDataReader reader2 = analyze2.ExecuteReader();
                while (reader2.Read())
                {
                    callback = int.Parse(reader2["callback"].ToString());
                }
                reader2.Close();
                //
                connection.Close();
                analitycs = new ChartValues<opertator>();
                analitycs.Clear();
                analitycs.Add(new opertator(emploers.Text, all_calls));
                analitycs.Add(new opertator(emploers.Text, add_cals));
                analitycs.Add(new opertator(emploers.Text, callback));
                legendTitle.Values = analitycs;
                Labels = new[] { "Всего звонков", "Добавлено звонков", "Положительный ответ" };
                legendTitle.Title = (emploers.SelectedItem as comboItems).name;
                //let create a mapper so LiveCharts know how to plot our CustomerViewModel class
                var customerVmMapper = Mappers.Xy<opertator>()
                    .X((value, index) => index) // lets use the position of the item as X
                    .Y(value => value.value); //and PurchasedItems property as Y

                //lets save the mapper globally
                Charting.For<opertator>(customerVmMapper);
                DataContext = this;
            }
            catch
            {
                connection.Close();
            }
        }

        private void clouse_call_Click(object sender, RoutedEventArgs e)
        {
            dell_call(1);
        }

        private void org_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();
                MySqlCommand analytic = new MySqlCommand("select ((select count(1) from calls_analytics t where t.id_org = @org and t.call_status != 0)/(select count(1) from calls_analytics t where t.id_org = @org) * 100) as answers, ((select count(1) from calls_analytics t where t.id_org = @org and t.call_status = 2)/(select count(1) from calls_analytics t where t.id_org = @org) * 100) as cancel, ((select count(1) from calls_analytics t where t.id_org = @org and t.call_status = 1)/(select count(1) from calls_analytics t where t.id_org = @org) * 100) as susesful", connection);
                analytic.Parameters.AddWithValue("org", org.SelectedValue);
                MySqlDataReader reader = analytic.ExecuteReader();
                if (reader.Read())
                {
                    callbacks.Value = double.Parse(reader["answers"].ToString());
                    clouse_calls.Value = double.Parse(reader["cancel"].ToString());
                    sucsesful_calls.Value = double.Parse(reader["susesful"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void setFiltersVisible(StackPanel panel, int val)
        {
            switch (val)
            {
                case 1:
                    panel.Height = 61;
                    break;
                case 0:
                    panel.Height = 0;
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
