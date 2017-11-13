using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ZJDZXJK.Helper
{
    public class ExcelHelper
    {
        public static DataTable ImportExcel(string path)
        {
            DataSet ds = new DataSet();
            string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=No;IMEX=1'", path);
            using (var oledbConn = new OleDbConnection(strConn))
            {
                oledbConn.Open();
                var sheetName = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new[] { null, null, null, "Table" });
                var sheet = new string[sheetName.Rows.Count];
                for (int i = 0, j = sheetName.Rows.Count; i < j; i++)
                {
                    sheet[i] = sheetName.Rows[i]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter(string.Format("select * from [{0}]", sheet[i]), oledbConn).Fill(ds);
                }
            }
            return ds.Tables[0];
        }

        public static DataTable ImportExcel(string path, int type)
        {
            DataTable dt = ImportExcel(path);
            DataRow dr = dt.Rows[0];
            if (type == 0)
            {
                if (dr[0].ToString().Equals("序号") && dr[1].ToString().Equals("姓名") && 
                    dr[2].ToString().Equals("性别") && dr[6].ToString().Equals("备注"))
                {
                    dt.Rows[0].Delete();
                    return dt;
                }
            }
            if(type == 1)
            {
                if (dr[0].ToString().Equals("题号") && dr[1].ToString().Equals("题型") && 
                    dr[2].ToString().Equals("试题内容") && dr[7].ToString().Equals("选项D"))
                {
                    dt.Rows[0].Delete();
                    return dt;
                }                    
            }
            return null;
        }
        public static void ExportExcel(string fileName, List<object> list)
        {
            string FileName = fileName;
            long totalCount = list.Count;
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            int ColumnsCount = list[0].ToString().Split(',').Length;
            for (int r = 0; r <= list.Count; r++)
            {
                string[] RowData = list[r-1<1?0:r-1].ToString().Replace("{", "").Replace("}", "").Split(',');
                for (int i = 0; i < ColumnsCount; i++)
                {
                    if (r == 0)
                        worksheet.Cells[1, i + 1] = RowData[i].Split('=')[0];
                    else
                        worksheet.Cells[r + 1, i + 1] = RowData[i].Split('=').Length>1? RowData[i].Split('=')[1] : "";
                }
            }
            workbook.Saved = true;
            workbook.SaveCopyAs(FileName);
        }
        public static void ExportExcel(string fileName, DataTable dt)
        {
            string FileName = fileName;
            long totalCount = dt.Rows.Count;
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i];
                }
            }
            workbook.Saved = true;
            workbook.SaveCopyAs(FileName);
        }
    }
}