using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonCollection
{
    public partial class FormRibbons : Form
    {

        public List<string> ribbons;

        public FormRibbons()
        {
            InitializeComponent();
        }

        public void LoadRibbons()
        {
            foreach(string r in ribbons)
            {
                listBoxRibbons.Items.Add(r);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ribbons = new List<string>();
            foreach (ListViewItem item in listBoxRibbons.Items)
            {
                ribbons.Add(item.Text);
            }
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBoxRibbons.Items.Add("ribbon");
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listBoxRibbons.SelectedItems)
            {
                listBoxRibbons.Items.Remove(eachItem);
            }
        }
    }
}
