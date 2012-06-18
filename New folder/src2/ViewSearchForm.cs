using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Costs
{
    public partial class ViewSearchForm : Form
    {
        public bool ok = false;

        public ViewSearchForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void ViewSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ok = true;
                Close();
            }
            if (e.KeyValue == 27)
                Close();
        }
    }
}
