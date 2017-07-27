using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Wyc_NEWRK
{
    public partial class db3tomysql : Form
    {
        public db3tomysql()
        {
            InitializeComponent();
        }

        //线程开始的时候调用的委托
        private delegate void maxValueDelegate(int maxValue);
        //线程执行中调用的委托
        private delegate void nowValueDelegate(int nowValue);

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadMethod method = new ThreadMethod();
            //先订阅一下事件
            method.threadStartEvent += new EventHandler(method_threadStartEvent);
            method.threadEvent += new EventHandler(method_threadEvent);
            method.threadEndEvent += new EventHandler(method_threadEndEvent);
            int num = new SqliteHelper().GetMaxID("ID", "Content");
            int pagesize = num / 1000;
            pagesize = pagesize + 1;
            for (int i = 0; i < pagesize; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(method.runMethod));
                thread.Start(i);
            }
        }

        /// <summary>
        /// 线程完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void method_threadEndEvent(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 线程执行中的事件,设置进度条当前进度
        /// 但是我不能直接操作进度条,需要一个委托来替我完成
        /// </summary>
        /// <param name="sender">ThreadMethod函数中传过来的当前值</param>
        /// <param name="e"></param>
        void method_threadEvent(object sender, EventArgs e)
        {
            int nowValue = Convert.ToInt32(sender);
            nowValueDelegate now = new nowValueDelegate(setNow);
            this.Invoke(now, nowValue);
        }

        /// <summary>
        /// 线程开始事件,设置进度条最大值
        /// 但是我不能直接操作进度条,需要一个委托来替我完成
        /// </summary>
        /// <param name="sender">ThreadMethod函数中传过来的最大值</param>
        /// <param name="e"></param>
        void method_threadStartEvent(object sender, EventArgs e)
        {
            int maxValue = Convert.ToInt32(sender);
            maxValueDelegate max = new maxValueDelegate(setMax);
            this.Invoke(max, maxValue);
        }

        /// <summary>
        /// 我被委托调用,专门设置进度条最大值的
        /// </summary>
        /// <param name="maxValue"></param>
        private void setMax(int maxValue)
        {
            this.progressBar1.Maximum = maxValue;
        }

        /// <summary>
        /// 我被委托调用,专门设置进度条当前值的
        /// </summary>
        /// <param name="nowValue"></param>
        private void setNow(int nowValue)
        {
            this.progressBar1.Value = nowValue;
        }
    }
}
