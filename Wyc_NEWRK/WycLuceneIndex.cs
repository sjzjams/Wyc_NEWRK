using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Wyc_NEWRK
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Util;
    using LuceneIO = Lucene.Net.Store;
    using System.IO;
    public class WycLuceneIndex
    {
        public static void PrepareIndex(bool isPangu)
        {
            Analyzer analyzer = null;
            if (isPangu)
            {
                analyzer = new PanGuAnalyzer();//盘古Analyzer
            }
            else
            {
                analyzer = new StandardAnalyzer(Version.LUCENE_29);
            }
            //测试文章测试
            DirectoryInfo dirInfo = Directory.CreateDirectory(Config.INDEX_STORE_PATH);
            LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            CreateIndex(writer, "jeffreyzhao", "博客园有一个老赵，人格魅力巨大，洋名就叫jeffreyzhao。据我所知，他还是一个胖子，一个钢琴业余爱好者。");
            CreateIndex(writer, "lucene测试", "这是一个测试，关于lucene.net的 关注老赵");
            CreateIndex(writer, "博客园里有牛人", "Hello World. 我认识的一个高手，他拥有广博的知识，有极客的态度，还经常到园子里来看看");
            CreateIndex(writer, "奥巴马", "美国现任总统是奥巴马？确定不是奥巴牛和奥巴羊 不知道问老赵");
            CreateIndex(writer, "奥林匹克", "奥林匹克运动会将来到南美美丽热情的国度巴西，也就是亚马逊河流域的一个地方");
            CreateIndex(writer, "写给自己", "博客园的jeffwong，新的开始，继续努力了");
            writer.Optimize();
            writer.Close();
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
    }
}
