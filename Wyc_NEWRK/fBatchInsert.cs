using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;
using PanGu;
using System.Text.RegularExpressions;
using System.Data.OleDb;
namespace Wyc_NEWRK
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Util;
    using LuceneIO = Lucene.Net.Store;
    using Lucene.Net.Search;
    using System.Threading;
    using Wyc_NEWRK.Service;
    using ADOX;
    public class fBatchInsert
    {

        public delegate void GetTotalDelegate(int current);
        public delegate void GetTotalNum(int num);
        public delegate void GetTitletotal(int current);
        public delegate void GetTotalDelegateOutPut(int current);
        public delegate void GetTotaldaoruxuansan(int current);
        public delegate void GetMday(int count);
        //导入数据总数
        public int GetTotal(GetTotalDelegate gettotaldelegate)
        {
            int total = 0;
            wznr_Servise wznr = new wznr_Servise();
            Stopwatch sw = new Stopwatch();
            DataTable dt = wznr.GetOldbDataTable("select * from Content");//wznr.GetDataTable1("select * from News");//wznr.GetDataTable("select * from News"); //wznr.GetOldbDataTable("select 标题,内容 from Content");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total++;
                    gettotaldelegate(total);
                }
            }

            return total;
        }
        //获取db3数据库内容总数
        //public int GetTotal(GetTotalDelegate gettotaldelegate)
        //{
        //    int total = 0;
        //    //DataTable dt = new SqliteHelper().SqliteQuery("select * from [Content]").Tables[0];
        //    int num = new SqliteHelper().GetMaxID("ID", "Content");
        //    if (num > 0)
        //    {
        //        for (int i = 0; i < num; i++)
        //        {
        //            total++;
        //            gettotaldelegate(total);
        //        }
        //    }
        //    return total;
        //}
        //根据txt文件里行数调整进度条
        public int GetTitleTotal(GetTitletotal gettitletot)
        {
            int total = 0;
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string titlepath = path + @"\App_Data\sytitle.txt";
            StreamReader file = new StreamReader(titlepath, System.Text.Encoding.GetEncoding("GB2312"));
            string strFile = file.ReadToEnd(); //获取所有行
            string[] arraFile = strFile.Split('\n');
            for (int i = 0; i < arraFile.Length; i++)
            {
                total++;
                gettitletot(total);
            }
            return total;
        }
        //导出数据总数
        public int GetTotalN(GetTotalNum gettotalnum)
        {
            int total = 0;
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = wznr.GetDataTable("select * from test5Table");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total++;
                    gettotalnum(total);
                }
            }
            return total;
        }
        //查询数据总数，导入宣三系统
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getxs"></param>
        /// <returns></returns>
        public int GetTotalXS(GetTotaldaoruxuansan getxs)
        {
            int total = 0;
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = wznr.GetDataTable2("select * from KFLR");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total++;
                    getxs(total);
                }
            }
            return total;
        }
        //按月查询
        public int GetTotalMXS(GetMday getxs, string sql)
        {
            int total = 0;
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = wznr.GetDataTable2(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total++;
                    getxs(total);
                }
            }
            return total;
        }
        //按标题搜索生成内容 2012-6-5创建
        public void ShengChengNeiRong(GetTitletotal getTotalRecordsDelegate, System.Diagnostics.Stopwatch sw, System.Windows.Forms.RichTextBox rich)
        {
            
            int totalRecords = 0;
            wznr_Servise wznr = new wznr_Servise();
            DataTable newdt = wznr.GetNeiRongTable();
            DataRow newdr;
            FenCiHelper fch = new FenCiHelper();
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string titlepath = path + @"\App_Data\sytitle.txt";
            StreamReader file = new StreamReader(titlepath, System.Text.Encoding.GetEncoding("GB2312"));
            string keyword;//文章标题行
            string line;//关键词行
            string   strFile   =   file.ReadToEnd(); //获取所有行
            string[]   arraFile   =   strFile.Split('\n');
            string tongyicititle;//同义词标题
            for (int i = 0; i < arraFile.Length; i++)
            {
                totalRecords++;
                getTotalRecordsDelegate(totalRecords);
                string key=arraFile[i];
                //tongyicititle = fch.PanGuFenCiTYC( arraFile[i]);//输出同义词标题
                keyword = fch.PanguFenCi(arraFile[i]);
                string[] arr = keyword.Split('/');
                string txtpath = path + @"\App_Data\sDict.txt";
                //读取文本内容逐行
                StreamReader file1 = new StreamReader(txtpath);

                while ((line = file1.ReadLine()) != null)
                {
                    string[] arrtxt = line.Split(',');
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (arrtxt[0].Equals(arr[j]))
                        {
                            
                            keyword = arrtxt[0] + key;
                            rich.Text += keyword ;
                            rich.ForeColor = System.Drawing.Color.Green;//ConsoleColor.Green;
                            break;
                        }
                    }
                }
                string field = "contents";//搜索的对应字段
                string[] fieldArr = new string[] { field, "title" };//两个字段
                string rangeField = "createdate";//范围搜索对应字段
                IList<Analyzer> listAnalyzer = WycLuceneAnalyzer.BuildAnalyzers(); //LuceneAnalyzer.BuildAnalyzers();
                BooleanClause.Occur[] occurs = new BooleanClause.Occur[] { BooleanClause.Occur.MUST, BooleanClause.Occur.SHOULD };
                foreach (Analyzer analyzer in listAnalyzer)
                {
                    WycLuceneSearch.PanguQueryTest(analyzer, field, keyword, rich);//通过盘古分词搜索
                }
                newdr = newdt.NewRow();
                newdr["Title"] = key;//keyword;//tongyicititle;
                newdr["p"] = rich.Text;
                newdt.Rows.Add(newdr);
            }
            string sqlp = "insert into test5Table (Title,p)" +
                " SELECT  nc.Title,nc.p" +
                " FROM @NewBulkTestTvp AS nc";
            sw.Start();
            wznr.TableValuedToDB(newdt, sqlp, "dbo.test5Udt");
            sw.Stop();
            getTotalRecordsDelegate(totalRecords);
               
        }
        //创建索引导入数据2012-5-29添加--修改类（弃用）
        public void CreateIndexImport(GetTotalDelegate getTotalRecordsDelegate, System.Diagnostics.Stopwatch sw,System.Windows.Forms.RichTextBox rich)
        {
            int totalRecords = 0;
            FenCiHelper fch = new FenCiHelper();
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = wznr.GetOldbDataTable("select 标题,内容 from Content");//wznr.GetDataTable1("SELECT Title, Content FROM News");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype", typeof(string)) });//添加新列
            DataRow dr = dt.NewRow();//实例化新行
            DataTable newdt = wznr.GetTableSchema();
            DataRow newdr;
            string line;
            string p;//每个p标签
            for (int i = 0; i < dt.Rows.Count; i++)//添加新列分词录入
            {
                totalRecords++;
                getTotalRecordsDelegate(totalRecords);
                string title = Convert.ToString(dt.Rows[i][0]);//获取到标题
                string content = Convert.ToString(dt.Rows[i][1]);//获取到内容
                //string bztype = Convert.ToString(dt.Rows[i][2]);//类别
                //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                title = fch.PanguFenCi(title);
                //p标签获取获取每个p---2012-5-18日修改
                //p = GetPhtml(content);
                //获取路径,循环每次都需要读取文本文件里设置的关键词 
                string path = Directory.GetCurrentDirectory();
                string txtpath = path + @"\App_Data\sDict.txt";
                //读取文本内容逐行
                StreamReader file = new StreamReader(txtpath);
                //分类操作，读取文本与标题分词判断
                string[] arr = title.Split('/');
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtxt = line.Split(',');
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (arrtxt[0].Equals(arr[j]))
                        {
                            dt.Rows[i][3] += arrtxt[1] + ",";//类别
                            break;
                            //strtxt = arrtxt[1];//所属类别
                        }
                    }
                }
                //调用PanGuContentFenCi（content）进行分词同时输出同义词
                dt.Rows[i][1] = fch.PanGuContentFenCi(content);
                dt.Rows[i][2] = title;
                //dt.Rows[i][3] = bztype;
                Analyzer analyzer = new PanGuAnalyzer();//盘古Analyzer
                DirectoryInfo dirInfo = Directory.CreateDirectory(Config.INDEX_STORE_PATH);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
                foreach (Match m in Regex.Matches(content, @"<(\w+)>[^P]*[^<]*</(\w+)>"))
                {

                    newdr = newdt.NewRow();
                    newdr["Title"] = dt.Rows[i][0];
                    newdr["p"] = m.Value;
                    newdr["p1"] = content;
                    newdr["keys"] = title;
                    newdr["bztype"] = dt.Rows[i][3];
                    newdt.Rows.Add(newdr);
                   // CreateIndex(writer, dt.Rows[i][0].ToString(), m.Value);创建索引
                }
                writer.Optimize();
                writer.Close();
                rich.Text += dt.Rows[i][2].ToString() + "-----索引创建成功\n";
                rich.ForeColor = System.Drawing.Color.Green;//ConsoleColor.Green;
            }
            string sql = "insert into test3Table (Title,Content,keys,bztype)" +
                " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
                " FROM @NewBulkTestTvp AS nc";
            string sqlp = "insert into test4Table (Title,p,p1,keys,bztype)" +
                " SELECT  nc.Title,nc.p,nc.p1,nc.keys,nc.bztype" +
                " FROM @NewBulkTestTvp AS nc";
            sw.Start();
            wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
            wznr.TableValuedToDB(newdt, sqlp, "dbo.test4Udt");
            sw.Stop();
            getTotalRecordsDelegate(totalRecords);

        }
        //导入数据
        public void fBatchImport(GetTotalDelegate getTotalRecordsDelegate, System.Diagnostics.Stopwatch sw)
        {
            int totalRecords = 0;
            FenCiHelper fch = new FenCiHelper();
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = wznr.GetOldbDataTable("select 标题,内容 from Content");//wznr.GetDataTable1("SELECT Title, Content FROM News");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype",typeof(string)) });//添加新列
            DataRow dr = dt.NewRow();//实例化新行
            DataTable newdt = wznr.GetTableSchema();
            DataRow newdr;
            string line;
            string p;//每个p标签
            for (int i = 0; i < dt.Rows.Count; i++)//添加新列分词录入
            {
                
                string title = Convert.ToString(dt.Rows[i][0]);//获取到标题
                string content = Convert.ToString(dt.Rows[i][1]);//获取到内容
                //string bztype = Convert.ToString(dt.Rows[i][2]);//类别
                //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                title = fch.PanguFenCi(title);
                //p标签获取获取每个p---2012-5-18日修改
                //p = GetPhtml(content);
                //获取路径,循环每次都需要读取文本文件里设置的关键词 
                string path = Directory.GetCurrentDirectory();
                string txtpath = path + @"\App_Data\sDict.txt";
                //读取文本内容逐行
                StreamReader file = new StreamReader(txtpath);
                //分类操作，读取文本与标题分词判断
                string[] arr = title.Split('/');
                while ((line = file.ReadLine()) != null)
                {
                    string[] arrtxt = line.Split(',');
                    for (int j = 0; j < arr.Length; j++)
                     {
                        if (arrtxt[0].Equals(arr[j]))
                        {
                            dt.Rows[i][3] += arrtxt[1] + ",";//类别
                            break;
                            //strtxt = arrtxt[1];//所属类别
                        }
                    }
                }
                //调用PanGuContentFenCi（content）进行分词同时输出同义词
                dt.Rows[i][1] = fch.PanGuContentFenCi(content);
                dt.Rows[i][2] = title;
                //dt.Rows[i][3] = bztype;<(\w+)>[^P]*[^<]*</(\w+)>||<p[^>]*>[^<]*</p>
                // Regex("\ba\w{6}\b", RegexOptions.IgnoreCase);, RegexOptions.IgnoreCase//区别大小写
                foreach (Match m in Regex.Matches(content, @"<p[^>]*>[^<]*</p>", RegexOptions.IgnoreCase))
                {
                    if (m.Value.Length > 50)
                    {
                        newdr = newdt.NewRow();
                        newdr["Title"] = dt.Rows[i][0];
                        newdr["p"] = m.Value;
                        newdr["p1"] = content;
                        newdr["keys"] = title;
                        newdr["bztype"] = dt.Rows[i][3];
                        newdt.Rows.Add(newdr);
                    }
                }
                totalRecords++;
                getTotalRecordsDelegate(totalRecords);
                //newdt.Rows.Add(newdr);
            }
            
            string sql = "insert into test3Table (Title,Content,keys,bztype)" +
                " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
                " FROM @NewBulkTestTvp AS nc";
            string sqlp = "insert into test4Table (Title,p,p1,keys,bztype)" +
                " SELECT  nc.Title,nc.p,nc.p1,nc.keys,nc.bztype" +
                " FROM @NewBulkTestTvp AS nc";
            sw.Start();
            wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
            wznr.TableValuedToDB(newdt, sqlp, "dbo.test4Udt");
            sw.Stop();
            getTotalRecordsDelegate(totalRecords);
        }
        //导出access数据库

        public void access_OutPut(GetTotalDelegateOutPut gettotaldelegateoutput, System.Diagnostics.Stopwatch sw, System.Windows.Forms.RichTextBox rich)
        {
            wznr_Servise wznr = new wznr_Servise();
            int totalRecords = 0;
            string dbn = System.AppDomain.CurrentDomain.BaseDirectory + "Access_Data\\" + "SpiderResult.mdb";//数据库文件名称

            // 创建数据库文件  
            File.Delete(dbn);
            ADOX.Catalog catalog = new Catalog();
            catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbn + ";Jet OLEDB:Engine Type=5");
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbn);
            conn.Open();
            // 创建数据表  
            string sql = "CREATE TABLE Content([ID] Counter primary key,[已采] Bit,[已发] Bit,[标题] Memo,[内容] Memo,[PageUrl] Memo)";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            sw.Start();
            //DbTransaction trans = conn.BeginTransaction(); // <-------------------  
            try
            {
                // 查询出数据导出 通过数据库查询出 
                DataTable dt = wznr.GetDataTable("SELECT Title, p FROM test5Table order by NEWID()");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalRecords++;
                    gettotaldelegateoutput(totalRecords);
                    //cmd.CommandText = "insert into [Content] ([已采],[已发],[标题],[内容],[PageUrl]) values (?,?,?,?,?)";
                    string strSql = "insert into [Content]([已采],[已发],[标题],[内容],[PageUrl]) values(?,?,?,?,?)";
                    OleDbParameter[] parameter ={
                                                new OleDbParameter("@[已采]", OleDbType.Boolean,1),
                                                new OleDbParameter("@[已发]", OleDbType.Boolean,1),
                                                new OleDbParameter("@[标题]", OleDbType.VarChar,0),
                                                new OleDbParameter("@[内容]", OleDbType.VarChar,0),
                                                new OleDbParameter("@[PageUrl]", OleDbType.VarChar,0)
                                            };

                    parameter[0].Value = 1;
                    parameter[1].Value = 0;
                    parameter[2].Value = dt.Rows[i][0].ToString();
                    parameter[3].Value = dt.Rows[i][1].ToString();
                    parameter[4].Value = "http://www.xxx.com.cn";
                    rich.AppendText(parameter[2].Value.ToString() + "-----导出成功\n");
                    rich.ForeColor = System.Drawing.Color.Green;//ConsoleColor.Green;
                    rich.Focus();
                    DbHelperOleDb.GetSingle(strSql.ToString(), parameter);
                    
                    //cmd.ExecuteNonQuery();
                }
                //trans.Commit(); // <-------------------
            }
            catch
            {
                // trans.Rollback(); // <-------------------
                throw; // <-------------------
                //   }
                // 停止计时
                
            }
            sw.Stop();
        }

        //导出数据db3
        public void OutPut(GetTotalDelegateOutPut gettotaldelegateoutput,System.Diagnostics.Stopwatch sw,System.Windows.Forms.RichTextBox rich )
        {
            wznr_Servise wznr = new wznr_Servise();
            int totalRecords = 0;
            string dbn ="SpiderResult.db3";//数据库文件名称
            // 创建数据库文件  
            File.Delete(dbn);
            SQLiteConnection.CreateFile(dbn);
            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                // 连接数据库  
                conn.ConnectionString = "Data Source=" + dbn + "";
                conn.Open();
                // 创建数据表  
                string sql = "CREATE TABLE Content([ID] integer primary key autoincrement,[已采] tinyint(1) default 0,[已发] tinyint(1) default 0,[标题] Text,[内容] Text,[PageUrl] Text)";
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sw.Start();
                DbTransaction trans = conn.BeginTransaction(); // <-------------------  
                try
                {
                    // 查询出数据导出 通过数据库查询出 
                    DataTable dt = wznr.GetDataTable("SELECT Title, p FROM test5Table");
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalRecords++;
                        gettotaldelegateoutput(totalRecords);
                        cmd.CommandText = "insert into [Content] ([已采],[已发],[标题],[内容],[PageUrl]) values (?,?,?,?,?)";
                        SQLiteParameter[] parameter ={
                                                new SQLiteParameter("@[已采]",DbType.Byte,1),
                                                new SQLiteParameter("@[已发]",DbType.Byte,1),
                                                new SQLiteParameter("@[标题]",DbType.Object),
                                                new SQLiteParameter("@[内容]",DbType.Object),
                                                new SQLiteParameter("@[PageUrl]",DbType.Object)
                                            };
                        //foreach (SQLiteParameter p in parameter)
                        //{
                        //    cmd.Parameters.Add(p);
                        //}
                        cmd.Parameters[0].Value = 1;
                        cmd.Parameters[1].Value = 0;
                        cmd.Parameters[2].Value = dt.Rows[i][0].ToString();
                        cmd.Parameters[3].Value = dt.Rows[i][1].ToString();
                        cmd.Parameters[4].Value = "http://www.9111766.com";
                        //rich.Text += cmd.Parameters[2].Value.ToString()+"-----导出成功\n";
                        //StreamWriter sr = File.CreateText(dt.Rows[i][0].ToString().Trim()+".txt");
                        //sr.WriteLine(dt.Rows[i][0].ToString()+"\n"+dt.Rows[i][1].ToString());
                        //sr.Close();
                        rich.AppendText(cmd.Parameters[2].Value.ToString() + "-----导出成功\n");
                        rich.ForeColor = System.Drawing.Color.Green;//ConsoleColor.Green;
                        rich.Focus();
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit(); // <-------------------
                }
                catch
                {
                    trans.Rollback(); // <-------------------
                    throw; // <-------------------
                }
                // 停止计时
                sw.Stop();
            }
        }
        //获取p标签,正则表达式获取---2012-5-17日添加
        public string GetPhtml(string str)
        {
            string restult = "";
            //<(\w+)>[^P]*[^<]*</(\w+)> 获取<p>*</p>中的值
            foreach (Match m in Regex.Matches(str, @"<(\w+)>[^P]*[^<]*</(\w+)>"))//@"<p[^>]*>[^<]*</p>"2012-5-18修改
            {
                restult += m.Value;
            }
            return restult;
        }
        /// <summary>
        /// C# :从一段字符串中，输入开始和结束的字符，取中间的字符
        /// </summary>
        /// <param name="str">一段字符串</param>
        /// <param name="strStart">开始字符</param>
        /// <param name="strEnd">结束字符</param>
        /// <returns></returns>
        public  string AnalyzeMessage(string str, string strStart, string strEnd)
        {
            string Result = "";
            int i = str.IndexOf(strStart);
            if (i >= 0)
            {
                int j = str.IndexOf(strEnd, i + strStart.Length);
                if (j > 0)
                {
                    Result = str.Substring(i + strStart.Length, j - i - strStart.Length);
                }
            }
            return Result;
        }
        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        private static void CreateIndex(IndexWriter writer, string title, string content)
        {
            try
            {
                Document doc = new Document();
                doc.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                doc.Add(new Field("contents", content, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                doc.Add(new Field("createdate", DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"), Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                writer.AddDocument(doc);
            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //导入数据十万级数据
        public void insertsqliteshuju(GetTotalDelegate getTotalRecordsDelegate, System.Diagnostics.Stopwatch sw)
        {
            int totalRecords = 0;
            FenCiHelper fch = new FenCiHelper();
            wznr_Servise wznr = new wznr_Servise();
            int num = new SqliteHelper().GetMaxID("ID", "Content");
            
            string line;
            string p;//每个p标签
            int pagesize = num / 1000;
            if (pagesize == 0)
            {
                DataTable dt = wznr.GetOldbDataTable("select 标题,内容 from Content");//wznr.GetDataTable1("SELECT Title, Content FROM News");
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype", typeof(string)) });//添加新列
                DataRow dr = dt.NewRow();//实例化新行
                DataTable newdt = wznr.GetTableSchema();
                DataRow newdr;
                for (int i = 0; i < dt.Rows.Count; i++)//添加新列分词录入
                {
                    totalRecords++;
                    getTotalRecordsDelegate(totalRecords);
                    string title = Convert.ToString(dt.Rows[i][0]);//获取到标题
                    string content = Convert.ToString(dt.Rows[i][1]);//获取到内容
                    //string bztype = Convert.ToString(dt.Rows[i][2]);//类别
                    //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                    title = fch.PanguFenCi(title);
                    //p标签获取获取每个p---2012-5-18日修改
                    //p = GetPhtml(content);
                    //获取路径,循环每次都需要读取文本文件里设置的关键词 
                    string path = Directory.GetCurrentDirectory();
                    string txtpath = path + @"\App_Data\sDict.txt";
                    //读取文本内容逐行
                    StreamReader file = new StreamReader(txtpath);
                    //分类操作，读取文本与标题分词判断
                    string[] arr = title.Split('/');
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] arrtxt = line.Split(',');
                        for (int j = 0; j < arr.Length; j++)
                        {
                            if (arrtxt[0].Equals(arr[j]))
                            {
                                dt.Rows[i][3] += arrtxt[1] + ",";//类别
                                break;
                                //strtxt = arrtxt[1];//所属类别
                            }
                        }
                    }
                    //调用PanGuContentFenCi（content）进行分词同时输出同义词
                    dt.Rows[i][1] = fch.PanGuContentFenCi(content);
                    dt.Rows[i][2] = title;
                    //dt.Rows[i][3] = bztype;
                    foreach (Match m in Regex.Matches(content, @"<(\w+)>[^P]*[^<]*</(\w+)>"))
                    {

                        newdr = newdt.NewRow();
                        newdr["Title"] = dt.Rows[i][0];
                        newdr["p"] = m.Value;
                        newdr["p1"] = content;
                        newdr["keys"] = title;
                        newdr["bztype"] = dt.Rows[i][3];
                        newdt.Rows.Add(newdr);
                    }

                    //newdt.Rows.Add(newdr);
                }
                string sql = "insert into test3Table (Title,Content,keys,bztype)" +
                " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
                " FROM @NewBulkTestTvp AS nc";
                string sqlp = "insert into test4Table (Title,p,p1,keys,bztype)" +
                    " SELECT  nc.Title,nc.p,nc.p1,nc.keys,nc.bztype" +
                    " FROM @NewBulkTestTvp AS nc";
                sw.Start();
                wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
                wznr.TableValuedToDB(newdt, sqlp, "dbo.test4Udt");
                sw.Stop();
                getTotalRecordsDelegate(totalRecords);
            }
            else
            {
                pagesize= pagesize + 1;
                List<String> dtsqllist = new List<string>();
                DataTable dt = new DataTable();
                for (int i = 0; i < pagesize; i++)
                {
                    string sqlitesql = "SELECT  标题,内容 FROM  content ORDER BY id asc LIMIT 1000 OFFSET (1000*'" + i + "')";
                        //dtsqllist.Add(sqlitesql);
                    dt = new SqliteHelper().GetQuery(sqlitesql);//查询sqlite数据库new SqliteHelper().gettablequert(dtsqllist); //
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype", typeof(string)) });//添加新列
                    DataRow dr = dt.NewRow();//实例化新行
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        totalRecords++;
                        getTotalRecordsDelegate(totalRecords);
                        string title = Convert.ToString(dt.Rows[k][0]);//获取到标题
                        string content = Convert.ToString(dt.Rows[k][1]);//获取到内容
                        //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                        title = fch.PanguFenCi(title);
                        //调用PanGuContentFenCi（content）进行分词同时输出同义词
                        dt.Rows[k][1] = content;
                        dt.Rows[k][2] = title;
                        dt.Rows[k][3] = 1;
                    }
                    string sql = "insert into test3Table (Title,Content,keys,bztype)" +
                   " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
                   " FROM @NewBulkTestTvp AS nc";
                    sw.Start();
                    wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
                    sw.Stop();
                    getTotalRecordsDelegate(totalRecords);
                }
            }
        }
        public SQLiteConnection GetSQLiteConnection()
        {
            string path = Directory.GetCurrentDirectory();
            string dbpath = path + @"\SpiderResult.db3";
            return new SQLiteConnection("Data Source=" + dbpath);
        }
        
        public void MyThreadInsert(GetTotalDelegate getTotalRecordsDelegate, System.Diagnostics.Stopwatch sw,int pagesize)
        {
            int totalRecords = 0;
            FenCiHelper fch = new FenCiHelper();
            wznr_Servise wznr = new wznr_Servise();
            DataTable dt = new DataTable();
                string sqlitesql = "SELECT  标题,内容 FROM  content ORDER BY id asc LIMIT 5000 OFFSET (5000*'" + pagesize + "')";
                //dtsqllist.Add(sqlitesql);
                dt = new SqliteHelper().GetQuery(sqlitesql);//查询sqlite数据库new SqliteHelper().gettablequert(dtsqllist); //
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype", typeof(string)) });//添加新列
                DataRow dr = dt.NewRow();//实例化新行
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    totalRecords++;
                    getTotalRecordsDelegate(totalRecords);
                    string title = Convert.ToString(dt.Rows[k][0]);//获取到标题
                    string content = Convert.ToString(dt.Rows[k][1]);//获取到内容
                    //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                    title = fch.PanguFenCi(title);
                    //调用PanGuContentFenCi（content）进行分词同时输出同义词
                    dt.Rows[k][1] = content;
                    dt.Rows[k][2] = title;
                    dt.Rows[k][3] = 1;
                    //Thread.Sleep(Convert.ToInt32(pagesize));
                    //threadEvent.Invoke(k, new EventArgs());//通知主界面我正在执行,i表示进度条当前进度
                    
                }
                string sql = "insert into test3Table (Title,Content,keys,bztype)" +
               " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
               " FROM @NewBulkTestTvp AS nc";
                sw.Start();
                wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
                sw.Stop();
                getTotalRecordsDelegate(totalRecords);
                
                
        }
        //导入到宣三系统
        /// <summary>
        /// 根据算法按照分配百分比，导入相对应数据
        /// </summary>
        /// <param name="gettotal"></param>
        /// <param name="sw"></param>
        /// <param name="rich"></param>
        public void daoruxuansan(GetTotaldaoruxuansan gettotal, GetMday getmtotal, System.Diagnostics.Stopwatch sw, System.Windows.Forms.RichTextBox rich)
        {
            wznr_Servise service = new wznr_Servise();
            servicemodel.service model = new servicemodel.service();
            serviceBLL bll = new serviceBLL();
            StringBuilder sb = new StringBuilder();
            string sql = "";
            string sqlfenye = "";
            double[] numbers = new double[5] { 0.31, 0.27, 0.08, 0.21, 0.13 };
            int totalRecords = 0;
            int total = 0;
            string name = "";
            int temp = 0;//循环第一次的值
            //for 两个循环，第一循环控制年份，第二循环控制月份，
            sw.Start();
            for (int y = 2010; y < 2013; y++)
            {
                for (int m = 1; m < 13; m++)
                {
                    int temp1 = 0;//计数器
                    double num = 0;//获取人员分配条数
                    sql = "select * from KFLR where datepart(mm,pubtime)=" + m + " and DATEPART(yy,pubtime)=" + y;
                    ////获取每月数据
                    int count = service.GetRecordCount("select count(*) from KFLR where datepart(mm,pubtime)=" + m + " and DATEPART(yy,pubtime)=" + y);
                    //按照比例分配 count*numbers[i],按照每个部门分配比例循环获按月份取条数
                    Form2WaittingGetTotalRecords form = new Form2WaittingGetTotalRecords();
                    form.Show();
                    form.Text = string.Format("{2}年-{0}月数据：{1}条", m.ToString(), count.ToString(),y.ToString());
                    totalRecords = GetTotalMXS(form.GetTotalNum, sql);//fBatchInsert.GetTotalXS(form.GetTotalNum);
                    for (int t = 0; t < numbers.Length; t++)
                    {
                        switch (numbers[t].ToString())
                        { 
                            case "0.31":
                                name = "杨敏月";
                                break;
                            case "0.27":
                                name = "刘洪那";
                                break;
                            case "0.08":
                                name = "朱欢";
                                break;
                            case "0.21":
                                name = "门诊";//默认刘雪梅部门
                                break;
                            case "0.13":
                                name = "马健";
                                break;
                            default:
                                break;
                        }
                        num =Convert.ToInt32( Math.Round((totalRecords * numbers[t]),0)); //Round((totalRecords * numbers[t]), 0);
                        temp = Convert.ToInt32(Math.Round((totalRecords * numbers[0]), 0));//记录第一次值
                        //select top 10 * from KFLR where ID not in(select top (10*(2-1)) ID from KFLR where DATEPART(MM,PubTime)=5 and DATEPART(YY,PubTime)=2010) and datepart(mm,pubtime)=5 and DATEPART(yy,pubtime)=2010
                        sqlfenye = "select top " + num + " * from KFLR where ID not in(select top " + temp1 + " ID from KFLR where DATEPART(MM,PubTime)=" + m + " and DATEPART(YY,PubTime)=" + y + ") and datepart(mm,pubtime)=" + m + " and DATEPART(yy,pubtime)=" + y + "";
                        temp1 = temp1 +Convert.ToInt32( num);//每次循环出来的值+上一次num的值
                        //DataTable dt = service.GetDataTable2("select top " + num + " * from KFLR where datepart(mm,pubtime)=" + m + " and DATEPART(yy,pubtime)=" + y);
                        DataTable dt = service.GetDataTable2(sqlfenye);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            model.username = Convert.ToString(dt.Rows[i]["username"]);//患者名称
                            model.userage = Convert.ToString(dt.Rows[i]["age"]);//患者年龄
                            model.usersex = "不详";//患者性别
                            model.userphoto = Convert.ToString(dt.Rows[i]["tel"]);//患者电话
                            model.useraddress = Convert.ToString(dt.Rows[i]["address"]);//患者地址
                            model.subtime = Convert.ToDateTime(dt.Rows[i]["pubtime"]);//提交时间
                            model.zhengzhuang = Convert.ToString(dt.Rows[i]["bingzhong"]);//病情症状
                            model.chats = Convert.ToString(dt.Rows[i]["ltrecord"]);//内容
                            model.eatime = dt.Rows[i]["teltime"] == DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["teltime"]);//通话日期
                            model.telextract = null;
                            model.eaname = Convert.ToString(dt.Rows[i]["zbusername"]);//值班人
                            model.userweb = Convert.ToString(dt.Rows[i]["webaddress"]);//所属网站
                            model.theirpeople = Convert.ToString(dt.Rows[i]["pubusername"]);//录入人员
                            model.flag = 0;
                            model.memo = Convert.ToString(dt.Rows[i]["beizhu"]).Trim();//备注
                            model.bj = "0";
                            model.sfzy = "1";
                            model.typeflag = "1";
                            model.hfdata = dt.Rows[i]["retime"] == DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["retime"]);//回复日期
                            model.sfyx = "0";//是否有效
                            model.zzy = "所属资源是：" + Convert.ToString(dt.Rows[i]["zbusername"]);//所属资源
                            model.sfdh = "1";
                            model.deleflag = "0";
                            model.zhenduan = Convert.ToString(dt.Rows[i]["bingzhong"]);//病种
                            model.stas = "0";
                            model.zzystas = "0";
                            model.lasttime = dt.Rows[i]["teltime"] == DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["teltime"]);
                            model.flagbumen = name + "部门";
                            model.xcbumen = "宣传四";
                            model.kfbumen = "宣传四";
                            model.bianyuan = "0";
                            model.urltext = null;
                            model.bqname = null;
                            model.byname = Convert.ToString(dt.Rows[i]["username"]);
                            model.byphone = Convert.ToString(dt.Rows[i]["tel2"]);
                            model.zydate = Convert.ToDateTime(dt.Rows[i]["pubtime"]);
                            model.strurl = "没有网址";
                            model.hotflag = "";
                            model.menzhenname = Convert.ToString(dt.Rows[i]["username2"]);
                            model.menzhenphone = Convert.ToString(dt.Rows[i]["tel2"]);
                            model.kefuname = Convert.ToString(dt.Rows[i]["username"]);
                            model.kefuphone = Convert.ToString(dt.Rows[i]["tel"]);
                            model.memo2 = Convert.ToString(dt.Rows[i]["juage3"]);
                            model.flag2 = "0";
                            model.jdflag = "0";
                            model.xcname = null;
                            model.baobei = Convert.ToDateTime("1987-06-19 00:00:00.000");
                            bll.Add(model);
                            total++;
                            gettotal(total);
                        }
                        //rich.AppendText(string.Format("{2}年-{0}月数据：{1}条", m.ToString(), count.ToString(), y.ToString()) + "分配给" + name + "" + num + "条分配语句：\n" + sqlfenye+ temp1 + "\n");
                        rich.AppendText(string.Format("{2}年-{0}月数据：{1}条", m.ToString(), count.ToString(), y.ToString()) + "分配给" + name + "" + num + "\n");
                        rich.ForeColor = System.Drawing.Color.Green;//设置字体颜色
                        rich.Focus();
                    }
                    form.Close();
                   
                }
            }
            // 停止计时
            sw.Stop();
        }

        /// <summary>
        /// 实现数据的四舍五入法
        /// </summary>
        /// <param name="v">要进行处理的数据</param>
        /// <param name="x">保留的小数位数</param>
        /// <returns>四舍五入后的结果</returns>
        private double Round(double v, int x)
        {
            bool isNegative = false;
            //如果是负数
            if (v < 0)
            {
                isNegative = true;
                v = -v;
            }

            int IValue = 1;
            for (int i = 1; i <= x; i++)
            {
                IValue = IValue * 10;
            }
            double Int = Math.Round(v * IValue + 0.5, 0);
            v = Int / IValue;

            if (isNegative)
            {
                v = -v;
            }

            return v;
        }
    }
}
