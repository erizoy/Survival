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
    public partial class TimePeriod:Form 
    {
        int param;
        public TimePeriod(int param)
        {
            InitializeComponent();
            dateTimePicker1.Value = Buyings.MinDate();
            this.param = param;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
                switch (param)
                {
                    case 0: (new ReportForm(dateTimePicker1.Value, dateTimePicker2.Value)).ShowDialog(); break;
                    default: (new ViewDiagramForm(dateTimePicker1.Value, dateTimePicker2.Value)).ShowDialog(); break;
                }
            else
                MessageBox.Show("Start date must be less then end date!!");
            this.Close();
        }
        
    }
}
