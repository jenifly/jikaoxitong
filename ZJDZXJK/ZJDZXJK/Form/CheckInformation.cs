using DSkin.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZJDZXJK.Cache;

namespace ZJDZXJK
{
    public partial class CheckInformation : DSkinForm
    {
        private bool check = true;
        private Login login;
        public CheckInformation(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void CheckInformation_Load(object sender, EventArgs e)
        {
            dSkinLabel8.Text = "姓名：" + JCache.userdata[1];
            dSkinLabel3.Text = "身份证号：" + JCache.userdata[3];
            dSkinLabel5.Text = "工资号：" + JCache.userdata[5];
            dSkinLabel7.Text = "班级：" + JCache.userdata[6];
            if (int.Parse(JCache.userdata[8].ToString()) < 3)
                dSkinLabel6.Text = "职称：管理员";
        }

        #region Others Helper
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (check)
            {
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
        #endregion

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            check = false;
            Close();
            Animation.AnimationEnd += Animation_AnimationEnd;
        }

        void Animation_AnimationEnd(object sender, DSkin.Animations.AnimationEventArgs e)
        {
            login.isShow = true;
            login.Location = Location;
            login.Animation.Asc = true;
            login.Animation.AnimationStarted += Animation_AnimationStarted;
            login.Animation.Start();
        }

        void Animation_AnimationStarted(object sender, DSkin.Animations.AnimationEventArgs e)
        {
            login.Show();
            Dispose();
        }

        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            Animation.Effect = new DSkin.Animations.ZoomEffect();
            check = false;
            switch (JCache.userdata[8].ToString())
            {
                case "1":
                    loginAdmin();
                    break;
                case "2":
                    loginAdmin();
                    break;
                case "3":
                    if (JCache.userdata[10].ToString().Equals(DateTime.Now.ToString("yyyy_MM_dd")))
                    {
                        if (DSkinMessageBox.Show("你已完成今日测试，再次测试数据不会传入后台数据库，确定继续吗？",
                               "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            JCache.AdminModer = false;
                            new Check_QustionBank().Show();
                            Close();
                            login.Dispose();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        JCache.AdminModer = true;
                        new Check_QustionBank().Show();
                        Close();
                        login.Dispose();
                    }
                    break;
            }
        }

        private void loginAdmin()
        {
            if (DSkinMessageBox.Show("管理员"+JCache.userdata[1]+"，您要登录后台吗？（选择否进入前台答题界面）",
                "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                new Admin().Show();
                Close();
                login.Dispose();
            }
            else
            {
                if (DSkinMessageBox.Show("您的答题数据要汇入后台数据库吗？",
               "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (JCache.userdata[10].ToString().Equals(DateTime.Now.ToString("yyyy_MM_dd")))
                    {
                        if (DSkinMessageBox.Show("你已完成今日测试，再次测试数据不会传入后台数据库，确定继续吗？",
                               "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            JCache.AdminModer = false;
                            new Check_QustionBank().Show();
                            Close();
                            login.Dispose();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        JCache.AdminModer = true;
                        new Check_QustionBank().Show();
                        Close();
                        login.Dispose();
                    }
                }
                else
                {
                    JCache.AdminModer = false;
                    new Check_QustionBank().Show();
                    Close();
                    login.Dispose();
                }
            }
        }
        private void CheckInformation_SystemButtonMouseClick(object sender, SystemButtonMouseClickEventArgs e)
        {
            if(dSkinPanel2.Visible == false)
                dSkinPanel2.Visible = true;
            else
                dSkinPanel2.Visible = false;
        }

        private void dSkinButton4_Click(object sender, EventArgs e)
        {
            dSkinPanel2.Visible = false;
        }

        private void dSkinButton3_Click(object sender, EventArgs e)
        {
            if (dSkinTextBox1.Text.Length == 0 || dSkinTextBox2.Text.Length == 0)
            {
                DSkinMessageBox.Show("提示", "输入不能为空！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!dSkinTextBox1.Text.Equals(dSkinTextBox2.Text))
            {
                DSkinMessageBox.Show("提示", "两次输入密码不一致！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (JCache.mysqlhelper.GetExecute(String.Format("update {0}.{1} set F8={2} where F4={3}",
                JCache.databaseName, JCache.dbt_user, dSkinTextBox1.Text, JCache.userdata[3])) > 0)
            {
                dSkinPanel2.Visible = false;
                DSkinMessageBox.Show("提示",  "密码修改成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
                DSkinMessageBox.Show("提示", "密码修改失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
