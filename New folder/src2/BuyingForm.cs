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
    public partial class BuyingForm : Form
    {
        private int index;
        private bool edit = false;

        public BuyingForm()
        {
            InitializeComponent();
        }

        public BuyingForm(int index)
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
                    if (comboBox1.SelectedIndex != -1 || MessageBox.Show("Вы не выбрали категорию. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (edit)
                        {
                            Item i = Buyings.FullSet.GetAllItems[index];
                            i.BuyDate = dateTimePicker1.Value;
                            i.CategoryIndex = comboBox1.SelectedIndex;
                            i.Cost = Convert.ToDecimal(textBox2.Text);
                            i.Description = textBox1.Text;
                            i.Comment = richTextBox1.Text;
                        }
                        else
                        {
                            Item i = new Item();
                            i.BuyDate = dateTimePicker1.Value;
                            i.CategoryIndex = comboBox1.SelectedIndex;
                            i.Cost = Convert.ToDecimal(textBox2.Text);
                            i.Description = textBox1.Text;
                            i.Comment = richTextBox1.Text;
                            Buyings.FullSet.GetAllItems.Add(i);
                            
                        }
                        Close();
                    }
                    else
                        DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("Неверно введена сумма!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
        }

        private void Buying_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Buyings.FullSet.GetAllCategories.Count; i++)
                comboBox1.Items.Add(Buyings.FullSet.GetAllCategories[i].Name);
            if (edit)
                comboBox1.SelectedIndex = Buyings.FullSet.GetAllItems[index].CategoryIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void BuyingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(sender,e);
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
