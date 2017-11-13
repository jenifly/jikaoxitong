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
    public partial class QustionBank_Details : DSkinForm
    {
        private Admin admin;
        private String dtName;
        private String QBName;
        private int type;
        private DataTable dt;
        private Loading loading;
        private Boolean isNull = false;
        public QustionBank_Details(Admin admin, String dtName,String QBName, int type)
        {
            loading = new Loading();
            loading.Show();
            this.admin = admin;
            this.dtName = dtName;
            this.QBName = QBName;
            this.type = type;
            InitializeComponent();
        }

        private void QustionBank_Details_Load(object sender, EventArgs e)
        {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
            dSkinLabel2.Text = QBName;
            if (type == 0)
                dSkinButton1.Visible = true;
        }
        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            if(type == 0)
            {
                dt = ExcelHelper.ImportExcel(dtName,1);
                if(dt == null)
                {
                    MessageBox.Show("您选择的 Excel 题库文件格式不正确！", "提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    isNull = true;
                }
                   
            }
            else
            {
                dt = JCache.mysqlhelper.GetDataTable("select * from " + dtName, dtName);
            }
        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isNull)
            {
                loading.Close();
                Close();
            }
            dSkinGridList1.DataSource = dt;
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
            String tableName = "tiku_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            JCache.mysqlhelper.CreateTable(JCache.databaseName, tableName, String.Format("CREATE TABLE {0}.{1}(F1 INT NOT NULL AUTO_INCREMENT,F2 VARCHAR(20)," +
                "F3 VARCHAR(100),F4 VARCHAR(20),F5 VARCHAR(50),F6 VARCHAR(50),F7 VARCHAR(50),F8 VARCHAR(50),PRIMARY KEY (F1))", JCache.databaseName, tableName));
            JCache.mysqlhelper.GetExecute(String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F5, F6, F7, F8) values " +
               "('{2}','{3}','{4}','{5}','{6}','{7}','{8}')", JCache.databaseName, JCache.dbt_tiku_manager, QBName,
               JCache.mysqlhelper.InsertXlsQB(dt, tableName), "未使用", DateTime.Now.ToString("yyyy年MM月dd日HH:mm:ss"), 
               "admin", "无", tableName));
        }

        void work_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Close();
            admin.re_1(true);
            Close();
        }
    }
}
