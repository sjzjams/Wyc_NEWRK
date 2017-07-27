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
    public partial class FormWaittingGetTotalRecords : Form
    {
        public void GetTotalDelegate(int current)
        {
            label2.Text = current.ToString();
            Application.DoEvents();
        }
        public FormWaittingGetTotalRecords()
        {
            InitializeComponent();
        }
    }
}
