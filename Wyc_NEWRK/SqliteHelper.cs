using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;
namespace Wyc_NEWRK
{
    class SqliteHelper
    {
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public  int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 链接sqlite
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetSQLiteConnection()
        {
            string path = Directory.GetCurrentDirectory();
            string dbpath = path + @"\SpiderResult.db3";
            return new SQLiteConnection("Data Source=" + dbpath);
        }
        /// <summary>
        /// 返回一个dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet SqliteQuery(string sql)
        {
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(sql, connection);
                    command.Fill(ds, "ds");
                }
                catch (SQLiteException ex)
                {

                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        /// <summary>
        /// 返回datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetQuery(string sql)
        {
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                    sda.Fill(ds);
                    return ds.Tables[0];
                }
                catch (SQLiteException ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 看foreach你懂的
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable gettablequert( List<String> sqllist)
        {
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    foreach (string item in sqllist)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(item, connection);
                        SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                        sda.Fill(ds);

                    }
                    return ds.Tables[0];
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string SQLString)
        {
            using (SQLiteConnection connection =GetSQLiteConnection() )
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
    }
}
