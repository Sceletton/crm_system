using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm_system.DB
{
    class org
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string Kurator { get; set; }
        public string Phone { get; set; }
        public string Prioriry { get; set; }

        public org(string id, string code, string name, string city, string status, string kurator, string phone, string prioriry)
        {
            Id = int.Parse(id);
            Name = name;
            Code = code;
            City = city;
            Status = status;
            Kurator = kurator;
            Phone = phone;
            Prioriry = prioriry;
        }
    }
    public class calls
    {
        public int id { get; set; }
        public string date_cal { get; set; }
        public string id_org { get; set; }
        public string org { get; set; }
        public string call_target { get; set; }
        public string status_call { get; set; }
        public string oper { get; set; }
        public int status { get; set; }
        public calls(string Id, string Date_call, string Org, string Call_target, string Status_call, string id_org, string Oper, int Status)
        {
            id = int.Parse(Id);
            date_cal = Date_call;
            org = Org;
            call_target = Call_target;
            status_call = Status_call;
            oper = Oper;
            status = Status;
        }
    }
    public class user
    {
        public int id { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public string roll { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string second_name { get; set; }
        public user(string Id, string Login, string Pass, string Roll, string Name, string Surname, string Second_name)
        {
            id = int.Parse(Id);
            login = Login;
            pass = Pass;
            roll = Roll;
            name = Name;
            surname = Surname;
            second_name = Second_name;
        }
    }
    public class roll
    {
        public int id { get; set; }
        public string name { get; set; }
        public roll(string Id, string Name)
        {
            id = int.Parse(Id);
            name = Name;
        }
    }
    public class grid_items
    {
        public int id { get; set; }
        public string name { get; set; }

        public grid_items(string Id, string Name)
        {
            id = int.Parse(Id);
            name = Name;
        }
    }
    public class worker
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Second_name { get; set; }
        public string Org { get; set; }
        public string Job { get; set; }

        public worker(string Id, string name, string surname, string second_name, string org, string job)
        {
            id = int.Parse(Id);
            Name = name;
            Surname = surname;
            Second_name = second_name;
            Org = org;
            Job = job;
        }
    }

    public class comboItems
    {
        public string value { get; set; }
        public string name { get; set; }
        public comboItems(string val, string nam)
        {
            value = val;
            name = nam;

        }
    }

    public class permision
    {
        public string caption { get; set; }
        public permision(string Caption)
        {
            caption = Caption;
        }
    }
    public class opertator
    {
        public string fio { get; set; }
        public int value { get; set; }
        public opertator(string Fio, int val)
        {
            fio = Fio;
            value = val;
        }
    }
}
