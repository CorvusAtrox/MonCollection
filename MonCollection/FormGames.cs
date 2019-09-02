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
    public partial class FormGames : Form
    {

        public Dictionary<string, SaveInfo> gameDict;
        private string[] languages = { "", "jp", "en", "fr", "it", "de", "", "es", "ko", "zh", "zh2"};

        public FormGames()
        {
            InitializeComponent();
            InitializeGameDict();
            InitializeBindings();
            SetGameOptions();
        }

        private void InitializeGameDict()
        {
            gameDict = new Dictionary<string, SaveInfo>();

            StreamReader dict = new StreamReader(Settings.Default.mons + "/mons.ini");
            string l;
            string[] split;
            int ind = 0;

            while (!dict.EndOfStream)
            {
                l = dict.ReadLine();
                split = l.Split(',');
                switch (split.Count())
                {
                    case 5:
                        gameDict.Add(split[0], new SaveInfo(split[4], split[1], split[2], split[3], ind));
                        break;
                    case 4:
                        gameDict.Add(split[0], new SaveInfo("en", split[1], split[2], split[3], ind));
                        break;
                    case 3:
                        gameDict.Add(split[0], new SaveInfo(split[2], split[1], "0", null, ind));
                        break;
                    case 2:
                        gameDict.Add(split[0], new SaveInfo("en", split[1], "0", null, ind));
                        break;
                    default:
                        gameDict.Add(split[0], new SaveInfo("en", "US", "0", null, ind));
                        break;
                }
                ind++;
            }
            dict.Close();
        }

        private void InitializeBindings()
        {
            comboBoxVersion.InitializeBinding();
            comboBoxVersion.DataSource = GameInfo.VersionDataSource;
            comboBoxLanguage.InitializeBinding();
            comboBoxLanguage.DataSource = GameInfo.LanguageDataSource(7);
        } 

        public void updateGameIni()
        {
            using (StreamWriter file = new StreamWriter(Settings.Default.mons + "/mons.ini", false))
            {
                string output = "";
                foreach (var entry in gameDict)
                {
                    output = entry.Key + "," + entry.Value.version.ToString();
                    output += "," + entry.Value.id.ToString() + "," + entry.Value.ot;
                    output += "," + entry.Value.language;
                    file.WriteLine(output);
                }
            }
        }

        private void SetGameOptions()
        {
            int temp = comboBoxGame.SelectedIndex;
            comboBoxGame.Items.Clear();
            foreach (var entry in gameDict)
            {
                comboBoxGame.Items.Add(entry.Key);
            }
            if (temp < comboBoxGame.Items.Count)
                comboBoxGame.SelectedIndex = temp;
        }

        private void ComboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            string g = comboBoxGame.Items[comboBoxGame.SelectedIndex].ToString();
            SaveInfo si = gameDict[g];
            int val = Array.IndexOf(languages,si.language);
            comboBoxLanguage.SelectedValue = val;
            textBoxID.Text = si.id.ToString();
            textBoxOT.Text = si.ot;
            comboBoxVersion.SelectedValue = (int)si.version;
        }

        private void ButtonAddGame_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter New Game's Title");
            gameDict.Add(input, new SaveInfo("en", "US", "0", null, gameDict.Count()));
            SetGameOptions();
        }

        private void ButtonChangeTitle_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Game's New Title");
            gameDict[input] = gameDict[comboBoxGame.Text];
            gameDict.Remove(comboBoxGame.Text);
            SetGameOptions();
        }

        private void ButtonRemoveGame_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure You Want To Remove This Game?", "Remove Game", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                gameDict.Remove(comboBoxGame.Text);
                SetGameOptions();
            }
        }

        private void ButtonSaveGame_Click(object sender, EventArgs e)
        {
            gameDict[comboBoxGame.Text] = new SaveInfo(languages[(int)comboBoxLanguage.SelectedValue],
                                                       ((GameVersion)comboBoxVersion.SelectedValue).ToString(),
                                                       textBoxID.Text, textBoxOT.Text,comboBoxGame.SelectedIndex);
        }
    }
}
