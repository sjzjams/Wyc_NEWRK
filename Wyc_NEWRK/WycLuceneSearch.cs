using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wyc_NEWRK
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Util;
    using PanGu;
    public class WycLuceneSearch
    {
        #region 辅助方法

        /// <summary>
        /// 显示搜索表达式
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="query"></param>
        /// <param name="keyword"></param>
        private static void ShowQueryExpression(Analyzer analyzer, Query query, string keyword,System.Windows.Forms.RichTextBox rich)
        {
           //rich.Text = string.Format("{0},Current Keywords:{1}", analyzer.GetType().Name, keyword)+"\n"+query.ToString()+"\n";
            rich.Text = string.Format("{0}", keyword) + "\n";
            //Console.WriteLine("{0},Current Keywords:{1}", analyzer.GetType().Name, keyword);
            //Console.WriteLine(query.ToString()); //显示搜索表达式
        }
        

        /// <summary>
        /// 搜索并显示结果
        /// </summary>
        /// <param name="query"></param>
        private static void SearchToShow(Query query,System.Windows.Forms.RichTextBox rich)
        {
            int n = 6;//最多返回多少个结果  
            TopDocs docs = Config.GenerateSearcher().Search(query, (Filter)null, n);
            ShowSearchResult(docs,rich);
        }

        /// <summary>
        /// 显示搜索结果
        /// </summary>
        /// <param name="queryResult"></param>
        private static void ShowSearchResult(TopDocs queryResult,System.Windows.Forms.RichTextBox rich)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                rich.Text = "Sorry，没有搜索到你要的结果。";
               // Console.WriteLine("Sorry，没有搜索到你要的结果。");
                return;
            }

            int counter = 1;
            
            foreach (ScoreDoc sd in queryResult.scoreDocs)
            {
                try
                {
                    Document doc = Config.GenerateSearcher().Doc(sd.doc);
                    string title = doc.Get("title");
                    string contents = doc.Get("contents");
                    string createdate = doc.Get("createdate");
                    //string result = string.Format("这是第{0}个搜索结果,title为{1},createdate:{2}，content:{3}{4} \n", counter, title, createdate, Environment.NewLine, contents);
                    //Console.WriteLine();
                    //Console.WriteLine(result);
                    rich.Text += contents+"\n"; //result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                counter++;
            }
        }

        #endregion

        #region 盘古分词搜索

        public static void PanguQueryTest(Analyzer analyzer, string field, string keyword, System.Windows.Forms.RichTextBox rich)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_29, field, analyzer);
            string panguQueryword = GetKeyWordsSplitBySpace(keyword,new PanGuTokenizer());//对关键字进行分词处理
            
            Query query = parser.Parse(panguQueryword);
            ShowQueryExpression(analyzer, query, keyword,rich);//显示搜索表达式
            SearchToShow(query,rich);//显示搜索结果
            //Console.WriteLine();
        }

        public static string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();
            
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }

        #endregion
    }
}
