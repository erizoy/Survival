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
    public partial class CategoryForm : Form
    {
        private bool edit = false;
        private Category currentCategory;

        public CategoryForm()
        {
            InitializeComponent();
            currentCategory = new Category();
            colorDialog1.Color = Color.Blue;
            Graphics.FromImage(pictureBox1.Image).Clear(colorDialog1.Color);
        }

        public CategoryForm(int index)
        {
            InitializeComponent();
            currentCategory = Buyings.FullSet.GetAllCategories[index];
            textBox1.Text = currentCategory.Name;
            textBox2.Text = currentCategory.Description;
            colorDialog1.Color = currentCategory.getColor();
            Graphics.FromImage(pictureBox1.Image).Clear(colorDialog1.Color);
            this.edit = true;
            button1.Text = "Редактировать";
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (String.IsNullOrEmpty(textBox1.Text))
                    MessageBox.Show("Введите название категории", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (exists(textBox1.Text)&&currentCategory.Name!=textBox1.Text)
                    MessageBox.Show("Категория с таким именем уже есть", "Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error);  
                else 
                {
                    currentCategory.Name = textBox1.Text;
                    currentCategory.Description = textBox2.Text;
                    currentCategory.setColor(colorDialog1.Color);
                    if (!edit)
                    {
                        Buyings.FullSet.GetAllCategories.Add(currentCategory);
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();       
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void CategoryFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(sender, e);
            if (e.KeyValue == 27)
                Close();
        }

        private bool exists(string name)
        {
            if (name == Income.Name)
                return true;
            if (name == Category.NAMEUNDEFINED)
                return true;
            foreach (Category c in Buyings.FullSet.GetAllCategories)
                if (c.Name == name) return true;
            return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Graphics.FromImage(pictureBox1.Image).Clear(colorDialog1.Color);
            pictureBox1.Invalidate();
        }
    }
}
