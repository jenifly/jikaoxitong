using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Update
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
            Application.Run(new Form1());
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
