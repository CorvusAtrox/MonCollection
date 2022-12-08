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
    public partial class FormEditDexes : Form
    {

        private List<MonSortOrder> dexes;

        public FormEditDexes(List<MonSortOrder> d)
        {
            InitializeComponent();
            dexes = d;
            SetGameOptions();
        }

        public List<MonSortOrder> UpdateDexes()
        {
            return dexes;
        }

        private void SetGameOptions()
        {
            int temp = comboBoxOrder.SelectedIndex;
            comboBoxOrder.Items.Clear();
            foreach (var entry in dexes)
            {
                comboBoxOrder.Items.Add(entry.Name);
            }
            if (temp < comboBoxOrder.Items.Count)
                comboBoxOrder.SelectedIndex = temp;
        }

        private void ShowDexOptions()
        {
            int temp = comboBoxDexes.SelectedIndex;
            comboBoxDexes.Items.Clear();
            foreach (var entry in dexes[comboBoxOrder.SelectedIndex].Dexes)
            {
                comboBoxDexes.Items.Add(entry.Key);
            }
            comboBoxDexes.Items.Add("Foreign");
            if (temp < comboBoxDexes.Items.Count)
                comboBoxDexes.SelectedIndex = temp;
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter New Order's Name");
            MonSortOrder newOrder = new MonSortOrder();
            newOrder.Name = input;
            newOrder.Dexes = new Dictionary<string, List<int>>();
            newOrder.Foreign = new List<int>();
            string input2 = Interaction.InputBox("Enter Main Dex Name");
            newOrder.Dexes.Add(input2, new List<int>());
            dexes.Add(newOrder);
            SetGameOptions();
        }

        private void buttonAddDex_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter New Dex's Name");
            dexes[comboBoxOrder.SelectedIndex].Dexes.Add(input, new List<int>());
            ShowDexOptions();
        }

        private void comboBoxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDexOptions();
        }

        private void comboBoxDexes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDexOrder();
        }

        private void ShowDexOrder()
        {
            List<int> species;

            if (comboBoxDexes.Text.Equals("Foreign"))
            {
                species = dexes[comboBoxOrder.SelectedIndex].Foreign;
            } 
            else
            {
                species = dexes[comboBoxOrder.SelectedIndex].Dexes[comboBoxDexes.Text];
            }

            textBoxSpecies.Text = string.Join(",", species);
            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();
            listSpecies.Items.Clear();
            int num = 1;
            foreach (int s in species)
            {
                if (s < PkmListSorted.Count)
                {
                    listSpecies.Items.Add(num.ToString() + ": " + PkmListSorted[nationalToSv(s)].Text);
                }
                else
                {
                    listSpecies.Items.Add(num.ToString() + ": " + s.ToString());
                }
                num++;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string[] spec = textBoxSpecies.Text.Split(',');
            List<int> species = new List<int>();
            foreach(string s in spec)
            {
                if(int.TryParse(s,out int item))
                {
                    species.Add(item);
                }
            }

            if (comboBoxDexes.Text.Equals("Foreign"))
            {
                List<int> inDex = new List<int>();
                foreach(var dex in dexes[comboBoxOrder.SelectedIndex].Dexes)
                {
                    foreach(int num in species)
                    {
                        if (dex.Value.Contains(num))
                        {
                            inDex.Add(num);
                        }
                    }
                }
                foreach(int num in inDex)
                {
                    species.Remove(num);
                }
                species.Sort();
                dexes[comboBoxOrder.SelectedIndex].Foreign = species;
            }
            else
            {
                dexes[comboBoxOrder.SelectedIndex].Dexes[comboBoxDexes.Text] = species;
            }

            ShowDexOrder();
        }

        private void buttonAutoFill_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Input desired Dex Range in the following format: min,max");
            string[] vals = input.Split(',');
            if(vals.Length > 1)
            {
                int min = 1;
                int max = 1;

                if(int.TryParse(vals[0],out int res))
                {
                    min = res;
                }

                if (int.TryParse(vals[1], out int ult))
                {
                    max = ult;
                }

                List<int> species = new List<int>();

                while(min <= max)
                {
                    species.Add(min);
                    min++;
                }

                if(species.Count > 0)
                {
                    if (comboBoxDexes.Text.Equals("Foreign"))
                    {
                        List<int> inDex = new List<int>();
                        foreach (var dex in dexes[comboBoxOrder.SelectedIndex].Dexes)
                        {
                            foreach (int num in species)
                            {
                                if (dex.Value.Contains(num))
                                {
                                    inDex.Add(num);
                                }
                            }
                        }
                        foreach (int num in inDex)
                        {
                            species.Remove(num);
                        }
                        dexes[comboBoxOrder.SelectedIndex].Foreign = species;
                    }
                    else
                    {
                        dexes[comboBoxOrder.SelectedIndex].Dexes[comboBoxDexes.Text] = species;
                    }
                }
            }
            ShowDexOrder();
        }

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("List Pokemon to add, comma separated");

            string[] spec = input.Split(',');
            List<int> species = new List<int>();
            foreach (string s in spec)
            {
                if (int.TryParse(s, out int item))
                {
                    species.Add(item);
                }
            }

            string indput = Interaction.InputBox("What is the Dex number of the first Pokemon?");

            if (int.TryParse(indput, out int index))
            {
                dexes[comboBoxOrder.SelectedIndex].Dexes[comboBoxDexes.Text].InsertRange(index-1, species);
            }
            else
            {
                dexes[comboBoxOrder.SelectedIndex].Dexes[comboBoxDexes.Text].AddRange(species);
            }
            ShowDexOrder();
        }

        public int nationalToSv(int index)
        {
            int[] dexNums =
            {
                918, 919, 920, 921, 954, 955, 956, 945, 946, 970, 971, 935, 936, 937, 960,
                963, 964, 965, 1003, 1004, 1005, 940, 941, 957, 958, 972, 973, 968, 969,
                974, 975, 1006, 1007, 962, 938, 939, 922, 923, 926, 927, 1000, 1001, 1002,
                929, 930, 959, 933, 934, 942, 943, 953, 944, 966, 967, 924, 925, 961, 947,
                948, 932, 931, 952, 1010, 1009, 928, 917, 1008, 978, 982, 979, 983, 984,
                981, 986, 992, 989, 990, 988, 991, 949, 950, 951, 976, 977, 996, 995, 994,
                997, 985, 993, 998, 999
            };

            if (index >= 917 && index < 917 + dexNums.Length)
            {
                index -= 917;
                index = dexNums[index];
            }

            return index;
        }
    }
}
