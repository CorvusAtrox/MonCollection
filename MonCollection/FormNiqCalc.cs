using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MonCollection.Properties;
using PKHeX.Core;

namespace MonCollection
{
    public partial class FormNiqCalc : Form
    {

        private List<MonData> PkmDB;
        private int ind = 0;
        private string game;
        private readonly LegalMoveSource LegalMoveSource = new LegalMoveSource();
        private List<ComboBox> moveBoxes;

        public Dictionary<string, MainForm.SaveInfo> gameDict;

        public FormNiqCalc()
        {
            InitializeComponent();
            InitializeBindings();
            InitializeGameDict();
        }

        private void InitializeBindings()
        {
            ComboBox[] cbs =
            {
                comboBoxSpecies
            };

            moveBoxes = new List<ComboBox>{ comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4, comboBoxMoveNew };

            foreach (var cb in cbs.Concat(moveBoxes))
            {
                cb.DisplayMember = nameof(ComboItem.Text);
                cb.ValueMember = nameof(ComboItem.Value);
            }

            var source = GameInfo.FilteredSources;

            comboBoxSpecies.DataSource = new BindingSource(source.Species, null);


            // Set the Move ComboBoxes too..
            LegalMoveSource.ReloadMoves(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);
        }

        private void InitializeGameDict()
        {
            gameDict = new Dictionary<string, MainForm.SaveInfo>();
            StreamReader dict = new StreamReader(Settings.Default.mons + "/mons.ini");
            string l;
            string[] split;
            int ind = 0;

            while (!dict.EndOfStream)
            {
                l = dict.ReadLine();
                split = l.Split(',');
                gameDict.Add(split[0], new MainForm.SaveInfo(split[2], split[1], ind));
                ind++;
            }
        }

        internal void LoadDB(List<MonData> data, int index)
        {
            PkmDB = data;
            ind = index;
            game = data[index].Game;
        }

        public void showValues()
        {
            MonData mon = PkmDB[ind];
            gameDict.TryGetValue(game, out MainForm.SaveInfo si);
            SaveFile sf = SaveUtil.GetBlankSAV(si.version, "blank");
            LegalityAnalysis legal = new LegalityAnalysis(MonDataToPKM(mon), sf.Personal);

            labelName.Text = String.Format("Name: {0}",mon.Nickname);
            labelGame.Text = String.Format("Game: {0}",game);

            comboBoxSpecies.SelectedValue = mon.Species;

            var query1 = PkmDB.Where(pk => pk.Species == mon.Species);
            var query2 = query1.Where(pk => pk.Gen == mon.Gen);
            var query3 = query1.Where(pk => pk.Game == game);

            labelSpVal.Text = String.Format("{0} {1} {2}",query1.Count(), query2.Count(), query3.Count());

            LegalMoveSource.ReloadMoves(legal.AllSuggestedMovesAndRelearn);
            foreach (ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.DataSource, null);
            }

            comboBoxMove1.SelectedValue = mon.Moves[0];
            comboBoxMove2.SelectedValue = mon.Moves[1];
            comboBoxMove3.SelectedValue = mon.Moves[2];
            comboBoxMove4.SelectedValue = mon.Moves[3];

            List<Label> moveLabels = new List<Label> { labelMove1, labelMove2, labelMove3, labelMove4 };

            IEnumerable<MonData> query1a;
            IEnumerable<MonData> query2a;
            IEnumerable<MonData> query3a;

            if(mon.Moves[0] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[0] || pk.Moves[1] == mon.Moves[0] || pk.Moves[2] == mon.Moves[0] || pk.Moves[3] == mon.Moves[0]);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => pk.Gen == pk.Gen);
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Game == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove1.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Moves[1] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[1] || pk.Moves[1] == mon.Moves[1] || pk.Moves[2] == mon.Moves[1] || pk.Moves[3] == mon.Moves[1]);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => pk.Gen == pk.Gen);
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Game == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove2.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Moves[2] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[2] || pk.Moves[1] == mon.Moves[2] || pk.Moves[2] == mon.Moves[2] || pk.Moves[3] == mon.Moves[2]);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => pk.Gen == pk.Gen);
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Game == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove3.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Moves[3] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[3] || pk.Moves[1] == mon.Moves[3] || pk.Moves[2] == mon.Moves[3] || pk.Moves[3] == mon.Moves[3]);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => pk.Gen == pk.Gen);
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Game == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove4.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }
        }

        private PKM MonDataToPKM(MonData data)
        {
            //TODO: Check Implementation
            PKM mon;
            switch (data.Gen)
            {
                case 1:
                    mon = new PK1();
                    break;
                case 2:
                    mon = new PK2();
                    break;
                case 3:
                    mon = new PK3();
                    break;
                case 4:
                    mon = new PK4();
                    break;
                case 5:
                    mon = new PK5();
                    break;
                case 6:
                    mon = new PK6();
                    break;
                case 7:
                    mon = new PK7();
                    break;
                default:
                    mon = new PK7();
                    break;
            }
            mon.Species = data.Species;
            mon.AltForm = data.AltForm;
            mon.CurrentLevel = data.Level;
            if (gameDict.TryGetValue(data.Game, out MainForm.SaveInfo val))
                mon.Version = (int)val.version;
            return mon;
        }

        private int getGen(string identifier)
        {
            string sub = Regex.Match(identifier, @"\.[pcx][kb][0-9]*$").Value;
            return int.Parse(sub.Substring(3));
        }

        private string getGame(string identifier)
        {
            string[] strings = identifier.Split('\\');
            int count = strings.Count();
            return strings[count - 2];
        }

        private void ComboBoxMoveNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PkmDB != null) {
                IEnumerable<MonData> query1;
                IEnumerable<MonData> query2;
                IEnumerable<MonData> query3;
                IEnumerable<MonData> query1a;
                IEnumerable<MonData> query2a;
                IEnumerable<MonData> query3a;

                MonData mon = PkmDB[ind];

                if ((int)comboBoxMoveNew.SelectedValue > 0)
                {
                    query1 = PkmDB.Where(pk => pk.Moves[0] == (int)comboBoxMoveNew.SelectedValue || pk.Moves[1] == (int)comboBoxMoveNew.SelectedValue
                                            || pk.Moves[2] == (int)comboBoxMoveNew.SelectedValue || pk.Moves[3] == (int)comboBoxMoveNew.SelectedValue);
                    query1a = query1.Where(pk => pk.Species == mon.Species);
                    query2 = query1.Where(pk => pk.Gen == mon.Gen);
                    query2a = query2.Where(pk => pk.Species == mon.Species);
                    query3 = query1.Where(pk => pk.Game == game);
                    query3a = query3.Where(pk => pk.Species == mon.Species);

                    labelNewMove.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                    , query1.Count()+1, query1a.Count()+1, query2.Count()+1
                                                    , query2a.Count()+1, query3.Count()+1, query3a.Count()+1);
                }
                else
                {
                    labelNewMove.Text = "0";
                }
            }
        }
    }
}
