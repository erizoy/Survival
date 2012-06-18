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
    public partial class ViewCategorySelectionForm : Form
    {
        public List<string> names = new List<string>();
        public bool ok = false;

        public ViewCategorySelectionForm()
        {
            InitializeComponent();

        }

        private void ViewCategorySelectionForm_Load(object sender, EventArgs e)
        {
            listView1.Items.Add(Income.Name);
            for (int i = 0; i < Buyings.FullSet.GetAllCategories.Count; i++)
                listView1.Items.Add(Buyings.FullSet.GetAllCategories[i].Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                MessageBox.Show("Выберете одну или более категорий", "Внимание!");
            else
            {
                for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                    names.Add(listView1.SelectedItems[i].Text);
                ok = true;
                Close();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                names.Add(listView1.SelectedItems[0].Text);
                ok = true;
                Close();
            }
        }

        private void ViewCategorySelectionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(null,null);
            if (e.KeyValue == 27)
                Close();
        }
    }
}
