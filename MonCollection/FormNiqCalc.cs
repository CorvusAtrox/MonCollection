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

        private List<PKM> PkmDB;
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

        public void loadDB(List<PKM> data, int index)
        {
            PkmDB = data;
            ind = index;
            game = getGame(data[index].Identifier);
        }

        public void showValues()
        {
            PKM mon = PkmDB[ind];
            gameDict.TryGetValue(game, out MainForm.SaveInfo si);
            SaveFile sf = SaveUtil.GetBlankSAV(si.version, "blank");
            LegalityAnalysis legal = new LegalityAnalysis(mon, sf.Personal);

            labelName.Text = String.Format("Name: {0}",mon.Nickname);
            labelGame.Text = String.Format("Game: {0}",game);

            comboBoxSpecies.SelectedValue = mon.Species;

            var query1 = PkmDB.Where(pk => pk.Species == mon.Species);
            var query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
            var query3 = query1.Where(pk => getGame(pk.Identifier) == game);

            labelSpVal.Text = String.Format("{0} {1} {2}",query1.Count(), query2.Count(), query3.Count());

            LegalMoveSource.ReloadMoves(legal.AllSuggestedMovesAndRelearn);
            foreach (ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.DataSource, null);
            }

            comboBoxMove1.SelectedValue = mon.Move1;
            comboBoxMove2.SelectedValue = mon.Move2;
            comboBoxMove3.SelectedValue = mon.Move3;
            comboBoxMove4.SelectedValue = mon.Move4;

            List<Label> moveLabels = new List<Label> { labelMove1, labelMove2, labelMove3, labelMove4 };

            IEnumerable<PKM> query1a;
            IEnumerable<PKM> query2a;
            IEnumerable<PKM> query3a;

            if(mon.Move1 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move1 || pk.Move2 == mon.Move1 || pk.Move3 == mon.Move1 || pk.Move4 == mon.Move1);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => getGame(pk.Identifier) == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove1.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move2 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move2 || pk.Move2 == mon.Move2 || pk.Move3 == mon.Move2 || pk.Move4 == mon.Move2);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => getGame(pk.Identifier) == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove2.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move3 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move3 || pk.Move2 == mon.Move3 || pk.Move3 == mon.Move3 || pk.Move4 == mon.Move3);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => getGame(pk.Identifier) == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove3.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move4 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move4 || pk.Move2 == mon.Move4 || pk.Move3 == mon.Move4 || pk.Move4 == mon.Move4);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => getGame(pk.Identifier) == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove4.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }
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
                IEnumerable<PKM> query1;
                IEnumerable<PKM> query2;
                IEnumerable<PKM> query3;
                IEnumerable<PKM> query1a;
                IEnumerable<PKM> query2a;
                IEnumerable<PKM> query3a;

                PKM mon = PkmDB[ind];

                if ((int)comboBoxMoveNew.SelectedValue > 0)
                {
                    query1 = PkmDB.Where(pk => pk.Move1 == (int)comboBoxMoveNew.SelectedValue || pk.Move2 == (int)comboBoxMoveNew.SelectedValue
                                            || pk.Move3 == (int)comboBoxMoveNew.SelectedValue || pk.Move4 == (int)comboBoxMoveNew.SelectedValue);
                    query1a = query1.Where(pk => pk.Species == mon.Species);
                    query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                    query2a = query2.Where(pk => pk.Species == mon.Species);
                    query3 = query1.Where(pk => getGame(pk.Identifier) == game);
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
