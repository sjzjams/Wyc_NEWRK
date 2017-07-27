using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wyc_NEWRK
{
    public partial class Form2WaittingGetTotalRecords : Form
    {
        public void GetTotalNum(int num)
        {
            label2.Text = num.ToString();
            Application.DoEvents();
        }
        public Form2WaittingGetTotalRecords()
        {
            InitializeComponent();
        }
    }
}
