using DSkin.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ZJDZXJK.Cache;
using ZJDZXJK.Helper;

namespace ZJDZXJK 
{
    public partial class ChooseQB : DSkinForm
    {
        private Admin admin;
        private Loading loading = new Loading();
        public ChooseQB(Admin admin)
        {
            loading.Show();
            this.admin = admin;
            InitializeComponent();
        }
        private void ChooseQB_Load(object sender, EventArgs e)
        {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
        }
        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            dSkinGridList1.DataSource = JCache.mysqlhelper.GetDataTable(
                String.Format("select * from {0}", JCache.dbt_tiku_manager), JCache.dbt_tiku_manager);
        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loading.Close();
        }

        private void dSkinGridList1_ItemDoubleClick(object sender, DSkin.Controls.DSkinGridListMouseEventArgs e)
        {
            DataRow dataRow = (e.Item.RowData as DataRowView).Row;
            admin.dSkinTextBox4.Text = dataRow[1].ToString();
            admin.QB_Setting_tbName = dataRow[7].ToString();
            Close();
        }
    }
}
