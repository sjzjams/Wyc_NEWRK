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
    public partial class hetong : Form
    {
        public hetong()
        {
            InitializeComponent();
        }
        public int type { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            switch (type)
            {
                case 1:
                    
                    path = Application.StartupPath + "\\App_Data\\hetongNo.txt";
                    break;
                case 2:
                    
                    path = Application.StartupPath + "\\App_Data\\yuangongNo.txt";
                    break;
                case 3:
                    
                    path = Application.StartupPath + "\\App_Data\\yuangongNo1.txt";
                    break;
            }


            StreamWriter m_streamWriter = new StreamWriter(path,false, Encoding.GetEncoding("GB2312"));
            m_streamWriter.Flush();
            // 使用StreamWriter来往文件中写入内容
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            //开始写入
            foreach (string line in richTextBox1.Lines)
            {
                m_streamWriter.Write(line + "\n");
            }

            //清空缓冲区
            m_streamWriter.Flush();
            //关闭流
            m_streamWriter.Close();
            MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void hetong_Load(object sender, EventArgs e)
        {
            
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            showLineNo();
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            showLineNo();
        }
        private void showLineNo()
        {
            //获得当前坐标信息
            Point p = this.richTextBox1.Location;
            int crntFirstIndex = this.richTextBox1.GetCharIndexFromPosition(p);

            int crntFirstLine = this.richTextBox1.GetLineFromCharIndex(crntFirstIndex);

            Point crntFirstPos = this.richTextBox1.GetPositionFromCharIndex(crntFirstIndex);

            p.Y += this.richTextBox1.Height;

            int crntLastIndex = this.richTextBox1.GetCharIndexFromPosition(p);

            int crntLastLine = this.richTextBox1.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.richTextBox1.GetPositionFromCharIndex(crntLastIndex);

            //准备画图
            Graphics g = this.panel1.CreateGraphics();

            Font font = new Font(this.richTextBox1.Font, this.richTextBox1.Font.Style);

            SolidBrush brush = new SolidBrush(Color.Green);

            //画图开始

            //刷新画布

            Rectangle rect = this.panel1.ClientRectangle;
            brush.Color = this.panel1.BackColor;

            g.FillRectangle(brush, 0, 0, this.panel1.ClientRectangle.Width, this.panel1.ClientRectangle.Height);

            brush.Color = Color.White;//重置画笔颜色

            //绘制行号

            int lineSpace = 0;

            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);

            }

            else
            {
                lineSpace = Convert.ToInt32(this.richTextBox1.Font.Size);

            }

            int brushX = this.panel1.ClientRectangle.Width - Convert.ToInt32(font.Size * 3);

            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);//惊人的算法啊！！
            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {

                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);

                brushY -= lineSpace;
            }

            g.Dispose();

            font.Dispose();

            brush.Dispose();
        }
    }
}
