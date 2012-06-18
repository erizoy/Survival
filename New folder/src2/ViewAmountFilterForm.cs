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
    public partial class ViewAmountFilterForm : Form
    {
        public decimal left = 0;
        public decimal right = 0;
        public bool ok = false;

        public ViewAmountFilterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal d1 = 0;
            decimal d2 = 0;
            if (decimal.TryParse(textBox1.Text, out d1) && decimal.TryParse(textBox2.Text, out d2))
            {
                if (d1 >= 0 && d2 >= 0)
                {
                    if (d1 > d2)
                    {
                        left = d1;
                        right = d2;
                        ok = true;
                        Close();
                    }
                    else
                        MessageBox.Show("Левая граница не может быть больше правой!","Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Цена не может быть отрицательной!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Неверно введены числа!\nФормат чисел: xx,yy", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ViewAmountFilterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(null,null);
            if (e.KeyValue == 27)
                Close();
        }
    }
}
