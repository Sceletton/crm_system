using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace crm_system
{
    public class CheckFields
    {
        public void CheckNullFields(TextBox[] textBoxes)
        {
            if(textBoxes.Length!=0 || textBoxes != null)
            {
                for (int i = 0; i< textBoxes.Length;i++)
                {
                    if(textBoxes[i].Text == null || textBoxes[i].Text == "")
                    {
                        textBoxes[i].BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        textBoxes[i].BorderBrush = Brushes.Black;
                    }
                }
            }
        }
        public bool array_exists(string[] mass1, string var)
        {
            for (int i = 0; i<= mass1.Length; i++)
            {
                if (mass1[i] == var)
                {
                    return true;
                }
            }
            return false;
        }
        public void CheckFieldsCaption(TextBox tb, string type = "alphanumber")
        {
            Regex regex;
            MatchCollection match;
            int selStart = tb.SelectionStart; 
            try
            {
                switch (type)
                {
                    case "alpha":
                        regex = new Regex(@"\W");
                        if (!char.IsDigit(tb.Text[tb.SelectionStart - 1]))
                        {
                            match = regex.Matches(tb.Text[tb.SelectionStart - 1].ToString());
                            if (match.Count > 0)
                            {
                                tb.Text = tb.Text.Remove(tb.SelectionStart - 1, 1);
                                tb.SelectionStart = selStart - 1;
                            }
                        }
                        else
                        {
                            tb.Text = tb.Text.Remove(tb.SelectionStart - 1, 1);
                            tb.SelectionStart = selStart - 1;
                        }
                        break;
                    case "number":
                        regex = new Regex(@"\D");
                        match = regex.Matches(tb.Text[tb.SelectionStart - 1].ToString());
                        if (match.Count > 0)
                        {
                            tb.Text = tb.Text.Remove(tb.SelectionStart - 1, 1);
                            tb.SelectionStart = selStart - 1;
                        }
                        break;
                    case "alphanumber":
                        regex = new Regex(@"\W");
                        match = regex.Matches(tb.Text[tb.SelectionStart - 1].ToString());
                        if (match.Count > 0 && tb.Text[tb.SelectionStart - 1].ToString() != " " && tb.Text[tb.SelectionStart - 1].ToString() != ('"').ToString() && tb.Text[tb.SelectionStart - 1].ToString() != "<" && tb.Text[tb.SelectionStart - 1].ToString() != ">" && tb.Text[tb.SelectionStart - 1].ToString() != "(" && tb.Text[tb.SelectionStart - 1].ToString() != ")" && tb.Text[tb.SelectionStart - 1].ToString() != "-")
                        {
                            tb.Text = tb.Text.Remove(tb.SelectionStart - 1, 1);
                            tb.SelectionStart = selStart - 1;
                        }
                        break;
                }
            }
            catch
            {

            }


        }
    }
}
