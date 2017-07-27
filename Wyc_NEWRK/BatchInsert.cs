using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Threading;
namespace Wyc_NEWRK
{
    public partial class BatchInsert : Form
    {
        public int TotalRecords { get; set; }
        internal fBatchInsert fBatchInsert { get; set; }
        public BatchInsert()
        {
            InitializeComponent();
        }
        public void GetTotalDelegate(int current)
        {
            progressBar1.Value = (int)((decimal)(current * 100) / numericUpDownRecords.Value);
            Application.DoEvents();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                progressBar1.Value = 0;
                Stopwatch sw = new Stopwatch();
                fBatchInsert.fBatchImport(GetTotalDelegate, sw);
                //fBatchInsert.insertsqliteshuju(GetTotalDelegate, sw);
                //int num = new SqliteHelper().GetMaxID("ID", "Content");
                //int pagesize = num / 1000;
                //pagesize = pagesize + 1;
                //for (int i = 0; i < 1; i++)
                //{
                    
                //    fBatchInsert.MyThreadInsert(GetTotalDelegate, sw, i);
                //}
                //fBatchInsert.getTotalRecordsDelegate+= new EventHandler(method_threadStartEvent);
                //fBatchInsert.threadEndEvent += new EventHandler(method_threadEndEvent);
                //Thread thread = new Thread( new ParameterizedThreadStart(fBatchInsert.MyThreadInsert));
                //thread.Start(1);
                MessageBox.Show(string.Format("导入 {0} 数据成功! 耗时: {1} ms", numericUpDownRecords.Value, sw.ElapsedMilliseconds));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            //int total = SubstringCount("abcddafasfasfljlasjgs", "as");
            //MessageBox.Show(string.Format("出现{0}次",total));
        }
       
        private void BatchInsert_Load(object sender, EventArgs e)
        {
            //GetTotalRecordsDelegate();
            numericUpDownRecords.Maximum = TotalRecords;
            numericUpDownRecords.Value = TotalRecords;
            
        }
        /// <summary>
        /// 计算字符串中子串出现的次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="substring">子串</param>
        /// <returns>出现的次数</returns>
        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }

        
    }
}
