using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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
            gameDict.Add("Red [Dustin]", new MainForm.SaveInfo("en", GameVersion.RD, 0));
            gameDict.Add("Red [JOHANN]", new MainForm.SaveInfo("en", GameVersion.RD, 1));
            gameDict.Add("Blue [GARY]", new MainForm.SaveInfo("en", GameVersion.GN, 2));
            gameDict.Add("Blue [Yuuya]", new MainForm.SaveInfo("fr", GameVersion.GN, 3));
            gameDict.Add("Yellow [HERR J]", new MainForm.SaveInfo("en", GameVersion.YW, 4));
            gameDict.Add("Yellow [Juan]", new MainForm.SaveInfo("es", GameVersion.YW, 5));
            gameDict.Add("Gold [Dorothy]", new MainForm.SaveInfo("en", GameVersion.GD, 6));
            gameDict.Add("Silver [Yuna]", new MainForm.SaveInfo("fr", GameVersion.SV, 7));
            gameDict.Add("Crystal [Catria]", new MainForm.SaveInfo("es", GameVersion.C, 8));
            gameDict.Add("Ruby [NICOLE]", new MainForm.SaveInfo("en", GameVersion.R, 9));
            gameDict.Add("Ruby [Phi]", new MainForm.SaveInfo("en", GameVersion.R, 10));
            gameDict.Add("Sapphire [Sigma]", new MainForm.SaveInfo("en", GameVersion.S, 11));
            gameDict.Add("Emerald [GrmCrpr]", new MainForm.SaveInfo("en", GameVersion.E, 12));
            gameDict.Add("FireRed [Martha]", new MainForm.SaveInfo("en", GameVersion.FR, 13));
            gameDict.Add("LeafGreen [MARY]", new MainForm.SaveInfo("en", GameVersion.LG, 14));
            gameDict.Add("LeafGreen [Satoshi]", new MainForm.SaveInfo("en", GameVersion.LG, 15));
            gameDict.Add("Colosseum [HARRY]", new MainForm.SaveInfo("en", GameVersion.CXD, 16));
            gameDict.Add("Colosseum [SNAGEM]", new MainForm.SaveInfo("en", GameVersion.CXD, 17));
            gameDict.Add("XD [DAVID]", new MainForm.SaveInfo("en", GameVersion.CXD, 18));
            gameDict.Add("XD [MirEgal]", new MainForm.SaveInfo("en", GameVersion.CXD, 19));
            gameDict.Add("XD [NASP]", new MainForm.SaveInfo("en", GameVersion.CXD, 20));
            gameDict.Add("XD [SMEDLY]", new MainForm.SaveInfo("en", GameVersion.CXD, 21));
            gameDict.Add("XD [WILLY]", new MainForm.SaveInfo("en", GameVersion.CXD, 22));
            gameDict.Add("Snakewood [Pete]", new MainForm.SaveInfo("en", GameVersion.R, 23));
            gameDict.Add("Grand Day Out [Wanda]", new MainForm.SaveInfo("en", GameVersion.LG, 24));
            gameDict.Add("Diamond [JOHANN]", new MainForm.SaveInfo("en", GameVersion.D, 25));
            gameDict.Add("Pearl [Jake]", new MainForm.SaveInfo("en", GameVersion.P, 26));
            gameDict.Add("Platinum [Guess]", new MainForm.SaveInfo("en", GameVersion.Pt, 27));
            gameDict.Add("HeartGold [LIAKS]", new MainForm.SaveInfo("en", GameVersion.HG, 28));
            gameDict.Add("SoulSilver [WOLFI]", new MainForm.SaveInfo("de", GameVersion.SS, 29));
            gameDict.Add("Black [KONRAD]", new MainForm.SaveInfo("en", GameVersion.B, 30));
            gameDict.Add("White [JnaBrta]", new MainForm.SaveInfo("de", GameVersion.W, 31));
            gameDict.Add("Black 2 [Crow]", new MainForm.SaveInfo("en", GameVersion.B2, 32));
            gameDict.Add("White 2 [Bow]", new MainForm.SaveInfo("en", GameVersion.W2, 33));
            gameDict.Add("X [だいすけ]", new MainForm.SaveInfo("ja", GameVersion.X, 34));
            gameDict.Add("Y [Fukurou]", new MainForm.SaveInfo("de", GameVersion.Y, 35));
            gameDict.Add("Omega Ruby [Akira]", new MainForm.SaveInfo("es", GameVersion.OR, 36));
            gameDict.Add("Alpha Sapphire [Dschohehn]", new MainForm.SaveInfo("de", GameVersion.AS, 37));
            gameDict.Add("Bank [corvusbrachy]", new MainForm.SaveInfo("en", GameVersion.US, 38));
            gameDict.Add("Bank [corvusossi]", new MainForm.SaveInfo("de", GameVersion.UM, 39));
            gameDict.Add("Sun [Ramirez]", new MainForm.SaveInfo("es", GameVersion.SN, 40));
            gameDict.Add("Moon [Fina]", new MainForm.SaveInfo("de", GameVersion.MN, 41));
            gameDict.Add("Ultra Sun [Hibiki]", new MainForm.SaveInfo("de", GameVersion.US, 42));
            gameDict.Add("Ultra Moon [かなで]", new MainForm.SaveInfo("ja", GameVersion.UM, 43));
            gameDict.Add("Let's Go Pikachu [Suzy]", new MainForm.SaveInfo("en", GameVersion.GP, 44));
            gameDict.Add("Let's Go Eevee [Dieter]", new MainForm.SaveInfo("de", GameVersion.GE, 45));
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
            string sub = identifier.Substring(identifier.IndexOf(".p"));
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
