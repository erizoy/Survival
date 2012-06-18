#define FULL_VERSION

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Costs
{
    public partial class MainForm : Form
    {

        Font deff;
        Font newf;
        Filter filter;
        bool filterEnabled = false;

        #region MainForm

        public MainForm()
        {
            InitializeComponent();
        }
 
        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists("Serialize.xml"))
                Buyings.FullSet.FullDeserialize();
            RefreshList();
            RefreshFilter();
            dateTimePicker1.Value = Buyings.MinDate();
            dateTimePicker2.Value = DateTime.Now;
            deff = new Font(filterCategoriesListBox.Font, FontStyle.Regular);
            newf = new Font(filterCategoriesListBox.Font, FontStyle.Bold);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Buyings.FullSet != null)
                Buyings.FullSet.FullSerialize();
        }

        #endregion

        #region MainButtons

        private void button3_Click(object sender, EventArgs e)
        {
            (new Categories()).ShowDialog();
            RefreshList(filterEnabled);
        }

        private void showFilterButton_Click(object sender, EventArgs e)
        {

            if (panel1.Visible)
            {
                panel1.Hide();
                dataGridView1.Height += 120;
                filterEnabled = false;
            }
            else
            {
                dataGridView1.Height -= 120;
                panel1.Show();
                filterEnabled = true;
            }
            RefreshList(filterEnabled);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new BuyingForm()).ShowDialog();
            RefreshList(filterEnabled);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new IncomeForm()).ShowDialog();
            RefreshList(filterEnabled);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберете строку или строки, которые хотите удалить.\nДля этого нажмите на ячейку, слева от той операции, которую хотите удалить.\nДля множественного выбора пользуйтесь клавишей ctrl.", "Подсказка!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    Buyings.FullSet.GetAllItems.RemoveAt(r.Index);
                }
                RefreshList(filterEnabled);
            }
        }

        #endregion

        #region FilterPane

        private void button5_Click(object sender, EventArgs e)
        {
            filter = new Filter();
            filter.descriptionSearch = filterDescriptionBox.Text;
            if (String.IsNullOrWhiteSpace(filterStartPriceBox.Text))
                filterStartPriceBox.Text = "0";
            if (String.IsNullOrWhiteSpace(filterEndPriceBox.Text))
                filterEndPriceBox.Text = "0";
            filter.startCost = decimal.Parse(filterStartPriceBox.Text);
            filter.endCost = decimal.Parse(filterEndPriceBox.Text);
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                DateTime temp = dateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker2.Value;
                dateTimePicker2.Value = temp;
            }
            filter.startDate = dateTimePicker1.Value;
            filter.endDate = dateTimePicker2.Value;
            if (filterCategoriesListBox.SelectedIndices.Count != 0)
            {
                for (int i = 0; i < filterCategoriesListBox.Items.Count; i++)
                    filterCategoriesListBox.Items[i].Font = deff;
                foreach (int i in filterCategoriesListBox.SelectedIndices)
                {
                    filter.categoryIndexes.Add(Buyings.getIdByCategoryName(filterCategoriesListBox.Items[i].Text));
                    filterCategoriesListBox.Items[i].Font = newf;
                }
            }
            filterEnabled = true;
            RefreshList(filterEnabled);
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            RefreshFilter();
            RefreshList();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Приготовьтесь к погружению в море порно-роликов, предоставленных порталом www.youporn.com(Все права защищены)");
            System.Diagnostics.Process.Start("www.youporn.com");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 44 || e.KeyChar == 8))
                e.Handled = true;
        }

        #endregion
        
        #region Refreshing Procedures
        /// <summary>
/// Same as RefreshList(bool filter) with the parameter set false.
/// Prints all items in list.
/// </summary>
        private void RefreshList()
        {
            RefreshList(false);
        }
/// <summary>
/// Refreshes list of items specified by filter or not.
/// </summary>
/// <param name="filter">Enables filtering.</param>
        private void RefreshList(bool filter)
        {
            List<Item> list;
            if (filter)
                list = this.filter.getItemsSpecifiedBy();
            else
                list = Buyings.FullSet.GetAllItems;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows.Add();
                if (list[i].CategoryIndex == Income.INDEX)
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGreen;
                else
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightPink;
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = list[i].Category;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = list[i].Description;
                dataGridView1[1, dataGridView1.Rows.Count - 1].ToolTipText = list[i].Comment;
                dataGridView1[2, dataGridView1.Rows.Count - 1].Value = list[i].Cost;
                dataGridView1[3, dataGridView1.Rows.Count - 1].Value = list[i].BuyDate.ToShortDateString();
            }
        }

