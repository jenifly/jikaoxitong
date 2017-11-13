using DSkin.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Update
{
    public partial class Form1 : DSkinForm
    {
        private String new_versions;
        private string filename = Environment.CurrentDirectory + "\\2017岗前培训机考系统.exe";
        private string URL = "http://10.5.81.68/2017岗前培训机考系统.exe";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            new_versions = GetSomething("F1", "select * from " + JCache.dbt_versions);
            if (new_versions.Equals(JCache.current_versions))
            {
                if (DSkinMessageBox.Show("当前机考系统已是最新版！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }
        public String GetSomething(String st, String sqltxt)
        {
            try
            {
                using (MySqlConnection conn = SQLConnect(JCache.databaseName))
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand(sqltxt, conn);
                    MySqlDataReader mySqlDataReader = comm.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        return mySqlDataReader[st].ToString();
                    }
                }
            }
            catch (Exception)
            {
                if (DSkinMessageBox.Show("服务器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
            return null;
        }
        MySqlConnection SQLConnect(String databaseName)
        {
            return new MySqlConnection(String.Format(@"Data Source={0};User ID={1};Password={2};Database={3};pooling=false;CharSet=utf8",
             JCache.dbType, JCache.userId, JCache.password, databaseName));
        }

        public void DownloadFile()
        {
            float percent = 0;
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest) HttpWebRequest.Create(URL);
                Myrq.Timeout = 2000;
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                if (File.Exists(filename))
                    File.Delete(filename);
                Stream so = new FileStream(filename, FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes;
                    dSkinLabel5.Width = (int)((float)dSkinPanel1.Width * percent);
                    dSkinLabel4.Text = (percent * 100).ToString("00.00") + "%";
                    Application.DoEvents();
                }
                dSkinButton1.Text = "完  成";
                dSkinButton1.Enabled = true;
                so.Close();
                st.Close();
            }
            catch (Exception)
            {
                if (DSkinMessageBox.Show("服务器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if (dSkinButton1.Text.Equals("开始下载"))
            {
                dSkinButton1.Enabled = false;
                DownloadFile();
            }
            else
            {
                System.Diagnostics.Process.Start(filename);
                File.Delete(Environment.CurrentDirectory + "\\config");
                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(JCache.dbType + "," + new_versions);
                sw.Flush();
                sw.Close();
                fs.Close();
                Environment.Exit(0);
            }
                
        }
    }
}
