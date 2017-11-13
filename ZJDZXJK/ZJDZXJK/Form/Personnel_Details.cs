using DSkin.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZJDZXJK.Cache;
using ZJDZXJK.DBHelper;
using ZJDZXJK.Helper;

namespace ZJDZXJK 
{
    public partial class Personnel_Details : DSkinForm
    {
        private Admin admin;
        private String dtName;
        private int type;
        private DataTable dt;
        private Loading loading;
        private Boolean isNull = false;
        public Personnel_Details(Admin admin,String dtName, int type)
        {
            loading = new Loading();
            loading.Show();
            this.admin = admin;
            this.dtName = dtName;
            this.type = type;
            InitializeComponent();
        }

        private void QustionBank_Details_Load(object sender, EventArgs e)
        {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
            if (type == 0)
                dSkinButton1.Visible = true;
        }
        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            if(type == 0)
            {
                dt = ExcelHelper.ImportExcel(dtName,0);
                if(dt == null)
                {
                    MessageBox.Show("您选择的 Excel 题库文件格式不正确！", "提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    isNull = true;
                }
            }
            else
                dt = JCache.mysqlhelper.GetDataTable("select * from " + dtName, dtName);
            
        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isNull)
            {
                loading.Close();
                Close();
            }
            dSkinGridList3.DataSource = dt;
            loading.Close();
        }
        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show();
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork1);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted1);
            work.RunWorkerAsync(this);
        }
        void work_DoWork1(object sender, DoWorkEventArgs e)
        {
            JCache.mysqlhelper.InsertXlsUser(dt, JCache.dbt_user);
        }

        void work_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Close();
            admin.re_2(true);
            Close();
        }
    }
}
