using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wyc_NEWRK
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
           Application.Run(new IndexForm());
            //Application.Run(new db3tomysql());
        }
    }
}
