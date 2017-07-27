using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Wyc_NEWRK
{
    public partial class daoruxuansan : Form
    {
        public int TotalRecords { get; set; }
        public int totalmm { get; set; }
        internal fBatchInsert fBatchInsert { get; set; }
        public daoruxuansan()
        {
            InitializeComponent();
        }
        public void GetTotaldaoruxuansan(int current)
        {
                progressBar1.Value = (int)((decimal)(current * 100) / numericUpDown1.Value);
                Application.DoEvents();
        }

        //获取每月数量
        public void Getmday(int current)
        {
            if (current != 0)
            {
                progressBar1.Value = (int)((decimal)(current * 100) / numericUpDown1.Value);
                Application.DoEvents();
            }
        }
        //导入
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                progressBar1.Value = 0;
                Stopwatch sw = new Stopwatch();
                RichTextBox rich = new RichTextBox();
                fBatchInsert.daoruxuansan(GetTotaldaoruxuansan, Getmday,sw, richTextBox1);
                MessageBox.Show(string.Format("导入 {0} 数据成功! 耗时: {1} ms", numericUpDown1.Value, sw.ElapsedMilliseconds));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void daoruxuansan_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = TotalRecords;
            numericUpDown1.Value = TotalRecords;
        }
    }
}
