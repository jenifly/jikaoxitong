using ZJDZXJK.Cache;
using ZJDZXJK.DBHelper;
using ZJDZXJK.View;
using DSkin.Controls;
using DSkin.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using ZJDZXJK.Helper;
using System.Collections.Generic;
using System.Text;

namespace ZJDZXJK
{
    public partial class Admin : DSkinForm
    {
        public String QB_Setting_tbName;
        private Loading loading;
        private SaveFileDialog saveFileDialog;
        private bool MouseIsDown = false;
        private Rectangle MouseRect = Rectangle.Empty; //矩形（为鼠标画出矩形选区）
        private DSkinToolTip tip;
        private int count;
        private Timer timer = new Timer();

        public Admin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show();
            tip = new DSkinToolTip();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            dSkinGridList4.MouseDown += new MouseEventHandler(JMouseDown);
            dSkinGridList4.MouseUp += new MouseEventHandler(JMouseUp);
            dSkinGridList4.MouseMove += new MouseEventHandler(JMouseMove);
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork0);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted0);
            work.RunWorkerAsync(this);
        }

        #region mouseMove
        //定义三个方法
        private void ResizeToRectangle(object sender, Point p)
        {
            DrawRectangle(sender);
            MouseRect.Width = p.X - MouseRect.Left;
            MouseRect.Height = p.Y - MouseRect.Top;
            DrawRectangle(sender);
        }
        private void DrawRectangle(object sender)
        {
            Rectangle rect = ((DSkinGridList)sender).RectangleToScreen(MouseRect);
            ControlPaint.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }

        private void DrawStart(object sender, Point StartPoint)
        {
            ((DSkinGridList)sender).Capture = true;
            MouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }

        private void JMouseDown(object sender, MouseEventArgs e)
        {
            if(e.Location.Y > 30 && e.Location.Y < 30 * (count + 1))
            {
                MouseIsDown = true;
                DrawStart(sender, e.Location);
            }
        }

        private void JMouseUp(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
            {
                MouseIsDown = false;
                DrawRectangle(sender);
                if (MouseRect.Height > 5)
                {
                    int k = 30 - MouseRect.Top % 30;
                    int l = (MouseRect.Height - k) % 30;
                    int m = (MouseRect.Top - 30) / 30;
                    int j = 30 - MouseRect.Top % 30;
                    int n = (MouseRect.Height - k) / 30;
                    if (l > 0)
                        n++;
                    if (j > 0)
                        n++;
                    if (n > count - m)
                        n = count - m;
                    tip.Show( n + " 条数据", dSkinGridList4, e.Location);
                    delay = 2;
                    timer.Start();
                }
                MouseRect = Rectangle.Empty;
            }
        }

        private void JMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
                ResizeToRectangle(sender, e.Location);
        }

        int delay = 2;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (--delay == 0)
            {
                tip.Hide(dSkinGridList4);
                timer.Stop();
            }
        }
        #endregion

        void work_DoWork0(object sender, DoWorkEventArgs e)
        {
            dSkinListBox1.ItemClick += new EventHandler<ItemClickEventArgs>(ListBox_ItemClick);
            Admin_ItemView iv1 = new Admin_ItemView();
            iv1.ItemLoad("题库管理");
            iv1.DBaseControl.InnerDuiControl.Tag = "0";
            dSkinListBox1.Items.Add(iv1.DBaseControl.InnerDuiControl);
            Admin_ItemView iv2 = new Admin_ItemView();
            iv2.ItemLoad("人员管理");
            iv2.DBaseControl.InnerDuiControl.Tag = "1";
            dSkinListBox1.Items.Add(iv2.DBaseControl.InnerDuiControl);
            Admin_ItemView iv3 = new Admin_ItemView();
            iv3.ItemLoad("考试管理");
            iv3.DBaseControl.InnerDuiControl.Tag = "2";
            dSkinListBox1.Items.Add(iv3.DBaseControl.InnerDuiControl);
            Admin_ItemView iv4 = new Admin_ItemView();
            iv4.ItemLoad("考试结果查看");
            iv4.DBaseControl.InnerDuiControl.Tag = "3";
            dSkinListBox1.Items.Add(iv4.DBaseControl.InnerDuiControl);
            dSkinListBox1.Items[0].BackColor = Color.DarkCyan;
            Admin_ItemView iv5 = new Admin_ItemView();
            iv5.ItemLoad("考试试题分析");
            iv5.DBaseControl.InnerDuiControl.Tag = "4";
            dSkinListBox1.Items.Add(iv5.DBaseControl.InnerDuiControl);
            dSkinListBox1.Items[0].BackColor = Color.DarkCyan;
            dSkinListBox1.Items[0].BackColor = Color.WhiteSmoke;
            dSkinListBox1.Items[0].Text = "0";
        }

        void work_RunWorkerCompleted0(object sender, RunWorkerCompletedEventArgs e)
        {
            dSkinLabel2.Text = "您的登录时间是：\n\n" + DateTime.Now.ToString("yyyy年MM月dd日HH:mm");
            if (JCache.userdata[8].ToString().Equals("1"))
            {
                dSkinLabel3.Text = "您好，" + JCache.userdata[1] + "\n\n权限：超级管理员";
                Admin_ItemView iv6 = new Admin_ItemView();
                iv6.ItemLoad("管理员设置");
                iv6.DBaseControl.InnerDuiControl.Tag = "5";
                dSkinListBox1.Items.Add(iv6.DBaseControl.InnerDuiControl);
                Admin_ItemView iv7 = new Admin_ItemView();
                iv7.ItemLoad("系统服务器设置");
                iv7.DBaseControl.InnerDuiControl.Tag = "6";
                dSkinListBox1.Items.Add(iv7.DBaseControl.InnerDuiControl);
                dSkinLabel4.Size = new Size(200, 65);
                dSkinListBox1.Location = new Point(0, 197);
                dSkinListBox1.Size = new Size(200, 390);
            }
            else if (JCache.userdata[8].ToString().Equals("2"))
            {
                dSkinLabel3.Text = "您好，" + JCache.userdata[1] + "\n\n权限：一级管理员";
                dSkinLabel4.Size = new Size(200, 110);
                dSkinListBox1.Location = new Point(0, 242);
                dSkinListBox1.Size = new Size(200, 415);
            }
            re_1(false);
            dSkinDateTimePicker1.MaxDate = DateTime.Now;
            loading.Close();
        }
        private void dSkinTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dSkinTabControl1.SelectedIndex)
            {
                case 0:
                    if(dSkinGridList1.DataSource == null)
                       re_1(false);
                    break;
                case 1:
                    if (dSkinGridList3.DataSource == null)
                        re_2(false);
                    break;
                case 2:
                    if (dSkinGridList2.DataSource == null)
                        re_3();
                    break;
                case 3:
                    if (dSkinGridList4.DataSource == null)
                        re_4();
                    break;
                case 4:
                    if (dSkinGridList5.DataSource == null)
                        re_5();
                    break;
            }
        }

        public void re_1(Boolean isRe)
        {
            DataTable dt = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_tiku_manager, JCache.dbt_tiku_manager);
            if (dt != null)
                dSkinGridList1.DataSource = dt;
            if (isRe)
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void re_2(Boolean isRe)
        {
            DataTable dt = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_user,JCache.dbt_user);
            if (dt != null)
                dSkinGridList3.DataSource = dt;
            if (isRe)
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void re_3()
        {
            DataTable dt = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_exam_manager, JCache.dbt_exam_manager);
            if (dt != null)
                dSkinGridList2.DataSource = dt;
        }

        private void re_4()
        {
            //DataTable dt = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_user_score,JCache.dbt_user_score);
            //List<object> list = new List<object>();
            //if (dt != null)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        list.Add(new {
            //            序号 = i,
            //            姓名 = JCache.mysqlhelper.GetSomething("F2", String.Format("select F2 from {0}.{1} where F4='{2}'",
            //            JCache.databaseName, JCache.dbt_user, dt.Rows[i][2])),
            //            考试名称 = dt.Rows[i][3],
            //            分数 = dt.Rows[i][1],
            //            考试时间 = dt.Rows[i][4],
            //            答题时长 = dt.Rows[i][5],
            //            备注 = JCache.mysqlhelper.GetSomething("F7", String.Format("select F7 from {0}.{1} where F4='{2}'",
            //            JCache.databaseName, JCache.dbt_user, dt.Rows[i][2]))
            //        });
            //    }
            //}
            //dSkinGridList4.DataSource = list;
        }

        private void re_5()
        {
            DataTable dt1 = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_exam_qusetion, JCache.dbt_exam_qusetion);
            List<object> list = new List<object>();
            int index = 1;
            if (dt1 != null)
            {
                foreach (DataRow dr0 in dt1.Rows)
                {
                    DataTable dt2 = JCache.mysqlhelper.GetDataTable("select * from " + dr0[2], dr0[2].ToString());
                    foreach(DataRow dr in dt2.Rows)
                    {
                        DataRow _dr = JCache.mysqlhelper.GetDataTable(String.Format("select * from {0} where F1='{1}'",
                             dr0[2].ToString().Replace("exam_",""),dr[1]), dr0[2].ToString().Replace("exam_", "")).Rows[0];
                        float count = float.Parse(dr[4].ToString()) + float.Parse(dr[5].ToString()) +
                            float.Parse(dr[6].ToString()) + float.Parse(dr[7].ToString());
                        list.Add(new
                        {
                            序号 = index,
                            考试名称 = dr0[1],
                            题型 = _dr[1],
                            正确率 = float.Parse(((float.Parse(dr[4 + int.Parse(dr[3].ToString())].ToString()) / count) * 100).ToString("f2")),
                            答题次数 = (int)count,
                            详情 = GetDetails(dr, _dr[1].ToString()),
                            试题内容 = _dr[2],
                            答案 = _dr[3],
                            选项A = _dr[4],
                            选项B = _dr[5],
                            选项C = _dr[6],
                            选项D = _dr[7]
                        });
                        index++;
                    }
                }
            }
            dSkinGridList5.DataSource = list;
        }

        private String GetDetails(DataRow dr, String type)
        {
            var details = new StringBuilder();
            if (type.Equals("判断题"))
            {
                if (int.Parse(dr[4].ToString()) > 0)
                    details.Append("  正确" + dr[4] + "次");
                if (int.Parse(dr[5].ToString()) > 0)
                    details.Append("  错误" + dr[5] + "次");
            }
            else
            {
                if (int.Parse(dr[4].ToString()) > 0)
                    details.Append("  A选项" + dr[4] + "次");
                if (int.Parse(dr[5].ToString()) > 0)
                    details.Append("  B选项" + dr[5] + "次");
                if (int.Parse(dr[6].ToString()) > 0)
                    details.Append("  C选项" + dr[6] + "次");
                if (int.Parse(dr[7].ToString()) > 0)
                    details.Append("  D选项" + dr[7] + "次");
            }
            return details.ToString();
        }
        #region Click
        //查询考试成绩
        private void dSkinButton5_Click(object sender, EventArgs e)
        {
            DataTable dt = JCache.mysqlhelper.GetDataTable(String.Format("select * from {0} where F5='{1}'",
                JCache.dbt_user_score, dSkinDateTimePicker1.Value.ToString("yyyy年MM月dd日")),JCache.dbt_user_score);
            List<object> list = new List<object>();
            if (dt == null)
            {
                MessageBox.Show("不存在该天的数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new
                    {
                        序号 = i + 1,
                        姓名 = JCache.mysqlhelper.GetSomething("F2",
                        String.Format("select F2 from {0}.{1} where F4='{2}'", JCache.databaseName,
                        JCache.dbt_user, dt.Rows[i][2])),
                        考试名称 = dt.Rows[i][3],
                        分数 = int.Parse(dt.Rows[i][1].ToString()),
                        考试时间 = dt.Rows[i][4],
                        答题时长 = dt.Rows[i][5],
                        备注 = JCache.mysqlhelper.GetSomething("F7", String.Format("select F7 from {0}.{1} where F4='{2}'",
                        JCache.databaseName, JCache.dbt_user, dt.Rows[i][2]))
                    });
                }
            }
            count = list.Count;
            dSkinGridList4.DataSource = list;
        }
        //导出成绩
        private void dSkinButton6_Click(object sender, EventArgs e)
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*.xlsx|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                loading = new Loading();
                loading.Show();
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork2);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted2);
                work.RunWorkerAsync(this);
            }
        }

        void work_DoWork2(object sender, DoWorkEventArgs e)
        {
            ExcelHelper.ExportExcel(saveFileDialog.FileName + ".xlsx",dSkinGridList4.DataSource as List<object>);
        }
        void work_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Close();
            MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        //导出考试详情
        private void dSkinButton7_Click(object sender, EventArgs e)
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*.xlsx|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                loading = new Loading();
                loading.Show();
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork3);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted2);
                work.RunWorkerAsync(this);
            }
        }
        void work_DoWork3(object sender, DoWorkEventArgs e)
        {
            ExcelHelper.ExportExcel(saveFileDialog.FileName + ".xlsx", dSkinGridList5.DataSource as List<object>);
        }
        private void ListBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < (sender as DSkinListBox).Items.Count; i++)
            {

                if ((sender as DSkinListBox).Items[i].IsMouseEnter)
                {
                    (sender as DSkinListBox).Items[i].BackColor = Color.WhiteSmoke;
                    dSkinTabControl1.SelectedIndex = int.Parse((sender as DSkinListBox).Items[i].Tag + "");
                    (sender as DSkinListBox).Items[i].Text = "0";
                }
                else
                {
                    (sender as DSkinListBox).Items[i].BackColor = Color.Transparent;
                }
            }
        }
        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            String fileName = LoadExcelData();
            if (fileName != null)
            {
                new QustionBank_Details(this, fileName,
                    Path.GetFileNameWithoutExtension(fileName), 0).Show();
            }
        }
        private void dSkinButton4_Click(object sender, EventArgs e)
        {
            String fileName = LoadExcelData();
            if (fileName != null)
            {
                new Personnel_Details(this,fileName, 0).Show();
            }
        }
        private String LoadExcelData()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Excel文件|*.xlsx;*.xls";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return null;
        }
        private void dSkinGridList1_ItemDoubleClick(object sender, DSkinGridListMouseEventArgs e)
        {
            DataRow dataRow = (e.Item.RowData as DataRowView).Row;
            new QustionBank_Details(this, dataRow[7].ToString(), dataRow[1].ToString(), 1).Show();
        }
        private void dSkinButton3_Click(object sender, EventArgs e)
        {
            new ChooseQB(this).Show();
        }
        #endregion

