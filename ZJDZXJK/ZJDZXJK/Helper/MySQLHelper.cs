using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using ZJDZXJK.Cache;
using System.Text;
using DSkin.Forms;
using System.Windows.Forms;

namespace ZJDZXJK.DBHelper
{
    public class MySQLHelper
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        MySqlConnection SQLConnect()
        {
            return new MySqlConnection(String.Format(@"Data Source={0};User ID={1};Password={2};pooling=false;CharSet=utf8",
             JCache.dbType, JCache.userId, JCache.password));
        }
        MySqlConnection SQLConnect(String databaseName)
        {
            return new MySqlConnection(String.Format(@"Data Source={0};User ID={1};Password={2};Database={3};pooling=false;CharSet=utf8",
             JCache.dbType, JCache.userId, JCache.password, databaseName));
        }
        /// <summary>
        /// 创建数据库
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 创建数据库成功，NOK 创建数据库失败
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        public String CreateDatabase(String databaseName)
        {
            if (CheckDB(databaseName, false).Equals("OK"))
                if (GetExecute(String.Format("CREATE DATABASE {0};", databaseName)) > 0)
                    return "创建数据库成功";
            return "创建数据库失败";
        }
        /// <summary>
        /// 删除数据库
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 删除数据库成功，NOK 删除数据库失败
        /// </summary>
        /// <param name="DatabaseName">数据库名称</param>
        /// <returns></returns>
        public string DeleteDatabase(string databaseName)
        {
            if (CheckDB(databaseName, true).Equals("OK"))
                if (GetExecute(String.Format("DROP DATABASE `{0}`", databaseName)) > 0)
                    return "删除数据库成功";
            return "删除数据库失败";
        }
        /// <summary>
        /// 创建表
        /// 返回值格式为 如：表创建成功，表创建失败
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">要创建的表的名字</param>
        /// <param name="sql">建表sql语句</param>
        /// <returns></returns>
        public String CreateTable(String databaseName, String tableName, String sql)
        {
            if (CheckDB(databaseName, true).Equals("OK") && CheckDBT(databaseName, tableName, false).Equals("OK"))
                if (GetExecute(sql) > 0)
                    return "创建数据表成功";
            return "创建数据表失败";
        }
        /// <summary>
        /// 删除表
        /// 返回值格式为 如：表删除成功，表删除失败
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">要创建的表的名字</param>
        /// <returns></returns>
        /// <summary>
        public string DeleteTable(string databaseName, string tableName)
        {
            if (CheckDB(databaseName, true).Equals("OK") && CheckDBT(databaseName, tableName, true).Equals("OK"))
                if (GetExecute(String.Format("DROP TABLE `{0}`.`{1}`;", databaseName, tableName)) > 0)
                    return "删除数据表成功";
            return "删除数据表失败";
        }
        /// 检查数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="isDelete">是否是删除</param>
        /// <returns></returns>
        private String CheckDB(String databaseName, Boolean isDelete)
        {
            Boolean isDbExists = false;
            DataSet ds = GetDataSet("show databases", "Show");
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0] == null)
            {
                if (DSkinMessageBox.Show("服务器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                return "查询MySQL失败";
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Database"].ToString().ToUpper() == databaseName.ToUpper())
                    {
                        if (isDelete)
                        {
                            isDbExists = true;
                            break;
                        }
                        else
                            return "数据库已经存在，请先删除数据库！";
                    }
                }
            }
            if (isDelete && !isDbExists)
            {
                return "数据库不存在";
            }
            return "OK";
        }
        /// <summary>
        /// 检查数据表
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="isDelete">是否是删除</param>
        /// <returns></returns>
        /// <summary>
        private String CheckDBT(String databaseName, String tableName, Boolean isDelete)
        {
            Boolean isDbExists = false;
            DataSet ds = GetDataSet(String.Format("show tables from {0};", databaseName), "tables");
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0] == null)
            {
                return "查询MySQL失败";
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString().ToUpper() == tableName.ToUpper())
                    {
                        if (isDelete)
                        {
                            isDbExists = true;
                            break;
                        }
                        else
                            return "该数据表已经存在，请先删除该数据表！";
                    }
                }
            }
            if (isDelete && !isDbExists)
            {
                return "该数据表不存在";
            }
            return "OK";
        }
        /// 查询数据库
        /// </summary>
        /// <param name="sqltxt">查询SQL语句</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sqltxt, string tableName)
        {
            try
            {
                using (MySqlConnection conn = SQLConnect())
                {
                    if (conn != null)
                        conn.Open();
                    else
                        return null;
                    MySqlDataAdapter sqlData = new MySqlDataAdapter(sqltxt, conn);
                    DataSet ds = new DataSet();
                    sqlData.Fill(ds, tableName);
                    sqlData.Dispose();
                    return ds;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable GetDataTable(string sqltxt, string tableName)
        {
            try
            {
                using (MySqlConnection conn = SQLConnect(JCache.databaseName))
                {
                    if (conn != null)
                        conn.Open();
                    else
                        return null;
                    MySqlDataAdapter sqlData = new MySqlDataAdapter(sqltxt, conn);
                    DataSet ds = new DataSet();
                    sqlData.Fill(ds, tableName);
                    sqlData.Dispose();
                    return ds.Tables[0].Rows.Count==0?null: ds.Tables[0];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// DataTable写入特定MySQL题库数据库
        /// </summary>
        /// <param name="DataTable">数据表</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableName">数据表名</param>
        /// <returns></returns>
        /// <summary>
        public int InsertXlsQB(DataTable dt, String tableName)
        {
            int count = 0;
            string[] strs = new string[7];
            var sqlBuilder = new StringBuilder();
            string sqlHeader = String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F5, F6, F7, F8) values ",
                JCache.databaseName, tableName);
            sqlBuilder.Append(sqlHeader);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Length > 0)
                {
                    count++;
                    for (int n = 0; n < 7; n++)
                    {
                        strs[n] = dt.Rows[i][n + 1].ToString();
                    }
                    sqlBuilder.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                        strs[0], strs[1], strs[2], strs[3], strs[4], strs[5], strs[6]);
                    if (sqlBuilder.Length >= 1024 * 1024 * 1024)
                    {
                        GetExecute(sqlBuilder.Remove(sqlBuilder.Length - 1, 1).ToString());
                        sqlBuilder.Clear();
                        sqlBuilder.Append(sqlHeader);
                    }
                }
            }
            GetExecute(sqlBuilder.Remove(sqlBuilder.Length - 1, 1).ToString());
            return count;
        }
        public int InsertXlsUser(DataTable dt,String tableName)
        {
            int count = 0;
            string[] strs = new string[6];
            var sqlBuilder = new StringBuilder();
            string sqlHeader = String.Format("INSERT INTO {0}.{1} (F2, F3, F4, F5, F6, F7) values ",
                JCache.databaseName, tableName);
            sqlBuilder.Append(sqlHeader);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                count++;
                if (dt.Rows[i][0].ToString().Length > 0)
                {
                    for (int n = 0; n < 6; n++)
                    {
                        strs[n] = dt.Rows[i][n + 1].ToString();
                    }
                    sqlBuilder.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}'),",
                        strs[0], strs[1], strs[2], strs[3], strs[4], strs[5]);
                    if (sqlBuilder.Length >= 512 * 1024 * 1024)
                    {
                        GetExecute(sqlBuilder.Remove(sqlBuilder.Length - 1, 1).ToString());
                        sqlBuilder.Clear();
                        sqlBuilder.Append(sqlHeader);
                    }
                }
            }
            GetExecute(sqlBuilder.Remove(sqlBuilder.Length - 1, 1).ToString());
            return count;
        }
        /// <summary>
        /// 更新数据库数据
        /// </summary>
        /// <param name="sqltxt">SQL语句</param>
        /// <returns></returns>
        public int GetExecute(string sqltxt)
        {
            try
            {
                using (MySqlConnection conn = SQLConnect())
                {
                    if (conn != null)
                        conn.Open();
                    else
                        return -2;

                    MySqlCommand comm = new MySqlCommand(sqltxt, conn);
                    int res = comm.ExecuteNonQuery();
                    comm.Dispose();
                    return res;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateDataSet(DataSet dsForUpdate, string tableName, out string msg)
        {
            try
            {
                using (MySqlConnection conn = SQLConnect())
                {
                    msg = "OK";
                    using (MySqlDataAdapter SDA = new MySqlDataAdapter(String.Format("select * from {0}", tableName), conn))
                    {
                        ////这条语句必须存在
                        MySqlCommandBuilder sqlBulider = new MySqlCommandBuilder(SDA);
                        int k = SDA.Update(dsForUpdate, tableName);
                        //dsForUpdate.AcceptChanges();
                        return k;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return -1;
            }
        }

        public int CheckSomething(String sqltxt)
        {
            using (MySqlConnection conn = SQLConnect(JCache.databaseName))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sqltxt, conn);
                return comm.ExecuteScalar() == null?-1:1;
            }
        }

        public String GetSomething(String st, String sqltxt)
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
            return null;
        }
    }
}