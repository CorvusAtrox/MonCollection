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
            listGames.Text += data + "\n";
        }

        private void ListGames_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(listGames.Text);
            MessageBox.Show("Mon Collection","Data copied to clipboard");
        }
    }
}
