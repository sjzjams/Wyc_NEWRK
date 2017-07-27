using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using System.IO;

using PanGu;
using System.Diagnostics;
namespace Wyc_NEWRK
{
    public class FenCiHelper
    {
        //分词引用dll、Lucene.Net 一元分词
        public  string TestLowerCaseTokenizer(string text)
        {

            string fenci = "";
            TextReader tr = new StringReader(text);
            SimpleAnalyzer sA = new SimpleAnalyzer();
            //SimpleAnalyzer使用了LowerCaseTokenizer分词器
            TokenStream ts = sA.TokenStream("", tr);
            Lucene.Net.Analysis.Token t;
            while ((t = ts.Next()) != null)
            {
                fenci += t.TermText() + ",";
            }
            return fenci;
        }
        //用盘古标题分词
        public  string PanguFenCi(string ci)
        {
            string cizu = "";
            PanGu.Segment.Init();//初始化
            Segment seg = new Segment();
            if (seg != null)
            {

                ICollection<WordInfo> words = seg.DoSegment(ci);//分词
                foreach (Object item in words)
                {
                    cizu += item.ToString();//+ "/";
                    //item.ToString();
                }
                //cizu=TestLowerCaseTokenizer(cizu);
                // Console.WriteLine(cizu);
            }
            return cizu;

        }
        //盘古分词同时输出标题中的同义词
        public string PanGuFenCiTYC(string str)
        {
            string strtext = "";
            //初始化
            PanGu.Segment.Init();
            Segment seg = new Segment();
            PanGu.Match.MatchOptions m = new PanGu.Match.MatchOptions();//设置分词属性
            m.ChineseNameIdentify = true;//中文人名识别
            m.SynonymOutput = true;//同义词输出
            m.MultiDimensionality = true;//多元分词
            m.FilterStopWords = true;//过滤停用词
            m.IgnoreSpace = false;//忽略空格、回车、Tab
            if (seg != null)
            {
                ICollection<WordInfo> words = seg.DoSegment(str,m);//内容分词处理
                foreach (Object item in words)
                {
                    //item.ToString();分词,根据词替换
                    strtext += item.ToString();
                }
            }
            return str;
        }
        //盘古内容处理输出同义词
        public  string PanGuContentFenCi(string content)
        {
            string contentntext = "";
            PanGu.Segment.Init();//初始化
            Segment seg = new Segment();
            PanGu.Match.MatchOptions m = new PanGu.Match.MatchOptions();//参数二 m 为自定义分词选项
            m.ChineseNameIdentify = true;//中文人名识别
            m.SynonymOutput = true;//同义词输出
            m.MultiDimensionality = true;//多元分词
            m.FilterStopWords = true;//过滤停用词
            m.IgnoreSpace = false;//忽略空格、回车、Tab
            if (seg != null)
            {
                ICollection<WordInfo> words = seg.DoSegment(content, m);//内容分词处理
                foreach (Object item in words)
                {
                    //item.ToString();分词,根据词替换
                    contentntext += item.ToString() ;
                }
            }
            return contentntext;
        }
        //读取本地文本文件一行一个，主要用于判断分词所属类别
        public  void ReadLineTxt()
        {
            int counter = 0;
            string line;
            //StreamReader file = new StreamReader();
            Stopwatch sw = new Stopwatch();
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\Data\sDict.txt";
            //StreamReader file = new StreamReader(path+@"\keytxt.txt");
            StreamReader file = new StreamReader(txtpath);
            sw.Start();
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                counter++;
            }

            file.Close();
            sw.Stop();
            Console.WriteLine("有 {0} 行.经过时间{1}毫秒", counter, sw.ElapsedMilliseconds);
            // Suspend the screen.
            Console.ReadLine();

        }
        //读取Wyc_UI\bin\Debug\App_Data\Data\下文本
        public  void RealTxt(string strtxt)
        {
            //int count = 0;//计数器，只判断相同内容一次
            string line;
            //获取路径
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\Data\sDict.txt";
            //读取文本内容一行
            StreamReader file = new StreamReader(txtpath);
            while ((line = file.ReadLine()) != null)
            {
                string[] arr = line.Split(',');
                if (arr[0].Equals(strtxt))
                {

                    line = arr[0];
                    strtxt = arr[1];//所属类别
                    Console.WriteLine("病种名称：{0},病种类别：{1}", line, strtxt);
                    break;
                }

            }
            file.Close();
            //return strtxt;
        }
    }
}
