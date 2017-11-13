using ZJDZXJK.Cache;
using DSkin.Forms;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using DSkin.Controls;
using System.Drawing;
using DSkin.DirectUI;
using System.Text;

namespace ZJDZXJK
{
    public partial class Main : DSkinForm
    {
        private Loading loading;
        private Timer timer = new Timer();
        private int time_Min, qustiuonCount, shengyu , _time_min;
        private DataTable dt;
        private String dtName, examName;
        private List<DataRow> dataRows = new List<DataRow>();
        private List<int> bunengqueding = new List<int>();
        private List<String> judge = new List<string>();
        private int cruuent = 0, last = 0, type1 = -1, score = 0;
        private Dictionary<int, int> dictionary = new Dictionary<int, int>();
        private bool isSubmit = false;

        public Main(String examName, String dtName, int qustiuonCount,int time_Min)
        {
            this.time_Min = _time_min = time_Min;
            this.examName = examName;
            this.qustiuonCount = qustiuonCount;
            shengyu = qustiuonCount;
            this.dtName = dtName;
            InitializeComponent();
        }

        #region 做题
        private void Login_Load(object sender, EventArgs e)
        {
            dSkinLabel1.Text = "你好，" + JCache.userdata[1] + "，欢迎使用株机段2017年新工入路岗前培训答题系统";
            loading = new Loading();
            loading.Show();
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
        }

        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            dt = JCache.mysqlhelper.GetDataTable("select * from " + dtName, dtName);
            int i = 0;
            foreach(int index in GetRandom(dt.Rows.Count))
            {
                if (type1 == -1 && dt.Rows[index][1].ToString().Equals("判断题"))
                    type1 = i;
                dataRows.Add(dt.Rows[index]);
                i++;
            }
        }
        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dSkinLabel5.Text = time_Min.ToString("00");
            dSkinLabel11.Text = qustiuonCount.ToString();
            dSkinLabel16.Text = "试题总览（" + qustiuonCount + "题）";
            time_Min = time_Min * 60;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
            LoadQid();
            LoadQ();
            loading.Close();
            timer.Start();
        }
        private void LoadQid()
        {
            for (int i = 0; i < type1; i++)
            {
                DuiButton btn = new DuiButton();
                btn.AdaptImage = false;
                btn.BaseColor = Color.SteelBlue;
                btn.Cursor = Cursors.Hand;
                btn.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btn.ForeColor = Color.White;
                btn.Radius = 32;
                btn.ShowButtonBorder = false;
                btn.Size = new Size(32, 32);
                btn.Text = (i + 1).ToString();
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextInnerPadding = new Padding(0, 1, 2, 0);
                btn.IsPureColor = true;
                dSkinListBox1.Items.Add(btn);
            }
            for (int i = type1; i < qustiuonCount; i++)
            {
                DuiButton btn = new DuiButton();
                btn.AdaptImage = false;
                btn.BaseColor = Color.SteelBlue;
                btn.Cursor = Cursors.Hand;
                btn.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btn.ForeColor = Color.White;
                btn.Radius = 32;
                btn.ShowButtonBorder = false;
                btn.Size = new Size(32, 32);
                btn.Text = (i + 1).ToString();
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextInnerPadding = new Padding(0, 1, 2, 0);
                btn.IsPureColor = true;
                dSkinListBox2.Items.Add(btn);
            }
            dSkinListBox1.Top = dSkinLabel15.Top + dSkinLabel15.Height + 20;
            dSkinListBox1.Height = ((type1 % 18)==0? type1 / 18: type1 / 18 + 1) * 42;
            dSkinLabel17.Top = dSkinListBox1.Top + dSkinListBox1.Height + 20;
            dSkinListBox2.Top = dSkinLabel17.Top + dSkinLabel17.Height + 20;
            dSkinListBox2.Height = ((qustiuonCount - type1) % 18 == 0 ? 
                (qustiuonCount - type1) /18:(qustiuonCount - type1) / 18 + 1) * 42 + 20;
        }

        private void setBackColor(int index, Color color)
        {
            if (index < type1)
                (dSkinListBox1.Items[index] as DuiButton).BaseColor = color;
            else
                (dSkinListBox2.Items[index - type1] as DuiButton).BaseColor = color;                
        }

        private Color getBackColor(int index)
        {
            if (index < type1)
               return (dSkinListBox1.Items[index] as DuiButton).BaseColor;
            else
                return (dSkinListBox2.Items[index - type1] as DuiButton).BaseColor;
        }

        private void setBBackColor(int index, Color color)
        {
            if (index < type1)
                (dSkinListBox1.Items[index] as DuiButton).BackColor = color;
            else
                (dSkinListBox2.Items[index - type1] as DuiButton).BackColor = color;
        }
        private void LoadQ()
        {
            setBBackColor(last, Color.Transparent);
            setBBackColor(cruuent, getBackColor(cruuent));
            last = cruuent;
            if (bunengqueding.Contains(cruuent))
                dSkinButton4.Text = "确定该题";
            else
                dSkinButton4.Text = "不能确定";
            DataRow dataRow = dataRows[cruuent];
            dSkinLabel14.Text = dataRow[1].ToString();
              String str = (cruuent + 1) + "、" + dataRow[2].ToString();
            Graphics g = dSkinLabel13.CreateGraphics();
            int rowIdx = 0;
            int RowNum = 1;
            int textHeight = (int)g.MeasureString("我", dSkinLabel13.Font).Width;
            for (int i = 0; i < str.Length; i++)
            {
                float strWidth = g.MeasureString(str.Substring(rowIdx, i - rowIdx + 1), dSkinLabel13.Font).Width;
                if (strWidth > dSkinLabel13.Width)
                {
                    RowNum++;
                    rowIdx = i + 1;
                }
            }
            dSkinLabel13.Height = RowNum * textHeight;
            dSkinLabel13.Text = str;
            dSkinListBox3.Top = dSkinLabel13.Top + dSkinLabel13.Height + 10;
            dSkinListBox3.Height = 0;
            dSkinListBox3.Items.Clear();
            if (cruuent >= type1)
                dSkinListBox3.Height = RB(new String[] { "正确", "错误" });
            if (cruuent < type1)
            {
                int count = 0;
                if (dataRow[4].ToString().Length > 0)
                    count = 1;
                if (dataRow[5].ToString().Length > 0)
                    count = 2;
                if (dataRow[6].ToString().Length > 0)
                    count = 3;
                if (dataRow[7].ToString().Length > 0)
                    count = 4;
                switch (count)
                {
                    case 1:
                        dSkinListBox3.Height = RB(new String[] {"A、" + dataRow[4]});
                        break;
                    case 2:
                        dSkinListBox3.Height = RB(new String[] { "A、" + dataRow[4],
                            "B、" + dataRow[5] });
                        break;
                    case 3:
                        dSkinListBox3.Height = RB(new String[] { "A、" + dataRow[4],
                            "B、" + dataRow[5], "C、" + dataRow[6] });
                        break;
                    case 4:
                        dSkinListBox3.Height = RB(new String[] { "A、" + dataRow[4],
                            "B、" + dataRow[5], "C、" + dataRow[6],  "D、" + dataRow[7] });
                        break;
                }
            }
        }

        private int RB(String [] strs)
        {
            Graphics g = dSkinLabel13.CreateGraphics();
            int lbheight = 0;
            for (int n = 0; n< strs.Length; n++)
            {
                String str = strs[n];
                int rowIdx = 0;
                DuiRadioButton duiRadioButton = new DuiRadioButton();
                duiRadioButton.CheckAlign = ContentAlignment.MiddleLeft;
                if (isSubmit)
                {
                    duiRadioButton.Enabled = false;
                    if (judge[cruuent].ToString().Equals("-=-"))
                        dSkinLabel19.Text = "";
                    else
                        dSkinLabel19.Text = "该题的答案为：" + judge[cruuent];
                }
                duiRadioButton.CheckFlagColorDisabled = Color.DarkCyan;
                duiRadioButton.CheckFlagColor = Color.DarkCyan;
                duiRadioButton.CheckRectBackColorDisabled = Color.Transparent;
                duiRadioButton.CheckRectColor = Color.DarkCyan;
                duiRadioButton.CheckRectColorDisabled = Color.DarkCyan;
                duiRadioButton.TextColorDisabled = ForeColor;
                duiRadioButton.CheckRectWidth = 14;
                duiRadioButton.Cursor = Cursors.Hand;
                duiRadioButton.Font = new Font("微软雅黑", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                duiRadioButton.ForeColor = SystemColors.ControlText;
                duiRadioButton.AutoSize = true;
                duiRadioButton.Margin = new Padding(0,3,0,3);
                StringBuilder sb = new StringBuilder(str);
                for (int i = 0; i < str.Length; i++)
                {
                    float strWidth = g.MeasureString(str.Substring(rowIdx, i - rowIdx + 1),
                        duiRadioButton.Font).Width;
                    if (strWidth > (dSkinListBox3.Width - 100))
                    {
                        rowIdx = i + 1;
                        sb.Insert(i, "\r\n      ");
                    }
                }
                duiRadioButton.Text = sb.ToString();
                lbheight += duiRadioButton.Height;
                duiRadioButton.TextOffset = new Point(4, 1);
                dSkinListBox3.Items.Add(duiRadioButton);
            }
            if (dictionary.ContainsKey(cruuent))
            {
                (dSkinListBox3.Items[dictionary[cruuent]] as DuiRadioButton).Checked = true;
            }
            return lbheight + strs.Length * 6 + 20;
        }
        private List<int> GetRandom(int dbCount)
        {
            List<int> list = new List<int>();
            Random rm = new Random();
            for (int i = 0; list.Count < qustiuonCount; i++)
            {
                int value = rm.Next(dbCount);
                if (!list.Contains(value))
                    list.Add(value);
            }
            list.Sort();
            return list;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            time_Min--;
            if (time_Min < 0)
            {
                timer.Stop();
                Submit();
                DSkinMessageBox.Show("考试时间结束，已自动交卷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int min = (time_Min / 60);
            dSkinLabel5.Text = min.ToString("00");
            dSkinLabel7.Text = (time_Min - min * 60).ToString("00");
        }

        private void dSkinButton3_Click(object sender, EventArgs e)
        {
            if (cruuent < qustiuonCount -1)
            {
                cruuent++;
                LoadQ();
            }
        }
        //答案
        private void dSkinListBox3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dictionary.ContainsKey(cruuent) || bunengqueding.Contains(cruuent))
            {
                dictionary[cruuent] = e.Index;
            }
            else
            {
                setBackColor(cruuent, Color.Green);
                setBBackColor(cruuent, Color.Green);
                dictionary.Add(cruuent, e.Index);
                dSkinLabel11.Text = (qustiuonCount - dictionary.Count).ToString();
            }
            if(dSkinCheckBox1.Checked == true)
            {
                if (cruuent < qustiuonCount - 1)
                {
                    cruuent++;
                    LoadQ();
                }
            }
        }

        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            if (cruuent > 0)
            {
                cruuent--;
                LoadQ();
            }
        }
        //不确定
        private void dSkinButton4_Click(object sender, EventArgs e)
        {
            Color color;
            if (dSkinButton4.Text.Equals("不能确定"))
            {
                dSkinButton4.Text = "确定该题";
                bunengqueding.Add(cruuent);
                color = Color.Goldenrod;
            }
            else
            {
                dSkinButton4.Text = "不能确定";
                bunengqueding.Remove(cruuent);
                if (dictionary.ContainsKey(cruuent))
                    color = Color.Green;
                else
                    color = Color.SteelBlue;
            }
            dSkinLabel12.Text = bunengqueding.Count.ToString();
            setBackColor(cruuent, color);
            setBBackColor(cruuent, color);
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            Submit();
        }

        //单选
        private void dSkinListBox1_ItemClick(object sender, ItemClickEventArgs e)
        {
            cruuent = e.Index;
            LoadQ();
        }
        //判断
        private void dSkinListBox2_ItemClick(object sender, ItemClickEventArgs e)
        {
            cruuent = e.Index + type1;
            LoadQ();
        }
        #endregion

        #region 交卷
        private void Submit()
        {
            dSkinButton1.Enabled = false;
            dSkinButton4.Enabled = false;
            if (isSubmit == false)
            {
                isSubmit = true;
                loading = new Loading();
                loading.Show();
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork1);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted1);
                work.RunWorkerAsync(this);
            }
        }

        void work_DoWork1(object sender, DoWorkEventArgs e)
        {
            JCache.mysqlhelper.CreateTable(JCache.databaseName, "exam_" + dtName, String.Format("CREATE TABLE {0}.{1}" +
              "(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(10), F3 VARCHAR(50),F4 int,F5 int DEFAULT '0',F6 int DEFAULT '0'," +
              "F7 int DEFAULT '0',F8 int DEFAULT '0',PRIMARY KEY (F1))", JCache.databaseName, "exam_" + dtName));
            if (JCache.mysqlhelper.CheckSomething(String.Format("select * from {0}.{1} where F2='{2}'",
               JCache.databaseName, JCache.dbt_exam_qusetion, examName)) < 0)
                JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F2, F3) values ('{2}','{3}')",
               JCache.databaseName, JCache.dbt_exam_qusetion, examName, "exam_" + dtName));
            
            int answer = -1;
            for(int i = 0; i < qustiuonCount; i++)
            {
                DataRow dr = dataRows[i];
                String Janswer = dr[3].ToString();
                if (i < type1)
                {
                    answer = getAnswer(Janswer, 0);
                    if (dictionary.ContainsKey(i))
                    {
                        SQLH(dr[0].ToString(), answer, dictionary[i]);
                        if (dictionary[i] != answer)
                        {
                            ShowJudge(dSkinListBox1.Items[i] as DuiButton,true);
                            judge.Add(Janswer);
                        }
                        else
                        {
                            judge.Add("-=-");
                            score++;
                        }
                    }
                    else
                    {
                        ShowJudge(dSkinListBox1.Items[i] as DuiButton,false);
                        judge.Add(Janswer);
                    }
                }
                if (i >= type1)// && i < qustiuonCount)
                {
                    answer = getAnswer(Janswer, 1);
                    if (dictionary.ContainsKey(i))
                    {
                        SQLH(dr[0].ToString(), answer, dictionary[i]);
                        if (dictionary[i] != answer)
                        {
                            ShowJudge(dSkinListBox2.Items[i - type1] as DuiButton,true);
                            judge.Add(Janswer);
                        }
                        else
                        {
                            judge.Add("-=-");
                            score++;
                        }
                    }
                    else
                    {
                        ShowJudge(dSkinListBox2.Items[i - type1] as DuiButton,false);
                        judge.Add(Janswer);
                    }
                }
            }
            if (JCache.AdminModer)
            {
                JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F5, F6) values " +
                    "('{2}','{3}','{4}','{5}','{6}')",
                  JCache.databaseName, JCache.dbt_user_score, score, JCache.userdata[3].ToString(),
                  examName, DateTime.Now.ToString("yyyy年MM月dd日"), GetTime()));
                JCache.mysqlhelper.GetExecute(String.Format("update {0}.{1} set F11='{2}' where F4='{3}'",
                    JCache.databaseName, JCache.dbt_user, DateTime.Now.ToString("yyyy_MM_dd"), JCache.userdata[3].ToString()));
            }
            setBBackColor(cruuent, getBackColor(cruuent));
        }


        private void SQLH(String id,int correct, int answer)
        {
            if (JCache.AdminModer)
                if (JCache.mysqlhelper.CheckSomething(String.Format("select * from {0}.{1} where F2='{2}'",
                    JCache.databaseName, "exam_" + dtName, id)) > 0)
                {
                    JCache.mysqlhelper.GetExecute(String.Format("update {0}.{1} set F{2}=F{2}+1 where F2={3}",
                    JCache.databaseName, "exam_" + dtName, answer + 5, id));
                }
                else
                {
                    JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F{2}) values ('{3}','{4}','{5}','{6}')",
                   JCache.databaseName, "exam_" + dtName, answer + 5, id, dtName, correct, 1));
                }
        }
        private void ShowJudge(DuiButton btn,bool type)
        {
            if(type)
                btn.BaseColor = Color.FromArgb(192, 64, 0);
            else
                btn.BaseColor = Color.BurlyWood;
        }
        private int getAnswer(String answer, int type)
        {
            if(type == 0)
            {
                switch (answer)
                {
                    case "A":
                        return 0;
                    case "B":
                        return 1;
                    case "C":
                        return 2;
                    case "D":
                        return 3;
                }
            }
            if(type == 1)
            {
                switch (answer)
                {
                    case "正确":
                        return 0;
                    case "错误":
                        return 1;
                }
            }
            return -1;
        }

        void work_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            foreach(DuiRadioButton rbtn in dSkinListBox3.Items)
            {
                rbtn.Enabled = false;
            }
            dSkinLabel9.Visible = false;
            dSkinLabel10.Visible = false;
            dSkinLabel11.Visible = false;
            dSkinLabel12.Visible = false;
            dSkinLabel20.Text = "得分：";
            dSkinLabel21.Text = score.ToString();
            loading.Close();
            DSkinMessageBox.Show("交卷成功！你的得分为：" + score, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private String GetTime()
        {
            int _min = int.Parse(dSkinLabel5.Text);
            int seconde = int.Parse(dSkinLabel7.Text);
            if(_min == 0 && seconde == 0)
            {
                return _time_min + "分";
            }
            return (_time_min - _min - 1).ToString("00") + "分" + (60 - seconde).ToString("00") + "秒";
        }
        #endregion

        #region Others Helper

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (DSkinMessageBox.Show("确定要退出程序?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }


        private void dSkinPanel5_MouseEnter(object sender, EventArgs e)
        {
            if (dSkinPanel5.Focused == false)
                dSkinPanel5.Focus();
        }

        private void dSkinPanel4_MouseEnter(object sender, EventArgs e)
        {
            if (dSkinPanel4.Focused == false)
                dSkinPanel4.Focus();
        }

        private void dSkinListBox1_MouseEnter(object sender, EventArgs e)
        {
            if (dSkinPanel5.Focused == false)
                dSkinPanel5.Focus();
        }

        private void dSkinListBox2_MouseEnter(object sender, EventArgs e)
        {
            if (dSkinPanel5.Focused == false)
                dSkinPanel5.Focus();
        }
        #endregion


    }
}
