using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PanGu;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using System.IO;
using Lucene.Net.Documents;
using System.Diagnostics;
namespace Wyc_NEWRK
{
    public partial class test : Form
    {
        public int TotalRecords { get; set; }
        internal fBatchInsert fBatchInsert { get; set; }
        public test()
        {
            InitializeComponent();
        }
        public void GetTitletotal(int current)
        {
            progressBar1.Value = (int)((decimal)(current * 100) / numericUpDown1.Value);
            Application.DoEvents();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                progressBar1.Value = 0;
                Stopwatch sw = new Stopwatch();
                RichTextBox rich = new RichTextBox();
                fBatchInsert.ShengChengNeiRong(GetTitletotal, sw, richTextBox1);
                //fBatchInsert.CreateIndexImport(GetTotalDelegate, sw, richTextBox1);
                MessageBox.Show(string.Format("导入 {0} 数据成功! 耗时: {1} ms", numericUpDown1.Value, sw.ElapsedMilliseconds));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }            
        }

        private void test_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = TotalRecords;
            numericUpDown1.Value = TotalRecords;
        }
        
    }
}
