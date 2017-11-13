using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZJDZXJK.DBHelper;

namespace ZJDZXJK.Cache
{
    public class JCache
    {
        public static bool AdminModer = false;
        public static String current_versions;
        public static DataRow userdata;
        public static String dbType = "localhost";
        public static String userId = "root";
        public static String password = "root";
        public static String databaseName = "zjd_zxjk";
        public static String dbt_tiku_manager = "tiku_manager";
        public static String dbt_exam_manager = "exam_manager";
        public static String dbt_exam_qusetion = "exam_qusetion";
        public static String dbt_user = "zjd_user";
        public static String dbt_user_score = "zjd_user_score"; 
        public static String dbt_versions = "zjd_versions"; 
        public static MySQLHelper mysqlhelper;
    }
}
