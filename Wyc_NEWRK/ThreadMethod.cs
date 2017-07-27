using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;

namespace Wyc_NEWRK
{
    public class ThreadMethod
    {
        /// <summary>
        /// 线程开始事件
        /// </summary>
        public event EventHandler threadStartEvent;
        /// <summary>
        /// 线程执行时事件
        /// </summary>
        public event EventHandler threadEvent;
        /// <summary>
        /// 线程结束事件
        /// </summary>
        public event EventHandler threadEndEvent;

        public void runMethod(object i)
        {
            FenCiHelper fch = new FenCiHelper();
            wznr_Servise wznr = new wznr_Servise();
            
            DataTable dt = new DataTable();
           
                string sqlitesql = "SELECT  标题,内容 FROM  content ORDER BY id asc LIMIT 1000 OFFSET (1000*'" + i + "')";
                //dtsqllist.Add(sqlitesql);
                dt = new SqliteHelper().GetQuery(sqlitesql);//查询sqlite数据库new SqliteHelper().gettablequert(dtsqllist); //
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("keys", typeof(string)), new DataColumn("bztype", typeof(string)) });//添加新列
                DataRow dr = dt.NewRow();//实例化新行
                threadStartEvent.Invoke(dt.Rows.Count, new EventArgs());//通知主界面,我开始了,count用来设置进度条的最大值
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    Thread.Sleep(0);//0毫秒过去后从新计算优先级
                    threadEvent.Invoke(k, new EventArgs());//通知主界面我正在执行,i表示进度条当前进度
                    string title = Convert.ToString(dt.Rows[k][0]);//获取到标题
                    string content = Convert.ToString(dt.Rows[k][1]);//获取到内容
                    //调用PanguFenCi（aa）进行分词添加到datatable中批量录入到关键词表中。
                    title = fch.PanguFenCi(title);
                    //调用PanGuContentFenCi（content）进行分词同时输出同义词
                    dt.Rows[k][1] = content;
                    dt.Rows[k][2] = title;
                    dt.Rows[k][3] = 1;
                }
                threadEndEvent.Invoke(new object(), new EventArgs());//通知主界面我已经完成了
                string sql = "insert into test3Table (Title,Content,keys,bztype)" +
               " SELECT  nc.Title,nc.Content,nc.keys,nc.bztype" +
               " FROM @NewBulkTestTvp AS nc";
                wznr.TableValuedToDB(dt, sql, "dbo.test3Udt");
            
        }
    }
}
