using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void CheckFieldsCaption(TextBox tb, string type)
        {

        }
    }
}
