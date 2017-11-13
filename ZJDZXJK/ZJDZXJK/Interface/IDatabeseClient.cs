using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DatabaseClient
{
    /// <summary>
    /// 与数据库交互接口
    /// 内部使用
    /// </summary>
    interface IDatabeseClient
    {

        DataSet GetDataSet(string dbType, string sqltxt, string tableName);

        int GetExecute(string dbType, string sqltxt);

        int UpdateDataSet(string dbType, DataSet dsForUpdate, string tableName, out string msg);
    }
}
