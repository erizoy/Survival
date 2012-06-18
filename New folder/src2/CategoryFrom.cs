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
    public partial class CategoryFrom : Form
    {
        private int index;
        private bool edit = false;

        public CategoryFrom()
        {
            InitializeComponent();
        }
        public CategoryFrom(int index)
        {
            InitializeComponent();
            textBox1.Text = Buyings.FullSet.GetAllCategories[index].Name;
            textBox2.Text = Buyings.FullSet.GetAllCategories[index].Description;
            edit = true;
            this.index = index;
            button1.Text = "Редактировать";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                bool check = true;
                for (int j = 0; j < Buyings.FullSet.GetAllCategories.Count; j++)
                {
                    if (Buyings.FullSet.GetAllCategories[j].Name == textBox1.Text)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    for (int i = 0; i < Buyings.FullSet.GetAllItems.Count; i++)
                    {
                        if (Buyings.FullSet.GetAllItems[i].Category == Buyings.FullSet.GetAllCategories[index].Name)
                        {
                            Buyings.FullSet.GetAllItems[i].Category = textBox1.Text;
                        }
                    }
                    Buyings.FullSet.GetAllCategories[index].Name = textBox1.Text;
                    Buyings.FullSet.GetAllCategories[index].Description = textBox2.Text;
                    this.Close();
                }
                else if (Buyings.FullSet.GetAllCategories[index].Name == textBox1.Text && Buyings.FullSet.GetAllCategories[index].Description == textBox2.Text)
                {
                    this.Close();
                }
                else if (Buyings.FullSet.GetAllCategories[index].Description != textBox2.Text)
                {
                    Buyings.FullSet.GetAllCategories[index].Description = textBox2.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Категория с таким именем уже есть", "Ошибка!");
                }
            }
            else
            {
                Category c = new Category();
                c.Name = textBox1.Text;
                c.Description = textBox2.Text;
                bool check = true;
                for (int j = 0; j < Buyings.FullSet.GetAllCategories.Count; j++)
                {
                    if (Buyings.FullSet.GetAllCategories[j].Name == c.Name)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    Buyings.FullSet.GetAllCategories.Add(c);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Категория с таким именем уже есть","Ошибка!");
                }
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
                button1_Click(null, null);
            if (e.KeyValue == 27)
                Close();
        }
    }
}
