using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ADOX;
using System.IO;
namespace Wyc_NEWRK
{
    public partial class Output : Form
    {
        public int TotalRecords { get; set; }
        internal fBatchInsert fBatchInsert { get; set; }
        public Output()
        {
            InitializeComponent();
        }
        public void GetTotalDelegateOutPut(int current)
        {
            progressBar1.Value = (int)((decimal)(current * 100) / numericUpDown1.Value);
            Application.DoEvents();
        }
        //导出数据
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                progressBar1.Value = 0;
                Stopwatch sw = new Stopwatch();
                RichTextBox rich = new RichTextBox();
                fBatchInsert.OutPut(GetTotalDelegateOutPut, sw,richTextBox1);
                MessageBox.Show(string.Format("导入 {0} 数据成功! 耗时: {1} ms", numericUpDown1.Value, sw.ElapsedMilliseconds));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Output_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = TotalRecords;
            numericUpDown1.Value = TotalRecords;
        }
        //导出access数据库
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                progressBar1.Value = 0;
                Stopwatch sw = new Stopwatch();
                RichTextBox rich = new RichTextBox();
                fBatchInsert.access_OutPut(GetTotalDelegateOutPut, sw, richTextBox1);
                MessageBox.Show(string.Format("导入 {0} 数据成功! 耗时: {1} ms", numericUpDown1.Value, sw.ElapsedMilliseconds));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dbn = System.AppDomain.CurrentDomain.BaseDirectory + "Access_Data\\" + "SpiderResult.mdb";//数据库文件名称
            //// 创建数据库文件  
            File.Delete(dbn);
            ADOX.Catalog catalog = new Catalog();
            catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbn + ";Jet OLEDB:Engine Type=5");
            ADOX.Table table = new ADOX.Table();
            table.Name = "Content";
            ADOX.Column column = new ADOX.Column();
            column.ParentCatalog = catalog;
            column.Name = "ID";
            column.Type = DataTypeEnum.adInteger;
            column.DefinedSize = 9;
            column.Properties["AutoIncrement"].Value = true;
            table.Columns.Append(column, DataTypeEnum.adInteger, 9);
            table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "ID", "", "");
            table.Columns.Append("已采", DataTypeEnum.adBoolean, 0);
            table.Columns.Append("已发", DataTypeEnum.adBoolean, 0);
            table.Columns.Append("标题", DataTypeEnum.adVarWChar, 0);
            table.Columns.Append("内容", DataTypeEnum.adVarWChar, 0);
            table.Columns.Append("PageUrl", DataTypeEnum.adVarWChar, 0);
            catalog.Tables.Append(table);
            
            MessageBox.Show(string.Format("创建成功"));
        }
    }
}
