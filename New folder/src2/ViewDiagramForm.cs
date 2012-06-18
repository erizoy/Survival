using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace Costs
{
    public partial class ViewDiagramForm : Form
    {
        List<CategoryReport> list;

        public ViewDiagramForm()
        {
            InitializeComponent();
            list = Report.getCategoryReportList();
        }

        public ViewDiagramForm(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            list = Report.getCategoryReportListBetween(startDate, endDate);
        }

        void drawdiagramm(List<CategoryReport> list)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            int margin = 50;
            int step = (this.Width - 2 * margin) / list.Count;
            int width = step * 2 / 3;
            int bottomLine = this.Height - margin;
            int maxHeight = this.Height - 2 * margin;
            int maxWidth = this.Width - 2 * margin;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen p = new Pen(Color.Black);
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p, margin / 2, bottomLine, margin / 2, margin / 2);
            g.DrawLine(p, margin / 2, bottomLine, this.Width - margin / 2,bottomLine);
            decimal maxSum = 0;
            foreach (CategoryReport c in list) 
                if (c.categoryTotal > maxSum) 
                    maxSum = c.categoryTotal; 
            int height;
            for (int i = 0; i < list.Count; i++)
            {
                height = (int)(list[i].categoryTotal / maxSum * maxHeight);
                g.FillRectangle(Buyings.getBrushByCategoryId(list[i].categoryIndex), i * step + margin, bottomLine - height, width, height);
            }
        }

        private void ViewDiagramForm_Shown(object sender, EventArgs e)
        {
            drawdiagramm(list);
        }
    }
}
