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
    
    public partial class IncomeForm : Form
    {
        private int index;
        private bool edit = false;

        public IncomeForm()
        {
            InitializeComponent();
        }
        public IncomeForm(int index)
        {
            InitializeComponent();
            dateTimePicker1.Value = Convert.ToDateTime(Buyings.FullSet.GetAllItems[index].BuyDate);
            textBox1.Text = Buyings.FullSet.GetAllItems[index].Description;
            textBox2.Text = Buyings.FullSet.GetAllItems[index].Cost.ToString();
            richTextBox1.Text = Buyings.FullSet.GetAllItems[index].Comment;
            edit = true;
            this.index = index;
            button1.Text = "Редактировать";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") textBox2.Text = "0";
            if (Convert.ToDecimal(textBox2.Text) > 0)
            {
                if (edit)
                {
                    Item i = Buyings.FullSet.GetAllItems[index];
                    i.BuyDate = dateTimePicker1.Value;
                    i.Cost = Convert.ToDecimal(textBox2.Text);
                    i.Description = textBox1.Text;
                    i.Comment = richTextBox1.Text;
                }
                else
                {
                    Item i = new Item();
                    i.BuyDate = dateTimePicker1.Value;
                    i.CategoryIndex = Income.INDEX;
                    i.Cost = Convert.ToDecimal(textBox2.Text);
                    i.Description = textBox1.Text;
                    i.Comment = richTextBox1.Text;
                    Buyings.FullSet.GetAllItems.Add(i);
                }
                Close();
            }
            else
                MessageBox.Show("Неверно введена сумма!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void IncomeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(null, null);
            if (e.KeyValue == 27)
                Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 44 || e.KeyChar == 8))
                e.Handled = true;
        }
    }
}
