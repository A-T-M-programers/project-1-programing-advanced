using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;

namespace project_1_programing_advanced
{
    public class Valdition
    {
        TextBox textBox;
        public bool Ifempty(TextBox textBox1)
        {
            bool result;
            if (textBox1.Text==""||textBox1.Text==null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        public bool ToEmpty(TextBox textBox1)
        {
            bool result1;
            string result = textBox1.Text;
            string newresult = "";
            for (int i = 0; i < result.Length-1; i++)
            {
                if (result[i]==' '&&result[i+1]==' ')
                {
                    for (int j = 0; j < result.Length-1; j++)
                    {
                        if ((i+1)!=j)
                        {
                            newresult += result[j];
                        }
                    }
                    result = newresult;
                }
            }
            if (result==" ")
            {
                result = "";
                result1 = false;
            }
            else
            {
                result1 = true;
            }
            
            return result1;
        }
    }
}