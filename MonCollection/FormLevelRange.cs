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
    public partial class FormLevelRange : Form
    {

        public bool set = false;
        public (int,int) setLevels;

        public FormLevelRange()
        {
            InitializeComponent();
        }

        public void loadLevels()
        {
            textBoxMin.Text = setLevels.Item1.ToString();
            textBoxMax.Text = setLevels.Item2.ToString();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            set = true;

            int val1 = int.Parse(textBoxMin.Text);
            int val2 = int.Parse(textBoxMax.Text);

            if (val1 > val2)
            {
                int temp = val1;
                val1 = val2;
                val2 = temp;
            }

            if(val1 < 0)
            {
                val1 = 0;
            }

            if(val2 > 100)
            {
                val2 = 100;
            }

            setLevels.Item1 = val1;
            setLevels.Item2 = val2;
            Close();
        }
    }
}
