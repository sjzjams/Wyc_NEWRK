using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hubble.SQLClient;
using System.Data.SqlClient;
using System.IO;

namespace Wyc_NEWRK
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Search;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Util;
    using LuceneIO = Lucene.Net.Store;
    using Wyc_NEWRK.Service;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 创建配置文件app.config
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="databasename"></param>
        private void CreateXml(string dbname, string databasename)
        {
            string oldb=@"|DataDirectory|\App_Data\SpiderResult.mdb";
            StringBuilder strxml=new StringBuilder();
            strxml.Append("<configuration>");
            strxml.Append("<configSections></configSections>");
            strxml.Append("<connectionStrings>");
            strxml.Append("<add name=\"connectionstring\" connectionString=\"Data Source="+dbname+";Initial Catalog="+databasename+";User ID=sa;Pwd=sa;\" providerName=\"System.Data.SqlClient\" />");
            strxml.Append("<add name=\"Wyc_UI.Properties.Settings.SpiderResultConnectionString\" connectionString=\"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+oldb+";Persist Security Info=True\" providerName=\"System.Data.OleDb\" />");
            strxml.Append("</connectionStrings>");
            strxml.Append("<startup>");
            strxml.Append("<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.0\"/>");
            strxml.Append("</startup>");
            strxml.Append("</configuration>");
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(strxml.ToString());
            doc.Save(@"App.config");  
           //doc.Save(@"XT_GNB.xml");
            //doc.Save(Server.MapPath("~/XT_GNB.xml"));
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            fBatchInsert fb = new fBatchInsert();
           // batchinsert.Show();
            FormWaittingGetTotalRecords frmwaitting = new FormWaittingGetTotalRecords();
            frmwaitting.Show();
            int totalrecord = fb.GetTotal(frmwaitting.GetTotalDelegate);
            frmwaitting.Close();
            BatchInsert batchinsert = new BatchInsert();
            batchinsert.TotalRecords = totalrecord;
            batchinsert.fBatchInsert = fb;
            batchinsert.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var i=40-32/2;
            MessageBox.Show(i.ToString());
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button1_Click_1(object sender, EventArgs e)
        //{
            
        //    try
        //    {
        //        if (this.tablename.Text.Trim() == "")
        //        {

        //            MessageBox.Show("TableName不能为空！！", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        MessageBox.Show(string.Format("数据库名称：{0},连接字符串{1}", this.tablename.Text, this.dbtext));
        //    }
        //    catch (Exception e1)
        //    {
        //        MessageBox.Show(e1.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
        //    }
            
        //}
        /// <summary>
        /// 测试 链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Trim() == "")
                {
                    MessageBox.Show("DB Adapter不能为空！！", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dbtext.Text = string.Format("Data Source={0};Initial Catalog={1};User ID=sa;Pwd=sql2008;", comboBox1.Text.Trim(),textdatabase.Text.Trim());
                if (dbtext.Text.Trim() == "")
                {
                    MessageBox.Show("链接字符串不明确！！", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection con; //= new SqlConnection(dbtext.Text.Trim());
                con =new wznr_Servise().Getconn();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                       
                        MessageBox.Show("链接成功！", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btndatabase.Visible = true;
                        this.dbtext.ReadOnly = true;
                        //this.label1.Visible = true;
                        //this.tablename.Visible = true;
                        //this.button1.Visible = true;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textdatabase.ReadOnly = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Text = @"LQX\XUANWU2008";

            //richTextBox3.Text = string.Format("数据内容:{0}");
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndatabase_Click(object sender, EventArgs e)
        {
           
            string path = Directory.GetCurrentDirectory() + @"\App.config";
            if (!Directory.Exists(path))
            {
                //MessageBox.Show("cunzai");
            }
            else
            {
                CreateXml(comboBox1.Text.Trim(), textdatabase.Text.Trim());
            }
            string sql = string.Format("select * from master.dbo.sysdatabases where name = '{0}'",textdatabase.Text.Trim());
            wznr_Servise wznrservise = new wznr_Servise();
            DataTable dt = wznrservise.GetDataTable(sql);
            if (dt.Rows.Count>0)
            {
                MessageBox.Show("数据库已存在！", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string sql1 = " Create table test2Table( ID int IDENTITY PRIMARY KEY,   Title varchar(255),   Content ntext,   keys varchar(255)   )";
                    string sql2 = " CREATE TYPE test2Udt AS TABLE  (  Title varchar(255),  Content ntext,  keys varchar(255)  )";
                    wznrservise.ExecuteSql(sql1);
                    wznrservise.ExecuteSql(sql2);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            //wznrservise.Exists(strsql.ToString());
            //MessageBox.Show("创建成功！", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);           
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBatchInsert fb = new fBatchInsert();
            // batchinsert.Show();
            Form2WaittingGetTotalRecords frmwatiting = new Form2WaittingGetTotalRecords();
            frmwatiting.Show();
            int totalrecord = fb.GetTotalN(frmwatiting.GetTotalNum);
            frmwatiting.Close();
            Output output = new Output();
            output.TotalRecords = totalrecord;
            output.fBatchInsert = fb;
            output.ShowDialog();
        }

        private void 创建索引导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBatchInsert fb = new fBatchInsert();
            // batchinsert.Show();
            Form3WaittingGetTotalRecords frmwaitting = new Form3WaittingGetTotalRecords();
            frmwaitting.Show();
            int totalrecord = fb.GetTitleTotal(frmwaitting.GetTitletotal);
            frmwaitting.Close();
            test tt = new test();
            tt.TotalRecords = totalrecord;
            tt.fBatchInsert = fb;
            tt.ShowDialog();
            
        }
        //盘古测试查询
        private void button1_Click_1(object sender, EventArgs e)
        {
            FenCiHelper fch = new FenCiHelper();
            WycLuceneSearch wycsearch = new WycLuceneSearch();
            string keyword = this.textBox1.Text;
            string keyto = this.textBox1.Text;
            keyword = fch.PanguFenCi(keyword);//对关键字进行分词处理//fch.PanguFenCi(keyword);
            string line;
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\sDict.txt";
            //读取文本内容逐行
            StreamReader file = new StreamReader(txtpath);
            //分类操作，读取文本与标题分词判断
            string[] arr = keyword.Split('/');
            while ((line = file.ReadLine()) != null)
            {
                string[] arrtxt = line.Split(',');
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arrtxt[0].Equals(arr[j]))
                    {
                        keyword = arrtxt[0] + keyto;//dt.Rows[i][3] += arrtxt[1] + ",";//类别
                        break;
                        //strtxt = arrtxt[1];//所属类别
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
                WycLuceneSearch.PanguQueryTest(analyzer, field, keyword,richTextBox1);//通过盘古分词搜索
                WycLuceneSearch.PanguQueryTest(analyzer, field, keyto, richTextBox2);
            }
        }
        //创建索引
        private void button3_Click(object sender, EventArgs e)
        {
            bool isPangu = true;
            wznr_Servise wznr = new wznr_Servise();
            Analyzer analyzer = new PanGuAnalyzer();//盘古Analyzer
            DirectoryInfo dirInfo = Directory.CreateDirectory(Config.INDEX_STORE_PATH);
            LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            DataTable dt = wznr.GetDataTable("SELECT Title, p FROM test4Table");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = Convert.ToString(dt.Rows[i][0]);
                string content = Convert.ToString(dt.Rows[i][1]);
                CreateIndex(writer, title, content);
            }
            writer.Optimize();
            writer.Close();
            this.richTextBox1.Text = string.Format("{0}索引创建成功", isPangu ? "盘古分词" : string.Empty);
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
        //删除临时存储数据
        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "delete from test5Table";
            try
            {
                wznr_Servise servise = new wznr_Servise();
                int count = servise.ExecuteSql(sql);
                if (count > 0)
                {
                    MessageBox.Show("数据删除成功！", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        //导入数据
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("不再使用！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //wznr_Servise servise = new wznr_Servise();
            //servicemodel.service model = new servicemodel.service();
            //serviceBLL bll = new serviceBLL();
            //string sql = "select * from 查询 ";
            //DataTable dt = servise.GetDataTable2(sql);
            
            //for (int i = 0; i < dt.Rows.Count; i++)//添加
            //{
            //    model.username = Convert.ToString(dt.Rows[i]["username"]);//患者名称
            //    model.userage =Convert.ToString( dt.Rows[i]["age"]);//患者年龄
               
            //    model.usersex = "不详";//患者性别
            //    model.userphoto = Convert.ToString(dt.Rows[i]["tel"]);//患者电话
            //    model.useraddress = Convert.ToString(dt.Rows[i]["address"]);//患者地址
            //    model.subtime = Convert.ToDateTime(dt.Rows[i]["pubtime"]);//提交时间
            //    model.zhengzhuang = Convert.ToString(dt.Rows[i]["bingzhongbeizhu"]);//病情症状
            //    model.chats = Convert.ToString(dt.Rows[i]["ltrecord"]);//内容
            //    model.eatime = dt.Rows[i]["teltime"] == DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["teltime"]);//通话日期
            //    model.telextract = null;
            //    model.eaname = Convert.ToString(dt.Rows[i]["zbusername"]);//值班人
            //    model.userweb =Convert.ToString(dt.Rows[i]["webaddress"]);//所属网站
            //    model.theirpeople = Convert.ToString(dt.Rows[i]["pubusername"]);//录入人员
            //    model.flag = 0;
            //    model.memo = Convert.ToString(dt.Rows[i]["beizhu"]).Trim();//备注
            //    model.bj = "0";
            //    model.sfzy = "1";
            //    model.typeflag = "1";
            //    model.hfdata = dt.Rows[i]["retime"]== DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["retime"]);//回复日期
            //    model.sfyx = "0";//是否有效
            //    model.zzy = "所属资源是：" + Convert.ToString(dt.Rows[i]["zbusername"]);//所属资源
            //    model.sfdh = "1";
            //    model.deleflag = "0";
            //    model.zhenduan = Convert.ToString(dt.Rows[i]["bingzhong"]);//病种
            //    model.stas = "0";
            //    model.zzystas = "0";
            //    model.lasttime = dt.Rows[i]["teltime"] == DBNull.Value ? Convert.ToDateTime(dt.Rows[i]["pubtime"]) : Convert.ToDateTime(dt.Rows[i]["teltime"]);
            //    model.flagbumen = "门诊部门";
            //    model.xcbumen = "宣传四";
            //    model.kfbumen = "宣传四";
            //    model.bianyuan = "0";
            //    model.urltext = null;
            //    model.bqname = null;
            //    model.byname = Convert.ToString(dt.Rows[i]["username"]);
            //    model.byphone = Convert.ToString(dt.Rows[i]["tel2"]);
            //    model.zydate = Convert.ToDateTime(dt.Rows[i]["pubtime"]);
            //    model.strurl = "没有网址";
            //    model.hotflag = "";
            //    model.menzhenname = Convert.ToString(dt.Rows[i]["username2"]);
            //    model.menzhenphone = Convert.ToString(dt.Rows[i]["tel2"]);
            //    model.kefuname = Convert.ToString(dt.Rows[i]["username"]);
            //    model.kefuphone = Convert.ToString(dt.Rows[i]["tel"]);
            //    model.memo2 = Convert.ToString(dt.Rows[i]["juage3"]);
            //    model.flag2 = "0";
            //    model.jdflag = "0";
            //    model.xcname = null;
            //    model.baobei =Convert.ToDateTime("1987-06-19 00:00:00.000");
            //    bll.Add(model);
            //    this.label8.Text=i.ToString();
            //}
        }

        private void 导入资源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBatchInsert fb = new fBatchInsert();
            Form2WaittingGetTotalRecords form = new Form2WaittingGetTotalRecords();
            form.Show();
            int totalrecord = fb.GetTotalXS(form.GetTotalNum);
            form.Close();
            daoruxuansan daoru = new daoruxuansan();
            daoru.TotalRecords = totalrecord;
            daoru.fBatchInsert = fb;
            daoru.ShowDialog();
        }
        
    }
}
