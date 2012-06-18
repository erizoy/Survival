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
    public partial class ViewDateFilterForm : Form
    {
        public DateTime left = DateTime.MinValue;
        public DateTime right = DateTime.MaxValue;
        public bool ok = false;

        public ViewDateFilterForm()
        {
            InitializeComponent();
        }

        private void ViewDateFilterForm_Load(object sender, EventArgs e)
        {
            DateTime minDT = Buyings.FullSet.GetAllItems[0].BuyDate;
            for (int i = 1; i < Buyings.FullSet.GetAllItems.Count; i++)
            {
                if (DateTime.Compare(minDT, Buyings.FullSet.GetAllItems[i].BuyDate) == 1)
                    minDT = Buyings.FullSet.GetAllItems[i].BuyDate;
            }
            dateTimePicker1.Value = minDT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            left = dateTimePicker1.Value;
            right = dateTimePicker2.Value;
            ok = true;
            Close();
        }

        private void ViewDateFilterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                left = dateTimePicker1.Value;
                right = dateTimePicker2.Value;
                ok = true;
                Close();
            }
            if (e.KeyValue == 27)
                Close();
        }
    }
}
