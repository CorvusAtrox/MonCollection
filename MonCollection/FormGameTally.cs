using PKHeX.Core;
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
    public partial class FormGameTally : Form
    {
        public FormGameTally()
        {
            InitializeComponent();
        }

        public void addEntry(string data)
        {
            listGames.Items.Add(data);
        }

        private void ListGames_DoubleClick(object sender, EventArgs e)
        {
            string result = "";
            foreach (string item in listGames.Items)
                result += item + "\n";
            Clipboard.SetText(result);
            MessageBox.Show("Data copied to clipboard");
        }
    }
}
