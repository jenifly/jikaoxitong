using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DatabaseClient.DBHelper;
using System.Data;

namespace DatabaseClient
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDatabaseHelper
    {
        /// <summary>
        /// 创建数据库
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 创建数据库成功，NOK 创建数据库失败
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="databaseCollation">数据库编码格式</param>
        /// <returns></returns>
        string CreateDatabase(string databaseName, DatabaseCollation databaseCollation);

        /// <summary>
        /// 删除数据库
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 删除数据库成功，NOK 删除数据库失败
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        string DeleteDatabase(string databaseName);

        /// <summary>
        /// 创建表
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 表创建成功，NOK 表创建失败
        /// </summary>
        /// <param name="newTableInfo">要创建的表的信息</param>
        /// <returns></returns>
        string CreateTable(NewTableInfo newTableInfo);

        /// <summary>
        /// 删除表
        /// 返回值格式为 OK/NOK+空格+错误信息
        /// 如：OK 表删除成功，NOK 表删除失败
        /// </summary>
        /// <param name="DatabaseName">要删除的表所在数据库名</param>
        /// <param name="TableName">要删除的表名</param>
        /// <returns></returns>
        string DeleteTable(string DatabaseName,string TableName);

        /// <summary>
        /// SQL查询功能
        /// </summary>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="TableName">DataSet集合的表名</param>
        /// <returns></returns>
        DataSet GetDataSet(string SelectSQL, string TableName);

        /// <summary>
        /// SQL提交数据库更改
        /// 对表的增删改
        /// </summary>
        /// <param name="SqlString">修改语句</param>
        /// <returns></returns>
        int GetExcute(string SqlString);
    }
}
