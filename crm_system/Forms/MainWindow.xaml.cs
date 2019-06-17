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
using MenuItem = System.Windows.Controls.MenuItem;
using System.Net;

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
        public string query_orgs = null;
        string query_calls = null;
        string query_emps = null;
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
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
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
        public void show_del_message(string unit)
        {
            MessageBox.Show("Есть записи в разделе ["+ unit + "] ссылающиеся на удаляемую запись", "Прудупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
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
            //

            //Выгрузка организаций
            to_excel.Visibility = Visibility.Hidden;
            to_excel.Height = 0;
            //Выгрузка звонков
            create_report.Visibility = Visibility.Hidden;
            create_report.Height = 0;
            //Выгрузка сотрудников
            to_excel_workers.Visibility = Visibility.Hidden;
            to_excel_workers.Height = 0;
        }

        public void aunt_result()
        {
            if (CheckForInternetConnection())
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
                        roll_name.Text = "Вы вошли под  ролью " + read_rights["name"];
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
                                    //Настройки
                                    settings.Visibility = Visibility.Visible;
                                    settings.Height = 40;
                                    break;
                                case "13":
                                    //Выгрузка организаций
                                    to_excel.Visibility = Visibility.Visible;
                                    to_excel.Height = 20;
                                    break;
                                case "14":
                                    //Выгрузка звонков
                                    create_report.Visibility = Visibility.Visible;
                                    create_report.Height = 20;
                                    break;
                                case "15":
                                    //Выгрузка сотрудников
                                    to_excel_workers.Visibility = Visibility.Visible;
                                    to_excel_workers.Height = 20;
                                    break;

                            }
                        }
                    }
                    connection.Close();
                }
            }
            else 
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void refresh(string tab = null)
        {
            try
            {
                try
                {
                    connection = new MySqlConnection(constr);
                }
                catch
                {

                }
                if (CheckForInternetConnection())
                {
                    try
                    {
                        connection.Open();
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
                                MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, tt.name as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name, t.status_call as status from calls t" +
                                    "join org tt on tt.id = t.id_org" +
                                    "join users tt on tt.id = t.id_oper " +
                                    "where t.id_org = @org_id", connection);
                                sel_calls.Parameters.AddWithValue("org_id", org_id);
                                MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                                while (reader_calls.Read())
                                {
                                    MessageBox.Show(reader_calls["status"].ToString());
                                    callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString(), int.Parse(reader_calls["status"].ToString())));
                                }
                                reader_calls.Close();
                            }
                            else
                            {
                                MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name, t.status_call as status from calls t " +
                                    "join users tt on tt.id = t.id_oper", connection);
                                MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                                while (reader_calls.Read())
                                {
                                    callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString(), int.Parse(reader_calls["status"].ToString())));
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
                        //handbooks
                        if (tab == "handbooks" || tab == null)
                        {
                            List<grid_items> jobs = new List<grid_items>();
                            List<grid_items> cities = new List<grid_items>();
                            List<grid_items> rulls = new List<grid_items>();
                            //Должности
                            //try
                            //{
                            //    MySqlCommand sel_jobs = new MySqlCommand("select t.* from posts t", connection);
                            //    MySqlDataReader read_jobs = sel_jobs.ExecuteReader();
                            //    while (read_jobs.Read())
                            //    {
                            //        jobs.Add(new grid_items(read_jobs["id"].ToString(), read_jobs["name"].ToString()));
                            //    }
                            //    read_jobs.Close();
                            //    post_grid.ItemsSource = jobs;
                            //}
                            //catch (MySqlException sqlEx)
                            //{
                            //    MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении должностей!");
                            //    connection.Close();
                            //}

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
                            catch
                            {
                                //MessageBox.Show(sqlEx.Message.ToString(), "Ошибка при получении Городов!");
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
                            catch
                            {

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
                            try
                            {
                                MySqlCommand sel_sotrs = new MySqlCommand(query, connection);
                                MySqlDataReader read_sotrs = sel_sotrs.ExecuteReader();
                                while (read_sotrs.Read())
                                {
                                    workers.Add(new worker(read_sotrs["id"].ToString(), read_sotrs["name"].ToString(), read_sotrs["surname"].ToString(), read_sotrs["second_name"].ToString(), read_sotrs["org"].ToString(), read_sotrs["post"].ToString()));
                                }
                                read_sotrs.Close();
                                sotr_grid.ItemsSource = workers;
                            }
                            catch
                            {

                            }
                        }
                        //
                        List<comboItems> Orgs = new List<comboItems>();
                        List<comboItems> OrgsAnalisitcs = new List<comboItems>();
                        List<comboItems> Opers = new List<comboItems>();
                        List<grid_items> jobes = new List<grid_items>();
                        MySqlCommand sel_orgs = new MySqlCommand("select id, name from org order by name ", connection);
                        MySqlDataReader orgs_read = sel_orgs.ExecuteReader();
                        while (orgs_read.Read())
                        {
                            Orgs.Add(new comboItems(orgs_read["id"].ToString(), orgs_read["name"].ToString()));
                        }
                        orgs_read.Close();
                        MySqlCommand sel_orgs_analit = new MySqlCommand("select t.id, t.name from org t where exists (select null from calls tt where tt.id_org = t.id) order by name ", connection);
                        MySqlDataReader orgs_read_analit = sel_orgs_analit.ExecuteReader();
                        while (orgs_read_analit.Read())
                        {
                            OrgsAnalisitcs.Add(new comboItems(orgs_read_analit["id"].ToString(), orgs_read_analit["name"].ToString()));
                        }
                        orgs_read_analit.Close();
                        MySqlCommand sel_opers = new MySqlCommand("select t.id, t.name, t.surname, t.second_name from users t " +
                                                                    "where exists(select null from calls tt where tt.id_oper = t.id)", connection);
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
                        org.ItemsSource = OrgsAnalisitcs;

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
                        string save_dir = null;
                        MySqlCommand cm = new MySqlCommand("select t.save_path from settings t where  t.id_user = @id_user", connection);
                        cm.Parameters.AddWithValue("id_user", user_id);
                        MySqlDataReader rd = cm.ExecuteReader();
                        if (rd.Read())
                        {
                            save_dir = rd["save_path"].ToString();

                        }
                        rd.Close();
                        if (!Directory.Exists(save_dir))
                        {
                            MySqlCommand cmd = new MySqlCommand("update settings set save_path = @save_path where id_user = @id_user", connection);
                            cmd.Parameters.AddWithValue("id_user", user_id);
                            cmd.Parameters.AddWithValue("save_path", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                            cmd.ExecuteNonQuery();
                        }
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
                                setFiltersVisible(organiz_filter, orgs_cansel_filter, int.Parse(set_reader["orgs_search"].ToString()));
                                setFiltersVisible(emps_filter, emps_cansel_filter, int.Parse(set_reader["emps_search"].ToString()));
                                setFiltersVisible(calls_filter, calls_cansel_filter, int.Parse(set_reader["call_search"].ToString()));

                            }
                            set_reader.Close();
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
                            try
                            {
                                roll_grid.ItemsSource = rolls;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("asdasd");
                            }
                        }
                        connection.Close();
                    }
                    catch
                    {
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void del__org_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    org table = org_grid.SelectedItem as org;
                    int result = (int)MessageBox.Show("Удалить организацию " + table.Name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    int calls_count = 0, emps_count = 0;
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            MySqlCommand forgn_cnt = new MySqlCommand("select (select count(1) from calls t where t.id_org = @id) as calls_cnt , (select count(1) from workers t where t.id_org = @id) as emps_cnt", connection);
                            forgn_cnt.Parameters.AddWithValue("id", table.Id);
                            MySqlDataReader reader = forgn_cnt.ExecuteReader();
                            while (reader.Read())
                            {
                                calls_count = int.Parse(reader["calls_cnt"].ToString());
                                emps_count = int.Parse(reader["emps_cnt"].ToString());
                            }
                            reader.Close();
                            MessageBox.Show(calls_count + " " + emps_count);
                            if (emps_count != 0 || calls_count != 0)
                            {
                                int resu = (int)MessageBox.Show("Есть записи в разделах [Звонки] и [Сотрудники] ссылающиеся на удаляемую запись, они будут удалены. Продолжить?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                                switch (result)
                                {
                                    case (int)MessageBoxResult.Yes:

                                        MySqlCommand command = new MySqlCommand("delete from calls_analytics t where t.id_org = @id;" +
                                                                                "delete from calls where id_org = @id;" +
                                                                                "delete from workers where id_org = @id;" +
                                                                                "delete from org where id=@id;", connection);
                                        command.Parameters.AddWithValue("id", table.Id);
                                        command.ExecuteNonQuery();
                                        connection.Close();
                                        refresh();
                                        break;
                                }
                            }
                            else
                            {
                                MySqlCommand command = new MySqlCommand("delete from org where id=@id", connection);
                                command.Parameters.AddWithValue("id", table.Id);
                                command.ExecuteNonQuery();
                                connection.Close();
                                refresh();
                            }
                            break;
                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add__org_Click(object sender, RoutedEventArgs e)
        {
            if (!addOrgn.IsLoaded)
            {
                addOrgn = new addOrgn();
                addOrgn.Title = "Добавление организации";
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
            try
            {
                if (!addOrgn.IsLoaded)
                {
                    org table = org_grid.SelectedItem as org;
                    addOrgn.id = table.Id.ToString();
                    addOrgn = new addOrgn();
                    addOrgn.Title = "Редактирование организации "+table.Name;
                    addOrgn.Owner = this;
                    addOrgn.Show();
                }
                else
                {
                    addOrgn.Focus();
                }
            }
            catch
            {

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
                    add_Sotr.Title = "Добавление сорудника";
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

        private void add_st_Click(object sender, RoutedEventArgs e)
        {
            if (!add_Sotr.IsLoaded)
            {
                add_Sotr = new add_sotr();
                add_Sotr.Title = "Добавление сотрудника";
                add_sotr.id_sotr = null;
                add_Sotr.Owner = this;
                add_Sotr.Show();
            }
            else
            {
                add_Sotr.Focus();
            }
        }

        private void view__sotr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add__call_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!add_Call.IsLoaded)
                {
                    org table = org_grid.SelectedItem as org;
                    add_Call = new add_call();
                    add_Call.Owner = this;
                    add_Call.Title = "Добавление Звонка";
                    add_call.id_org = table.Id.ToString();
                    add_call.id_call = null;
                    add_Call.Show();
                }
                else
                {
                    add_Call.Focus();
                }
            }
            catch
            {

            }
        }

        private void del_call__Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    calls table = calls_grid.SelectedValue as calls;
                    int result = (int)MessageBox.Show("Отменить звонок организации [" + table.org + "] назначеный [" + table.date_cal + "] ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    int rolls_count = 0;
                    string[] rols_id = null, us_id = null;
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("update calls set status_call = 2 where id = @id", connection);
                            command.Parameters.AddWithValue("id", table.id);
                            command.ExecuteNonQuery();
                            MySqlCommand analytic = new MySqlCommand("insert into calls_analytics  (id_org,call_status, id_oper, id_call) values ((select t.id_org from calls t where t.id = @id_call), 2, @user_id, @id_call)", connection);
                            analytic.Parameters.AddWithValue("id_call", table.id);
                            analytic.Parameters.AddWithValue("user_id", user_id);
                            analytic.ExecuteNonQuery();
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
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_call__Click(object sender, RoutedEventArgs e)
        {
            if (!add_Call.IsLoaded)
            {
                add_Call = new add_call();
                add_call.id_call = null;
                add_call.id_org = null;
                add_Call.Title = "Добавление Звонка";
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
            try
            {
                if (!add_Call.IsLoaded)
                {
                    calls table = calls_grid.SelectedItem as calls;
                    add_Call = new add_call();
                    add_Call.Title = "Редактирование Звонка";
                    add_call.id_call = table.id.ToString();
                    add_Call.Owner = this;
                    add_Call.Show();
                }
                else
                {
                    add_Call.Focus();
                }
            }
            catch
            {

            }
        }

        private void del_us_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    user table = user_grid.SelectedItem as user;
                    int result = (int)MessageBox.Show("Удалить Пользователя " + table.login + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    int rolls_count = 0;
                    string[] rols_id = null, us_id = null;
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            int us_cnt = 0;
                            MySqlCommand sel_us_cnt = new MySqlCommand("select count(1) as count, REPLACE(GROUP_CONCAT(t.id),',',';') as users_id, REPLACE(GROUP_CONCAT(tt.id),',',';') as rols_id from users t join rols tt on tt.id = t.rol where tt.rights like '%9%' and tt.rights like '%10%'", connection);
                            MySqlDataReader reader = sel_us_cnt.ExecuteReader();
                            while (reader.Read())
                            {
                                us_cnt = int.Parse(reader["count"].ToString());
                                us_id = reader["users_id"].ToString().Split(';');
                                rols_id = reader["rols_id"].ToString().Split(';');
                            }
                            reader.Close();
                            if (us_cnt == 1 && in_arr(us_id, table.id.ToString()) && !in_arr(rols_id, table.roll.ToString()))
                            {
                                MessageBox.Show("В системе должна быть хотя бы однин пользователь, с правами на разделы: [Пользователи] и [Роли]", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                                connection.Close();
                            }
                            else
                            {
                                MySqlCommand del_roll = new MySqlCommand("delete from users where id = @rol_id", connection);
                                del_roll.Parameters.AddWithValue("rol_id", table.id);
                                del_roll.ExecuteNonQuery();
                                connection.Close();
                                refresh("rols");
                                permis_grid.ItemsSource = "";
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_us_Click(object sender, RoutedEventArgs e)
        {
            if (!add_User.IsLoaded)
            {
                add_User = new add_user();
                add_User.Owner = this;
                add_user.id_user = null;
                add_User.Show();
            }
            else
            {
                add_User.Focus();
            }
        }

        private void upd_us_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {

            }
        }

        private void del_roll_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    roll table = roll_grid.SelectedItem as roll;
                    int result = (int)MessageBox.Show("Удалить Роль " + table.name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            int rolls_count = 0, us_cnt = 0;
                            string[] rols_id = null;
                            connection.Open();
                            MySqlCommand rols_cnt = new MySqlCommand("select count(1) as count, REPLACE(GROUP_CONCAT(t.id),',',';') as rols_id from rols t where t.rights like '%9%' and t.rights like '%10%'", connection);
                            MySqlDataReader reader = rols_cnt.ExecuteReader();
                            while (reader.Read())
                            {
                                rolls_count = int.Parse(reader["count"].ToString());
                                rols_id = reader["rols_id"].ToString().Split(';');
                            }
                            reader.Close();
                            MySqlCommand cmd = new MySqlCommand("select count(1) as us_cnt from users t where t.rol = @id", connection);
                            cmd.Parameters.AddWithValue("id", table.id);
                            MySqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                us_cnt = int.Parse(rd["us_cnt"].ToString());
                            }
                            if (rolls_count == 1 && rols_id[0] == table.id.ToString())
                            {
                                MessageBox.Show("В системе должна быть хотя бы одна роль, с правами на разделы: [Пользователи] и [Роли]", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                                connection.Close();
                            }
                            else
                            {
                                if (us_cnt == 0)
                                {
                                    MySqlCommand del_roll = new MySqlCommand("delete from rols where id = @rol_id", connection);
                                    del_roll.Parameters.AddWithValue("rol_id", (roll_grid.SelectedItem as roll).id);
                                    del_roll.ExecuteNonQuery();
                                    connection.Close();
                                    refresh("rols");
                                    permis_grid.ItemsSource = "";
                                }
                                else
                                {
                                    show_del_message("Пользователи");
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_roll_Click(object sender, RoutedEventArgs e)
        {
            if (!add_rolles.IsLoaded)
            {
                add_rolles = new add_rolls();
                add_rolles.Title = "Добавление роли";
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
            try
            {
                if (!add_rolles.IsLoaded)
                {
                    roll table = roll_grid.SelectedItem as roll;
                    add_rolls.id_rool = table.id.ToString();
                    add_rolles = new add_rolls();
                    add_rolles.Title = "Редактирование роли " + table.name;
                    add_rolles.Owner = this;
                    add_rolles.Show();
                }
                else
                {
                    add_rolles.Focus();
                }
            }
            catch
            {

            }
        }

        private void add_post_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!hanboxes_posts.IsLoaded)
                {
                    var table = post_grid.SelectedItem as grid_items;
                    a_or_u_hanboxes.type = "posts";
                    a_or_u_hanboxes.hanbox_id = -1;
                    hanboxes_posts = new a_or_u_hanboxes();
                    hanboxes_posts.Title = "Добавление должности";
                    hanboxes_posts.Owner = this;
                    hanboxes_posts.Show();
                }
                else
                {
                    add_rolles.Focus();
                }
            }
            catch
            {

            }
        }

        private void upd_post_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!hanboxes_posts.IsLoaded)
                {
                    var table = post_grid.SelectedItem as grid_items;
                    a_or_u_hanboxes.hanbox_id = int.Parse(table.id.ToString());
                    a_or_u_hanboxes.type = "posts";
                    hanboxes_posts = new a_or_u_hanboxes();
                    hanboxes_posts.Title = "Редактирование должности "+table.name;
                    hanboxes_posts.Owner = this;
                    hanboxes_posts.Show();
                }
                else
                {
                    add_rolles.Focus();
                }
            }
            catch
            {

            }
        }

        private void del_post_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    var table = post_grid.SelectedItem as grid_items;
                    int result = (int)MessageBox.Show("Удалить должность " + table.name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            int emps_cnt = 0;
                            MySqlCommand sel_posts_cnt = new MySqlCommand("select count(1) as emps_cnt from workers t where t.id_post = @id", connection);
                            sel_posts_cnt.Parameters.AddWithValue("id", table.id);
                            MySqlDataReader reader = sel_posts_cnt.ExecuteReader();
                            while (reader.Read())
                            {
                                emps_cnt = int.Parse(reader["emps_cnt"].ToString());
                            }
                            reader.Close();
                            if (emps_cnt == 0)
                            {
                                MySqlCommand command = new MySqlCommand("delete from posts where id=@id", connection);
                                command.Parameters.AddWithValue("id", table.id);
                                command.ExecuteNonQuery();
                                connection.Close();
                                refresh("handbooks");
                            }
                            else
                            {
                                connection.Close();
                                show_del_message("Сотудники");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void add_citi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!hanboxes_cities.IsLoaded)
                {
                    var table = cities_grid.SelectedItem as grid_items;
                    a_or_u_hanboxes.type = "cities";
                    a_or_u_hanboxes.hanbox_id = -1;
                    hanboxes_cities = new a_or_u_hanboxes();
                    hanboxes_cities.Title = "Добавление города";
                    hanboxes_cities.Owner = this;
                    hanboxes_cities.Show();
                }
                else
                {
                    add_rolles.Focus();
                }
            }
            catch
            {

            }
        }

        private void upd_citi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!hanboxes_cities.IsLoaded)
                {
                    var table = cities_grid.SelectedItem as grid_items;
                    a_or_u_hanboxes.hanbox_id = int.Parse(table.id.ToString());
                    a_or_u_hanboxes.type = "cities";
                    hanboxes_cities = new a_or_u_hanboxes();
                    hanboxes_cities.Title = "Редактирование города "+table.name;
                    hanboxes_cities.Owner = this;
                    hanboxes_cities.Show();
                }
                else
                {
                    add_rolles.Focus();
                }
            }
            catch
            {

            }
        }

        private void del_cities_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    var table = cities_grid.SelectedItem as grid_items;
                    int result = (int)MessageBox.Show("Удалить город " + table.name + " ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            int orgs_cnt = 0;
                            MySqlCommand sel_orgs_cnt = new MySqlCommand("select count(1) as orgs_cnt from org t where t.city = @id", connection);
                            sel_orgs_cnt.Parameters.AddWithValue("id", table.id);
                            MySqlDataReader reader = sel_orgs_cnt.ExecuteReader();
                            while (reader.Read())
                            {
                                orgs_cnt = int.Parse(reader["orgs_cnt"].ToString());
                            }
                            reader.Close();
                            if (orgs_cnt == 0)
                            {
                                MySqlCommand command = new MySqlCommand("delete from cities where id=@id", connection);
                                command.Parameters.AddWithValue("id", table.id);
                                command.ExecuteNonQuery();
                                connection.Close();
                                refresh("handbooks");
                            }
                            else
                            {
                                connection.Close();
                                show_del_message("Организации");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void del_st_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
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
                            connection.Close();
                            refresh();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (CheckForInternetConnection())
            {
                try
                {
                    query_calls = "select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name, t.status_call as status  from calls t " +
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
                    query_calls = query_calls + " where " + filt;
                    connection.Open();
                    List<calls> callses = new List<calls>();
                    MySqlCommand sel_calls = new MySqlCommand(query_calls, connection);
                    MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                    while (reader_calls.Read())
                    {
                        callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString(), int.Parse(reader_calls["status"].ToString())));
                    }
                    calls_grid.ItemsSource = callses;
                    connection.Close();
                }
                catch
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public void sel_change_org()
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    List<org> orgs = new List<org>();
                    query_orgs = "select id, code, name, (select name from cities where id = city) as city, phone, (case status when 0 then 'Добавлен'  when 1 then 'Назначен звонок' when 2 then 'Перезвон' end) as status, (select CONCAT(surname,' ',name) from users where id = kurator) as kurator, (case priority when 0 then 'Низкий' when 1 then 'Средний' when 2 then 'Высокий' end) as priority from org";
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
                    query_orgs = query_orgs + " where " + filt;
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query_orgs, connection);
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
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void sel_change_sot()
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    List<worker> workers = new List<worker>();
                    query_emps = "select id, name, surname, second_name, (select t.name from org t where t.id = id_org) as org, (select t1.name from posts t1 where t1.id = id_post) as post from workers";
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
                    query_emps = query_emps + " where " + filt;
                    connection.Open();
                    MySqlCommand sel_sotrs = new MySqlCommand(query_emps, connection);
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
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (CheckForInternetConnection())
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
                catch (Exception ex)
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void upd_st_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!add_Sotr.IsLoaded)
                {
                    var table = sotr_grid.SelectedValue as worker;
                    add_sotr.id_sotr = table.id.ToString();
                    add_Sotr = new add_sotr();
                    add_Sotr.Title = "Редактирование сотрудника";
                    add_Sotr.Show();
                    add_Sotr.Owner = this;
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
            if (CheckForInternetConnection())
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
                        ExcelApp.Cells[i + 2, 5] = items.oper.ToString();
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
                    MessageBox.Show(ex.Message, "excel");
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void to_excel_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
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
                    MessageBox.Show(ex.Message, "excel");
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void to_excel_workers_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
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
                    excelappworkbook.SaveAs(path + @"\workers " + DateTime.Now.ToString().Replace(":", ".") + ".xlsx");
                    excelappworkbook.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message, "excel");
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (CheckForInternetConnection())
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
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public ChartValues<opertator> analitycs { get; set; }
        public string[] Labels { get; set; }

        public void emps_analytics_rf()
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    int all_calls = 0, add_cals = 0, callback = 0;
                    connection.Open();
                    MySqlCommand analyze = new MySqlCommand("select (select count(1) from calls_analytics t where t.id_oper = @id) as all_cals, (select count(1) from calls_analytics t where t.id_oper = @id and call_status = 0) as add_cals,(select count(1) from calls_analytics t where t.id_oper = @id and call_status = 1) as callback ", connection); // положительный ответ
                    analyze.Parameters.AddWithValue("id", int.Parse(emploers.SelectedValue.ToString()));
                    MySqlDataReader reader = analyze.ExecuteReader();
                    while (reader.Read())
                    {
                        all_calls = int.Parse(reader["all_cals"].ToString());
                        add_cals = int.Parse(reader["add_cals"].ToString());
                        callback = int.Parse(reader["callback"].ToString());
                    }
                    reader.Close();
                    connection.Close();
                    analitycs = new ChartValues<opertator>();
                    analitycs.Clear();
                    analitycs.Add(new opertator(emploers.Text, all_calls));
                    analitycs.Add(new opertator(emploers.Text, add_cals));
                    analitycs.Add(new opertator(emploers.Text, callback));
                    legendTitle.Values = analitycs;
                    Labels = new[] { "Всего звонков", "Добавлено звонков", "Положительный ответ" };
                    legendTitle.Title = (emploers.SelectedItem as comboItems).name;
                    var customerVmMapper = Mappers.Xy<opertator>()
                        .X((value, index) => index)
                        .Y(value => value.value);
                    Charting.For<opertator>(customerVmMapper);
                    DataContext = this;
                }
                catch
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void emploers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            emps_analytics_rf();
        }

        private void clouse_call_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    calls table = calls_grid.SelectedItem as calls;
                    int result = (int)MessageBox.Show("Закрыть звонок организации [" + table.org + "] назначеный [" + table.date_cal + "] ?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                    switch (result)
                    {
                        case (int)MessageBoxResult.Yes:
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("update calls set status_call = 1, id_oper = @id_us where id=@id", connection);
                            command.Parameters.AddWithValue("id", table.id);
                            command.Parameters.AddWithValue("id_us", user_id);
                            command.ExecuteNonQuery();
                            MySqlCommand analytic = new MySqlCommand("insert into calls_analytics (id_org,call_status, id_oper, id_call) values ((select t.id_org from calls t where t.id = @id_call), 1, @user_id, @id_call)", connection);
                            analytic.Parameters.AddWithValue("id_call", table.id);
                            analytic.Parameters.AddWithValue("user_id", user_id);
                            analytic.ExecuteNonQuery();
                            connection.Close();
                            refresh("calls");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void orgs_analytics_rf()
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand analytic = new MySqlCommand("select ((select count(1) from calls t where t.id_org = @org and t.status_call != 0)/(select count(1) from calls t where t.id_org = @org) * 100) as answers, ((select count(1) from calls t where t.id_org = @org and t.status_call = 2)/(select count(1) from calls t where t.id_org = @org) * 100) as cancel, ((select count(1) from calls t where t.id_org = @org and t.status_call = 1)/(select count(1) from calls t where t.id_org = @org) * 100) as susesful", connection);
                    analytic.Parameters.AddWithValue("org", org.SelectedValue);
                    MySqlDataReader reader = analytic.ExecuteReader();
                    if (reader.Read())
                    {
                        double calback, clouse, sucses;
                        if (reader["answers"].ToString() == "")
                        {
                            calback = 0;
                        }
                        else
                        {
                            calback = double.Parse(reader["answers"].ToString());
                        }
                        if (reader["cancel"].ToString() == "")
                        {
                            clouse = 0;
                        }
                        else
                        {

                            clouse = double.Parse(reader["cancel"].ToString());
                        }
                        if (reader["susesful"].ToString() == "")
                        {
                            sucses = 0;
                        }
                        else
                        {

                            sucses = double.Parse(reader["susesful"].ToString());
                        }
                        callbacks.Value = calback;
                        clouse_calls.Value = clouse;
                        sucsesful_calls.Value = sucses;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void org_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            orgs_analytics_rf();
        }

        private void orgs_cansel_filter_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    org_name_filt.Text = "";
                    city_org_filt.Text = "";
                    city_org_filt.SelectedValue = null;
                    prioryty_org_filt.Text = "";
                    prioryty_org_filt.SelectedValue = null;
                    org_status_filt.Text = "";
                    org_status_filt.SelectedValue = null;
                    kurator_org_filt.Text = "";
                    kurator_org_filt.SelectedValue = null;
                    phone_org_filt.Text = "";
                    connection.Open();
                    List<org> orgs = new List<org>();
                    MySqlCommand command = new MySqlCommand("select id, code, name, (select name from cities where id = city) as city, phone, (case status when 0 then 'Добавлен'  when 1 then 'Назначен звонок' when 2 then 'Перезвон' end) as status, (select CONCAT(surname,' ',name) from users where id = kurator) as kurator, (case priority when 0 then 'Низкий' when 1 then 'Средний' when 2 then 'Высокий' end) as priority from org", connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        orgs.Add(new org(reader["id"].ToString(), reader["code"].ToString(), reader["name"].ToString(), reader["city"].ToString(), reader["status"].ToString(), reader["kurator"].ToString(), reader["phone"].ToString(), reader["priority"].ToString()));
                    }
                    org_grid.ItemsSource = orgs;
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void emps_cansel_filter_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    name_filt.Text = "";
                    fam_filt.Text = "";
                    otch_filt.Text = "";
                    org_filt_.Text = "";
                    org_filt_.SelectedValue = null;
                    job_filt.Text = "";
                    job_filt.SelectedValue = null;
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
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void calls_cansel_filter_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    org_filt.Text = "";
                    org_filt.SelectedValue = null;
                    dat_filt.Text = "";
                    stat_filt.Text = "";
                    stat_filt.SelectedValue = null;
                    oper_filt.Text = "";
                    oper_filt.SelectedValue = null;
                    connection.Open();
                    List<calls> callses = new List<calls>();
                    if (org_id != null)
                    {
                        MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' when 2 then 'Отменён' when 3 then 'Перезвон' end as status_call, tt.name, tt.surname, tt.second_name, t.status_call as status  from calls t" +
                            "join users tt on tt.id = t.id_oper " +
                            "where t.id_org = @org_id", connection);
                        sel_calls.Parameters.AddWithValue("org_id", org_id);
                        MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                        while (reader_calls.Read())
                        {
                            callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString(), int.Parse(reader_calls["status"].ToString())));
                        }
                        reader_calls.Close();
                    }
                    else
                    {
                        MySqlCommand sel_calls = new MySqlCommand("select t.id, t.date_cal, t.id_org, (select tt.name from org tt where tt.id = t.id_org) as org, t.call_target,case t.status_call when 0 then 'Назначен' when 1 then 'Закончен' end as status_call, tt.name, tt.surname, tt.second_name, t.status_call as status from calls t " +
                            "join users tt on tt.id = t.id_oper", connection);
                        MySqlDataReader reader_calls = sel_calls.ExecuteReader();
                        while (reader_calls.Read())
                        {
                            callses.Add(new calls(reader_calls["id"].ToString(), reader_calls["date_cal"].ToString(), reader_calls["org"].ToString(), reader_calls["call_target"].ToString(), reader_calls["status_call"].ToString(), reader_calls["id_org"].ToString(), reader_calls["name"].ToString() + " " + reader_calls["surname"].ToString() + " " + reader_calls["second_name"].ToString(), int.Parse(reader_calls["status"].ToString())));
                        }
                        reader_calls.Close();
                    }
                    calls_grid.ItemsSource = callses;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dat_filt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_change();
        }

        private void calls_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var table = calls_grid.SelectedItem as calls;
                int status = int.Parse(table.status.ToString());
                switch (status)
                {
                    case 1: //закончен
                        del_call_.Height = 0;
                        clouse_call.Height = 0;
                        break;
                    case 2: //отменён
                        del_call_.Height = 0;
                        clouse_call.Height = 0;
                        break;
                    default:
                        del_call_.Height = 20;
                        clouse_call.Height = 20;
                        break;
                }
            }
            catch
            {

            }
        }

        private void delete_call_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    connection.Open();
                    calls calls = calls_grid.SelectedValue as calls;
                    MySqlCommand command = new MySqlCommand("delete from calls_analytics t where t.id_org = @id;" +
                                                            "delete from calls where id = @id;", connection);
                    command.Parameters.AddWithValue("id", calls.id);
                    command.ExecuteNonQuery();
                    refresh("calls");
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void refr_orgs_Click(object sender, RoutedEventArgs e)
        {
            
            if (query_orgs != null)
            {
                sel_change_org();
            }
            else
            {
                refresh("orgs");
            }
        }

        private void refr_calls_Click(object sender, RoutedEventArgs e)
        {
            if (query_calls != null)
            {
                sel_change();
            }
            else
            {
                refresh("calls");
            }
        }

        private void refr_emps_Click(object sender, RoutedEventArgs e)
        {
            if (query_emps != null)
            {
                sel_change_sot();
            }
            else
            {
                refresh("emps");
            }
        }

        private void refr_us_Click(object sender, RoutedEventArgs e)
        {
            refresh("users");
        }

        private void refr_rols_Click(object sender, RoutedEventArgs e)
        {
            refresh("rols");
        }

        private void refresh_posts_Click(object sender, RoutedEventArgs e)
        {
            refresh("handbooks");
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh("handbooks");
        }

        private void refresh_analytics_orgn_Click(object sender, RoutedEventArgs e)
        {
            orgs_analytics_rf();
        }

        private void refresh_analytics_emps_Click(object sender, RoutedEventArgs e)
        {
            emps_analytics_rf();
        }

        private void refresh_settings_Click(object sender, RoutedEventArgs e)
        {
            refresh("settings");
        }

        public void setFiltersVisible(StackPanel panel,MenuItem mt, int val)
        {
            switch (val)
            {
                case 1:
                    panel.Height = 61;
                    mt.Height = 20;
                    break;
                case 0:
                    panel.Height = 0;
                    mt.Height = 0;
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
