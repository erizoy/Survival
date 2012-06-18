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
    public delegate void CategoryDelegate(string name, string desciption);

    public partial class Categories : Form
    {

        public Categories()
        {
            InitializeComponent();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new CategoryForm()).ShowDialog(); 
             RefreshList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберете строку или строки, которые хотите удалить.\nДля этого нажмите на ячейку, слева от названия той категории, которую хотите удалить.\nДля множественного выбора пользуйтесь клавишей ctrl.","Подсказка!");
            }
            else
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    if (referenced(dataGridView1.SelectedRows[0].Index))
                    {
                        if (MessageBox.Show("В категории " + Buyings.FullSet.GetAllCategories[dataGridView1.SelectedRows[0].Index].Name + " есть покупки.\nВы уверены, что хотите удалить ее? Категория этих покупок будет не определена.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (Item itm in Buyings.FullSet.GetAllItems)
                                if (itm.CategoryIndex == dataGridView1.SelectedRows[0].Index) itm.CategoryIndex = -1;
                            Buyings.FullSet.GetAllCategories.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        }
                        else
                            return;
                    }
                    else
                        Buyings.FullSet.GetAllCategories.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                else 
                {
                    bool rr=false;
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                        if (referenced(r.Index)) 
                            rr = true;
                    if (rr)
                    {
                        if (MessageBox.Show("В этих категориях есть покупки.\nВы уверены, что хотите удалить их все?\nКатегория этих покупок будет не определена.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                            {
                                Buyings.FullSet.GetAllCategories.RemoveAt(r.Index);
                                foreach (Item itm in Buyings.FullSet.GetAllItems)
                                    if (itm.CategoryIndex == r.Index)
                                        itm.CategoryIndex = -1;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                            {
                                Buyings.FullSet.GetAllCategories.RemoveAt(r.Index);
                                foreach (Item itm in Buyings.FullSet.GetAllItems)
                                    if (itm.CategoryIndex == r.Index) 
                                        itm.CategoryIndex = -1;
                            }
                    }
                           
                }
               RefreshList();
            }

        }
       
        private void RefreshList()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < Buyings.FullSet.GetAllCategories.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = new Bitmap(40, 21);
                Graphics.FromImage((Image)dataGridView1[0, i].Value).Clear(Buyings.FullSet.GetAllCategories[i].getColor());
                dataGridView1[1, i].Value = Buyings.FullSet.GetAllCategories[i].Name;
                dataGridView1[2, i].Value = Buyings.FullSet.GetAllCategories[i].Description;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((new CategoryForm(e.RowIndex)).ShowDialog() == DialogResult.OK)
                RefreshList();
        }

        private void Categories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                Close();
            if (e.KeyData == Keys.Delete)
                button2_Click(sender, e);
        }
        
        private bool referenced(int index)
        {
            foreach (Item itm in Buyings.FullSet.GetAllItems)
                if (itm.CategoryIndex == index)
                    return true;
            return false;
        }
    }
}
