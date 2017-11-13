using DSkin.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZJDZXJK.Cache;

namespace ZJDZXJK
{
    public partial class Login : DSkinForm
    {
        private bool check = true;
        private string filename = Environment.CurrentDirectory + "\\Update.exe";
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            dSkinLabel2.Text = "by——Jenifly   v-" + JCache.current_versions;
            JCache.mysqlhelper.CreateDatabase(JCache.databaseName);
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_tiku_manager, String.Format("CREATE TABLE {0}.{1}" +
                "(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(20), F3 TEXT,F4 VARCHAR(20),F5 VARCHAR(50)," +
                "F6 VARCHAR(50),F7 VARCHAR(50),F8 VARCHAR(30),PRIMARY KEY (F1))",
                JCache.databaseName, JCache.dbt_tiku_manager));
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_exam_manager, String.Format("CREATE TABLE {0}.{1}" +
              "(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(20), F3 VARCHAR(20),F4 VARCHAR(20),F5 VARCHAR(50)," +
              "F6 VARCHAR(50),F7 VARCHAR(50),F8 VARCHAR(30),F9 VARCHAR(30),F10 VARCHAR(30),F11 VARCHAR(30),PRIMARY KEY (F1))",
              JCache.databaseName, JCache.dbt_exam_manager));
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_exam_qusetion, String.Format("CREATE TABLE {0}.{1}" +
               "(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(20),F3 VARCHAR(30),PRIMARY KEY (F1))",
               JCache.databaseName, JCache.dbt_exam_qusetion));
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_user, String.Format("CREATE TABLE {0}.{1}" +
              "(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(20),F3 VARCHAR(20),F4 VARCHAR(20),F5 VARCHAR(20)," +
              "F6 VARCHAR(20),F7 VARCHAR(20),F8 VARCHAR(20),F9 INT DEFAULT '3',F10 INT DEFAULT '0',F11 VARCHAR(20),PRIMARY KEY (F1))",
              JCache.databaseName, JCache.dbt_user));
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_user_score, String.Format("CREATE TABLE {0}.{1}" +
             "(F1 INT NOT NULL AUTO_INCREMENT,F2 INT,F3 VARCHAR(20),F4 VARCHAR(20),F5 VARCHAR(20),F6 VARCHAR(20),PRIMARY KEY (F1))",
             JCache.databaseName, JCache.dbt_user_score));
            JCache.mysqlhelper.CreateTable(JCache.databaseName, JCache.dbt_versions, String.Format("CREATE TABLE {0}.{1} (F1 VARCHAR(20))",
             JCache.databaseName, JCache.dbt_versions));
            if (JCache.mysqlhelper.CheckSomething("select * from " + JCache.dbt_versions) > 0)
            {
                if (!JCache.mysqlhelper.GetSomething("F1","select * from " + JCache.dbt_versions).Equals(JCache.current_versions))
                    if (DSkinMessageBox.Show("当前机考系统版本过低，请更新系统！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(filename);
                        Environment.Exit(0);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
               
                JCache.mysqlhelper.GetExecute(String.Format("update {0} set F1={1}",JCache.dbt_versions, "0.0.0.1"));

            }
            else
            {
                JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F1) values ('0.0.0.1')", JCache.databaseName, 
                    JCache.dbt_versions));
            }
        }


        public bool isShow = false;
        bool isFirst = true;
        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            String username = dSkinTextBox1.Text;
            String password = dSkinTextBox2.Text;
            if (username.Length == 0)
            {
                DSkinMessageBox.Show("用户名不能为空！", "提示", MessageBoxIcon.Error);
                return;
            }
            if (password.Length == 0)
            {
                DSkinMessageBox.Show("密码不能为空！","提示", MessageBoxIcon.Error);
                return;
            }
            DataTable dt = JCache.mysqlhelper.GetDataTable(
                String.Format("select * from {0} where F4='{1}'",JCache.dbt_user, username), JCache.dbt_user);
            if(dt != null)
                JCache.userdata = dt.Rows[0];
            else
            {
                DSkinMessageBox.Show("该用户不存在！", "提示", MessageBoxIcon.Error);
                return;
            }
            if(JCache.userdata[7].ToString().Equals("") || JCache.userdata[7].ToString().Equals(password))
            {
                check = false;
                MyLogin();
            }
            else
                DSkinMessageBox.Show("密码错误！", "提示", MessageBoxIcon.Error);
        }

        void MyLogin()
        {
            isShow = false;
            if (isFirst)
            {
                this.Animation.Effect = new DSkin.Animations.ThreeDTurn();
                this.Animation.AnimationEnd += Animation_AnimationEnd;
            }
            isFirst = false;

            this.Animation.Asc = false;
            this.Animation.Start();
        }
        void Animation_AnimationEnd(object sender, DSkin.Animations.AnimationEventArgs e)
        {
            if (!isShow)
            {
                Hide();
                CheckInformation checkInformation = new CheckInformation(this);
                checkInformation.Location = Location;
                checkInformation.Animation.Effect = new DSkin.Animations.ThreeDTurn();
                checkInformation.Show();
            }
        }

        #region Others Helper

        private void dSkinTextBox1_MouseEnter(object sender, EventArgs e)
        {
            dSkinBaseControl1.Borders.AllColor = Color.DeepSkyBlue;
        }

        private void dSkinTextBox1_MouseLeave(object sender, EventArgs e)
        {
            dSkinBaseControl1.Borders.AllColor = Color.Silver;
        }

        private void dSkinTextBox2_MouseEnter(object sender, EventArgs e)
        {
            dSkinBaseControl2.Borders.AllColor = Color.DeepSkyBlue;
        }

        private void dSkinTextBox2_MouseLeave(object sender, EventArgs e)
        {
            dSkinBaseControl2.Borders.AllColor = Color.Silver;
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (check) {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (DSkinMessageBox.Show("确定要退出程序?", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }
            else
                e.Cancel = false;
        }
    }
}