/// <summary>
/// Refreshes filter object and fields in Filter Panel
/// </summary>
        private void RefreshFilter()
        {
            filterEnabled = false;
            filter = new Filter();
            filterDescriptionBox.Text = "";
            filterStartPriceBox.Text = "0";
            filterEndPriceBox.Text = "0";
            dateTimePicker1.Value = Buyings.MinDate();
            dateTimePicker2.Value = DateTime.Now;
            filterCategoriesListBox.Clear();
            filterCategoriesListBox.Items.Add(Income.Name);
            for (int i = 0; i < Buyings.FullSet.GetAllCategories.Count; i++)
                filterCategoriesListBox.Items.Add(Buyings.FullSet.GetAllCategories[i].Name);
        }
        #endregion

        /// <summary>
        /// Looking for an item with the same fileds
        /// </summary>
        /// <param name="Category">Item category</param>
        /// <param name="Discription">Item description</param>
        /// <param name="Cost">Item price</param>
        /// <param name="BuyDate">Item buy date</param>
        /// <returns></returns>
        private int FindItem(string Category, string Discription, decimal Cost, DateTime BuyDate)
        {
            for (int i = 0; i < Buyings.FullSet.GetAllItems.Count; i++)
            {
                if (Buyings.FullSet.GetAllItems[i].Category == Category && Buyings.FullSet.GetAllItems[i].Description == Discription && Buyings.FullSet.GetAllItems[i].Cost == Cost && Buyings.FullSet.GetAllItems[i].BuyDate == BuyDate)
                    return i;
            }
            return -1;
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
	            {
	                string Category = (string)dataGridView1[0, e.RowIndex].Value;
	                string Discription = (string)dataGridView1[1, e.RowIndex].Value;
	                decimal Cost = (decimal)dataGridView1[2, e.RowIndex].Value;
	                DateTime BuyDate = Convert.ToDateTime((string)dataGridView1[3, e.RowIndex].Value);
                    if ((string)dataGridView1[0, e.RowIndex].Value != Income.Name)
                    {
                        if (new BuyingForm(FindItem(Category, Discription, Cost, BuyDate)).ShowDialog() == DialogResult.OK)
	                        RefreshList(filterEnabled);
                    }
                    else
                    {
                        if (new IncomeForm(FindItem(Category, Discription, Cost, BuyDate)).ShowDialog() == DialogResult.OK)
                            RefreshList(filterEnabled);
                    }   
	            }
            }
        }
        
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
/*            if (Filter.filter.ColoumnIndex == e.ColumnIndex)
                if (Filter.filter.Direction == ListSortDirection.Descending)
                    Filter.filter.Direction = ListSortDirection.Ascending;
                else
                    Filter.filter.Direction = ListSortDirection.Descending;
            else
                Filter.filter.Direction = ListSortDirection.Ascending;
            Filter.filter.ColoumnIndex = e.ColumnIndex;*/
        }

        #region Menu Файл
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Menu Действие
#if FULL_VERSION
        private void заВсеВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ReportForm()).ShowDialog();
        }
        private void заПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new TimePeriod(0)).ShowDialog();
        }
#else
       private void заВсеВремяToolStripMenuItem_Click(object sender, EventArgs e)
       {
            MessageBox.Show("Sorry, this function will be available in full version");
       }
        private void заПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, this function will be available in full version");
        }
#endif
        private void расходЗаУказанныйПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new TimePeriod(1)).ShowDialog();
        }

        private void расходЗаВсеВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ViewDiagramForm()).ShowDialog();
        }
        #endregion
 
        #region Помощь
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog();
        }
        #endregion

        #region TO_XLS
#if FULL_VERSION
        private void вывестиВТаблицуExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FullPathSVFL_XLS=" ";
            string stmp;
            SVFLDG_XLS.Filter = "XLS files (*.xls)|*.xls |All files (*.*)|*.*";
            SVFLDG_XLS.FileName = "Budget checkout.xls";
            SVFLDG_XLS.FilterIndex = 1;
            if (SVFLDG_XLS.ShowDialog() == DialogResult.OK)
            {
                FullPathSVFL_XLS = SVFLDG_XLS.FileName;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range chartRange;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;

                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 3, j + 1] = cell.Value;
                    }
                    stmp = "D" + (i + 3).ToString();
                    string s = "A" + (i + 3).ToString();
                    chartRange = xlWorkSheet.get_Range(s, stmp);
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.LightGreen)
                        chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.LightPink)
                        chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightPink);
                }

                xlWorkSheet.get_Range("a1", "d2").Merge(false);
                chartRange = xlWorkSheet.get_Range("a1", "d2");
                chartRange.FormulaR1C1 = "Budget sheet";
                chartRange.Font.Size = 16;
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                stmp = "D" + (i + 2).ToString();
                chartRange = xlWorkSheet.get_Range("A3", stmp);
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline,
                    Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.get_Range("a3", stmp);
                chartRange.Columns.AutoFit();

                try
                {
                    xlWorkBook.SaveAs(FullPathSVFL_XLS, Excel.XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please, close all aplication using this xls file or enter another name");
                    throw ex;
                }

                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                MessageBox.Show("Excel file created here --> " + FullPathSVFL_XLS);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

#else
       private void вывестиВТаблицуExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, but this function will be available in full version");
        }
#endif
 /*
        private void вывестиВТаблицуExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ooops");
        }*/
        #endregion
        
    }
}
