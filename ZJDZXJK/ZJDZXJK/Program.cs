using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZJDZXJK;
using ZJDZXJK.Cache;

namespace CoreSysExpress
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Read();
            JCache.mysqlhelper = new ZJDZXJK.DBHelper.MySQLHelper();
            //if(DateTime.Now.Year == 2017 && DateTime.Now.Month == 8 && DateTime.Now.Day < 31)
            // {
            //    DSkinMessageBox.Show("此版本为测试&试用版，将在2017年8月31日过期。", "提示");
            new Login().Show();
                Application.Run();
           // }
           // else
          //  {
          //      DSkinMessageBox.Show("本产品已过期！", "提示");
         //   }

         //   new ShowData().Show();
       //     Application.Run();
        }

        public static void Read()
        {
            String str = "";
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "\\config", Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                str += line;
            }
            string[] strs = str.Split(',');
            JCache.current_versions = strs[1];
            JCache.dbType = strs[0];
            sr.Close();
        }
    }
}
