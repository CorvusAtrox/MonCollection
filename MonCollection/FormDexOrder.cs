using Microsoft.VisualBasic;
using MonCollection.Properties;
using PKHeX.Core;
using PKHeX.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MonCollection
{
    public partial class FormDexOrder : Form
    {
        private List<MonSortOrder> dexes;
        public int dex;
        public int vis;

        public FormDexOrder(List<MonSortOrder> x, int d, int v)
        {
            InitializeComponent();
            dexes = x;
            foreach(var entry in dexes)
            {
                comboBoxOrder.Items.Add(entry.Name);
            }
            comboBoxOrder.SelectedIndex = (comboBoxOrder.Items.Count > d) ? d : 0;
            comboBoxVisible.SelectedIndex = v;
            LoadDexCheckboxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dex = comboBoxOrder.SelectedIndex;
            vis = comboBoxVisible.SelectedIndex;
            Close();
        }

        private void LoadDexCheckboxes()
        {
            checkedListBoxDexes.Items.Clear();
            foreach (var dex in dexes[comboBoxOrder.SelectedIndex].Dexes)
            {
                checkedListBoxDexes.Items.Add(dex.Key);
            }

            int numDex = checkedListBoxDexes.Items.Count;
            for(int i = 0; i < numDex; i++)
            {
                checkedListBoxDexes.SetItemChecked(i, true);
            }
        }

        public List<bool> getChecks()
        {
            List<bool> checks = new List<bool>();
            for(int i = 0; i < checkedListBoxDexes.Items.Count; i++)
            {
                checks.Add(checkedListBoxDexes.GetItemChecked(i));
            }

            return checks;
        }

        private void comboBoxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDexCheckboxes();
        }
    }
}
