using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Diagnostics;
using System.IO;
namespace Wyc_NEWRK
{
    public class wznr_Servise
    {
        private SqlConnection conn;
        private SqlConnection conn2005;
        private SqlConnection zxconn;
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        private DataSet ds;
        public SqlConnection Getconn()
        {
            //string path = Directory.GetCurrentDirectory()+@"\app.config";
            //string strconn = ConfigurationManager.OpenExeConfiguration(path).ConnectionStrings["connectionstring"].ToString();
            //string Strconn = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;//ConfigurationManager.AppSettings["connectionstring"].ToString();
            string Strconn = ConfigurationManager.ConnectionStrings["TFlicai"].ConnectionString;
            SqlConnection conn = new SqlConnection(Strconn);
            return conn;
        }

        
        public SqlConnection Getconn1()
        {
            string Strconn2005 = ConfigurationManager.ConnectionStrings["connectionstring1"].ConnectionString;//ConfigurationManager.AppSettings["connectionstring"].ToString();
            SqlConnection conn2005 = new SqlConnection(Strconn2005);
            return conn2005;
        }
        public SqlConnection Getconn2()
        {
            string zxconn = ConfigurationManager.ConnectionStrings["zxconnectionstring"].ConnectionString;//ConfigurationManager.AppSettings["connectionstring"].ToString();
            SqlConnection zxconn2005 = new SqlConnection(zxconn);
            return zxconn2005;
        }
        //获取oldb链接
        private  OleDbConnection con;
        public  OleDbConnection Con
        {
            get
            {

                string constring = ConfigurationManager.ConnectionStrings["Wyc_UI.Properties.Settings.SpiderResultConnectionString"].ConnectionString;
                if (con == null)
                {
                    con = new OleDbConnection(constring);
                    con.Open();
                }
                else if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                else if (con.State == System.Data.ConnectionState.Broken)
                {
                    con.Close();
                    con.Open();
                }
                return con;
                con.Close();

            }
        }

     

        //批量添加
        public void TableValuedToDB(DataTable dt,string sql,string typename)
        {
            conn = Getconn();
             string TSqlStatement = sql.ToString();
            SqlCommand cmd = new SqlCommand(TSqlStatement, conn);
            SqlParameter catParam = cmd.Parameters.AddWithValue("@NewBulkTestTvp", dt);
            catParam.SqlDbType = SqlDbType.Structured;
            //表值参数的名字叫BulkUdt，在上面的建立测试环境的SQL中有。  
            catParam.TypeName = typename;//"dbo.BulkUdt";
            try
            {
                
                conn.Open();
                if (dt != null && dt.Rows.Count != 0)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        //getTable
        public DataTable GetDataTable(string sql)
        {
            try
            {
                conn = Getconn();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds.Tables[0];

            }
            catch (Exception)
            {

                throw;
            }
        }
        //gettable12005
        public DataTable GetDataTable1(string sql)
        {
            try
            {
                conn2005 = Getconn1();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn2005);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds.Tables[0];

            }
            catch (Exception)
            {

                throw;
            }
        }
        //gettable12005
        public DataTable GetDataTable2(string sql)
        {
            try
            {
                zxconn = Getconn2();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, zxconn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds.Tables[0];

            }
            catch (Exception)
            {

                throw;
            }
        }
        //getoldbTable
        public DataTable GetOldbDataTable(string safeSql)
        {
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand(safeSql, Con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// ID int IDENTITY PRIMARY KEY, Title varchar(255), p ntext,p1 ntext,keys varchar(255),bztype varchar(255)
        /// </summary>
        /// <returns></returns>
        public  DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[]{  
              //new DataColumn("ID",typeof(int)),  
              new DataColumn("Title",typeof(string)),  
              new DataColumn("p",typeof(string)),
              new DataColumn("p1",typeof(string)),
              new DataColumn("keys",typeof(string)),
              new DataColumn("bztype",typeof(string))
            });

            return dt;
        }
        /// <summary>
        /// 创建生成文章临时表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNeiRongTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[]{  
              //new DataColumn("ID",typeof(int)),  
              new DataColumn("Title",typeof(string)),  
              new DataColumn("p",typeof(string))
            });

            return dt;
        }  
       
        //判断数据是否存在
        public  bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString()); //也可能=0
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString)
        {
            conn = Getconn();
                using (SqlCommand cmd = new SqlCommand(SQLString, conn))
                {
                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string sql)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select count(1) FROM KFLR ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            object obj = GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string SQLString)
        {
            zxconn = Getconn2();
                using (SqlCommand cmd = new SqlCommand(SQLString, zxconn))
                {
                    try
                    {
                        zxconn.Open();
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
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        zxconn.Close();
                        return  e.Message.ToString();
                    }
                }
            
        }
    }
}