#region Others Helper

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("确定要退出程序?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
        private void dSkinTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dSkinTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dSkinTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

#region  建立MySql数据库连接

        #endregion

#region 设置新考试
        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show();
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
        }
        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            if (dSkinTextBox7.Text.Length == 0 || dSkinTextBox6.Text.Length == 0 || dSkinTextBox5.Text.Length == 0 || dSkinTextBox4.Text.Length == 0 ||
                 dSkinTextBox1.Text.Length == 0 || dSkinTextBox3.Text.Length == 0 || dSkinTextBox2.Text.Length == 0)
            {
                MessageBox.Show("有数据为空值！请修改。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F5, F6, F7, F8, F9, F10, F11) values " +
                "('{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                JCache.databaseName,
                JCache.dbt_exam_manager,
                dSkinTextBox7.Text,
                dSkinTextBox6.Text,
                dSkinTextBox5.Text,
                dSkinTextBox1.Text,
                dSkinTextBox3.Text,
                dSkinTextBox2.Text,
                dSkinTextBox4.Text,
                QB_Setting_tbName,
                DateTime.Now.ToString("yyyy年MM月dd日HH:mm"),
                "否"));
        }
        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            re_3();
            loading.Close();
            MessageBox.Show("设置新考试成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }




        #endregion
    }
}
