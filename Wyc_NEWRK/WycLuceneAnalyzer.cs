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
    public class WycLuceneAnalyzer
    {
        /// <summary>
        /// 构造常见的几种Analyzer列表
        /// </summary>
        /// <returns></returns>
        public static IList<Analyzer> BuildAnalyzers()
        {
            IList<Analyzer> listAnalyzer = new List<Analyzer>()
            {
                new PanGuAnalyzer(),//盘古分词器  provide by eaglet http://pangusegment.codeplex.com/
                
                //new StandardAnalyzer(Version.LUCENE_29),
                //new WhitespaceAnalyzer(),
                //new KeywordAnalyzer(),
                //new SimpleAnalyzer(),
                //new StopAnalyzer(Version.LUCENE_29),
            };
            return listAnalyzer;
        }
    }
}
