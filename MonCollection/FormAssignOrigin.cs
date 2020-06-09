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
    public partial class FormAssignOrigin : Form
    {

        public bool set = false;
        public string setOT;
        public int setID;
        public string setOrigin;
        public Dictionary<string, int> regionVals;

        public FormAssignOrigin()
        {
            InitializeComponent();
        }

        public void LoadOrigins()
        {
            comboBoxOrigin.Items.Clear();
            foreach (var entry in regionVals)
                comboBoxOrigin.Items.Add(entry.Key.ToString());
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            set = true;
            setOT = textBoxOT.Text;
            setID = int.Parse(textBoxID.Text);
            setOrigin = comboBoxOrigin.Text;
            Close();
        }
    }
}
