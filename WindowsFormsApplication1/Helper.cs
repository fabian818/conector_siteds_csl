using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Helper
    {
        public static string GetDate(string date)
        {
            if(date.Equals(""))
            {
                return null;
            }
            else
            {
                string year = date.Substring(0, 4);
                string month = date.Substring(4, 2);
                string day = date.Substring(6, 2);
                date = year + "-" + month + "-" + day;
                return date;
            }
            
        }

        public static string GetDateTime(string date)
        {
            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);
            string hour = date.Substring(8, 2);
            string minute = date.Substring(10, 2);
            date = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":00";
            return date;
        }

        public static string SuavizatingCode(string code)
        {
            if (code.Length != 6)
            {
                return "0" + code;
            }
            else
            {
                return code;
            }
        }
        public static string GetCorrectDate(string date)
        {
            if (date.Equals(""))
            {
                return "2000-12-31";
            }
            else
            {
                date = date.Substring(0, 10);
                string year = date.Substring(6, 4);
                string month = date.Substring(3, 2);
                string day = date.Substring(0, 2);
                return year + "-" + month + "-" + day;
            }            
        }

        public static string GetCategory(string code)
        {
            return code.Substring(0, 2);
        }

        public static string GetSubCategory(string code)
        {
            return code.Substring(0, 4);
        }

        public static string SplitPoints(string code)
        {
            return code.Replace(".", "");
        }
    }
}
