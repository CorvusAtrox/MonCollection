using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MonCollection
{
    public partial class FormFilters : Form
    {
        public List<string> filters;

        public FormFilters()
        {
            InitializeComponent();
        }

        public void loadFilterList(List<string> vs)
        {
            foreach(string v in vs)
            {
                ListViewItem item = new ListViewItem(v);
                if (filters.Contains(v))
                    item.SubItems.Add("true");
                else
                    item.SubItems.Add("false");
                listViewFilters.Items.Add(item);
            }
        }

        public void updateFilters()
        {
            filters = new List<string>();
            foreach (ListViewItem item in listViewFilters.Items)
            {
                bool sh = bool.Parse(item.SubItems[1].Text);
                if (sh)
                    filters.Add(item.Text);
            }
        }

        private void ListViewFilters_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewFilters.SelectedItems)
            {
                bool sh = bool.Parse(item.SubItems[1].Text);
                if (sh)
                    item.SubItems[1].Text = "false";
                else
                    item.SubItems[1].Text = "true";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonSelectAll_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewFilters.Items)
                item.SubItems[1].Text = "true";
        }

        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewFilters.Items)
                item.SubItems[1].Text = "false";
        }
    }
}
