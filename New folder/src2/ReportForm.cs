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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            refreshReportForm(Report.getCategoryReportList());
        }

        public ReportForm(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            refreshReportForm(Report.getCategoryReportListBetween(startDate, endDate));
        }

        private void refreshReportForm(List<CategoryReport> list)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = Buyings.getNameByCategoryId(list[i].categoryIndex);
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = list[i].categoryTotal.ToString();
            }
        }

    }
}
