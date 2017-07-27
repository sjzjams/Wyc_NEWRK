using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wyc_NEWRK
{
    public partial class IndexForm : Form
    {
        public IndexForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 根据合同编号导出身份证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Lines.Length>0)
            {
                richTextBox1.Text = "";
            }
            wznr_Servise wznr = new wznr_Servise();
            string line;
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\hetongNo.txt";
            //读取文本内容逐行
            StreamReader file = new StreamReader(txtpath);
            DataTable dt= wznr.GetDataTable("select Name,IDNo,ContractCode from Financial f inner join InvestmentInfo i on f.FinancialId=i.FinancialId");
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {

                DataRow[] rows = dt.Select("ContractCode='"+line+"'");
                if(rows.Count()>0)
                {
                    i++;
                    richTextBox1.AppendText(string.Format("序号：{0},客户姓名：{3},该合同编号：{1},对应身份证为：{2} \n", i, line, rows[0][1],rows[0][0]));
                    richTextBox1.ForeColor = Color.Green;
                    richTextBox1.Focus();
                }else
                {
                    i++;
                    richTextBox1.AppendText(string.Format("序号：{0},客户姓名：无,该合同编号：{1}无此记录,对应身份证为：无 \n", i, line));
                    richTextBox1.ForeColor = Color.Red;
                    richTextBox1.Focus();
                }
                

            }
            file.Close();

        }
        /// <summary>
        /// 根据员工编号导出身份证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Lines.Length > 0)
            {
                richTextBox2.Text = "";
            }
            wznr_Servise wznr = new wznr_Servise();
            string line;
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\yuangongNo.txt";
            //读取文本内容逐行
            StreamReader file = new StreamReader(txtpath);
            DataTable dt = wznr.GetDataTable("select CardNo,Dept_Id,Name,Phone,IdentityCard from HrManagementDB..UserInfo  where Dept_Id in(select Id from HrManagementDB..DepartmentInfo  where FAld='31DC10A8-B578-4AE0-B7F2-5FB2D4BBF083')");
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {

                DataRow[] rows = dt.Select("CardNo='"+line+"'");
                if (rows.Count() > 0)
                {
                    i++;
                    richTextBox2.AppendText(string.Format("序号：{0},员工姓名：{1},员工编号：{2},对应身份证为：{3} \n", i, rows[0][2], rows[0][0],rows[0][4]));
                    richTextBox2.ForeColor = Color.Green;
                    richTextBox2.Focus();
                }
                else
                {
                    i++;
                    richTextBox2.AppendText(string.Format("序号：{0},员工姓名：无,员工编号：{1}无此记录,对应身份证为：无 \n", i, line));
                    richTextBox2.ForeColor = Color.Red;
                    richTextBox2.Focus();
                }


            }
        }
        /// <summary>
        /// 理财经理工号导出身份证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Lines.Length > 0)
            {
                richTextBox3.Text = "";
            }
            wznr_Servise wznr = new wznr_Servise();
            string line;
            //获取路径,循环每次都需要读取文本文件里设置的关键词 
            string path = Directory.GetCurrentDirectory();
            string txtpath = path + @"\App_Data\yuangongNo1.txt";
            //读取文本内容逐行
            StreamReader file = new StreamReader(txtpath);
            DataTable dt = wznr.GetDataTable("select CardNo,Dept_Id,Name,Phone,IdentityCard from HrManagementDB..UserInfo  where Dept_Id in(select Id from HrManagementDB..DepartmentInfo  where FAld='31DC10A8-B578-4AE0-B7F2-5FB2D4BBF083')");
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {

                DataRow[] rows = dt.Select("CardNo='" + line + "'");
                if (rows.Count() > 0)
                {
                    i++;
                    richTextBox3.AppendText(string.Format("序号：{0},员工姓名：{1},员工编号：{2},对应身份证为：{3} \n", i, rows[0][2], rows[0][0], rows[0][4]));
                    richTextBox3.ForeColor = Color.Green;
                    richTextBox3.Focus();
                }
                else
                {
                    i++;
                    richTextBox3.AppendText(string.Format("序号：{0},员工姓名：无,员工编号：{1}无此记录,对应身份证为：无 \n", i, line));
                    richTextBox3.ForeColor = Color.Red;
                    richTextBox3.Focus();
                }


            }
        }

        private void 设置合同txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开合同文件进行编辑
            hetong ht = new hetong();
            ht.type = 1;
            ht.Text = "设置合同编号";
            ht.ShowDialog();
            
        }

        private void 设置员工编号txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //设置团队经理工号
            hetong ht = new hetong();
            ht.type = 2;
            ht.Text = "设置团队经理工号";
            ht.ShowDialog();
            // MessageBox.Show("设置团队经理工号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 设置理财经理工号txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //设置理财经理工号
            hetong ht = new hetong();
            ht.type = 3;
            ht.Text = "设置理财经理工号";
            ht.ShowDialog();
            //MessageBox.Show("设置理财经理工号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
