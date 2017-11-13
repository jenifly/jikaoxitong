using DSkin.DirectUI;
using DSkin.Forms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZJDZXJK.Cache;
using ZJDZXJK.DBHelper;

namespace ZJDZXJK 
{
    public partial class Check_QustionBank : DSkinForm
    {
        private DataRow dataRow;
        private Boolean isJoinExam = false;
        public Check_QustionBank()
        {
            InitializeComponent();
        }


        #region Others Helper

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isJoinExam)
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

        private void Check_QustionBank_Load(object sender, EventArgs e)
        {
            this.EnableAnimation = false;
            DataTable dt = JCache.mysqlhelper.GetDataTable("select * from " + JCache.dbt_exam_manager,JCache.dbt_exam_manager);
            if (dt != null && dt.Rows.Count > 0)
                dSkinGridList1.DataSource = dt;
        }
        private void dSkinGridList1_ItemClick(object sender, DSkin.Controls.DSkinGridListMouseEventArgs e)
        {
            dataRow = (e.Item.RowData as DataRowView).Row;
            dSkinLabel3.Text = "考试名称： " + dataRow[1];
            dSkinLabel7.Text = dataRow[4] + "分钟";
            dSkinLabel8.Text = dataRow[3] + "分";
            dSkinLabel10.Text = dataRow[2] + "";
            dSkinLabel6.Text = "考试地点： " + dataRow[5];
            dSkinLabel5.Text = "组考单位： " + dataRow[6];
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if(dSkinLabel7.Text.Length == 0)
            {
                DSkinMessageBox.Show("请先选择考试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            new Main(dataRow[1].ToString(), dataRow[8].ToString(),int.Parse(dataRow[2].ToString()), int.Parse(dataRow[4].ToString())).Show();
            isJoinExam = true;
            Close();
        }
    }
}
