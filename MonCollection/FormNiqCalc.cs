using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
        private string origin;
        private readonly LegalMoveSource<ComboItem> LegalMoveSource = new(new LegalMoveComboSource());
        private List<ComboBox> moveBoxes;
        private int[] majorGenderDiff;
        private int[] noDiff;

        public Dictionary<string, SaveInfo> gameDict;

        private MonFamily monFamily;

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
                comboBoxBalls, comboBoxSpecies, comboBoxLang, comboBoxAbility, comboBoxNature
            };

            moveBoxes = new List<ComboBox>{ comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4, comboBoxMoveNew };

            foreach (var cb in cbs.Concat(moveBoxes))
            {
                cb.DisplayMember = nameof(ComboItem.Text);
                cb.ValueMember = nameof(ComboItem.Value);
            }

            var source = GameInfo.FilteredSources;

            comboBoxLang.DataSource = source.Languages;

            comboBoxBalls.DataSource = new BindingSource(source.Balls, null);
            comboBoxSpecies.DataSource = new BindingSource(source.Species, null);
            comboBoxAbility.DataSource = new BindingSource(source.Abilities, null);
            comboBoxNature.DataSource = new BindingSource(source.Natures, null);

            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);
        }

        private void InitializeGameDict()
        {
            majorGenderDiff = new int[] { 521, 592, 593, 668 };
            noDiff = new int[] { 414, 664, 665 };

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
                    case 6:
                        gameDict.Add(split[0], new SaveInfo(split[4], split[1], split[5], split[2], split[3], ind));
                        break;
                    case 5:
                        gameDict.Add(split[0], new SaveInfo(split[4], split[1], "", split[2], split[3], ind));
                        break;
                    case 4:
                        gameDict.Add(split[0], new SaveInfo("en", split[1], "", split[2], split[3], ind));
                        break;
                    case 3:
                        gameDict.Add(split[0], new SaveInfo(split[2], split[1], "", "0", null, ind));
                        break;
                    case 2:
                        gameDict.Add(split[0], new SaveInfo("en", split[1], "", "0", null, ind));
                        break;
                    default:
                        gameDict.Add(split[0], new SaveInfo("en", "SH", "", "0", null, ind));
                        break;
                }
                ind++;
            }
            dict.Dispose();
        }

        internal void LoadDB(List<MonData> data, int index)
        {
            PkmDB = data;
            ind = index;
            game = data[index].Game;
            origin = data[index].Origin;
        }

        public void ShowValues()
        {
            MonData mon = PkmDB[ind];
            gameDict.TryGetValue(game, out SaveInfo si);
            if (si.version == GameVersion.HOME)
                si.version = GameVersion.SH;
            SaveFile sf = SaveUtil.GetBlankSAV(si.version, "blank");
            LegalityAnalysis legal = new LegalityAnalysis(MonDataToPKM(mon), sf.Personal);

            //LegalMoveSource.ReloadMoves(legal);

            monFamily = new MonFamily();
            ushort[] family = monFamily.GetFamily(mon.Species);

            labelName.Text = String.Format("Name: {0}",mon.Nickname);
            labelGame.Text = String.Format("Game: {0}",game);

            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();

            comboBoxSpecies.Text = PkmListSorted[nationalToSv(mon.Species)].Text;

            bool gd = true;
            bool nd = false;
            if (majorGenderDiff.Contains(mon.Species))
                gd = false;
            if (noDiff.Contains(mon.Species))
                nd = true;

            var query1 = PkmDB.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            var query1f = PkmDB.Where(pk => family.Contains(pk.Species));
            var query2 = query1.Where(pk => pk.Game == game);
            var query2f = query1f.Where(pk => pk.Game == game);
            var query0 = query1.Where(pk => pk.Origin == origin);
            var query0f = query1f.Where(pk => pk.Origin == origin);

            labelSpVal.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})", query1f.Count(), query1.Count(), query0f.Count(), query0.Count(), query2f.Count(), query2.Count());

            /*LegalMoveSource.ReloadMoves(legal);
            foreach (ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.Display.DataSource, null);
            }*/

            comboBoxMove1.SelectedValue = mon.Moves[0];
            comboBoxMove2.SelectedValue = mon.Moves[1];
            comboBoxMove3.SelectedValue = mon.Moves[2];
            comboBoxMove4.SelectedValue = mon.Moves[3];

            List<Label> moveLabels = new List<Label> { labelMove1, labelMove2, labelMove3, labelMove4 };

            IEnumerable<MonData> query1a;
            IEnumerable<MonData> query2a;
            IEnumerable<MonData> query0a;

            if (mon.Moves[0] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[0] || pk.Moves[1] == mon.Moves[0] || pk.Moves[2] == mon.Moves[0] || pk.Moves[3] == mon.Moves[0]);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelMove1.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
            }

            if (mon.Moves[1] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[1] || pk.Moves[1] == mon.Moves[1] || pk.Moves[2] == mon.Moves[1] || pk.Moves[3] == mon.Moves[1]);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelMove2.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
            }

            if (mon.Moves[2] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[2] || pk.Moves[1] == mon.Moves[2] || pk.Moves[2] == mon.Moves[2] || pk.Moves[3] == mon.Moves[2]);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelMove3.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
            }

            if (mon.Moves[3] > 0)
            {
                query1 = PkmDB.Where(pk => pk.Moves[0] == mon.Moves[3] || pk.Moves[1] == mon.Moves[3] || pk.Moves[2] == mon.Moves[3] || pk.Moves[3] == mon.Moves[3]);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelMove4.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
            }

            comboBoxLang.SelectedValue = mon.Language;

            query1 = PkmDB.Where(pk => pk.Language == mon.Language);
            query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query1f = query1.Where(pk => family.Contains(pk.Species));
            query2 = query1.Where(pk => pk.Game == game);
            query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query2f = query2.Where(pk => family.Contains(pk.Species));
            query0 = query1.Where(pk => pk.Origin == origin);
            query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query0f = query0.Where(pk => family.Contains(pk.Species));

            labelLangVal.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                            , query1.Count(), query1f.Count(), query1a.Count(),
                                              query0.Count(), query0f.Count(), query0a.Count(),
                                              query2.Count(), query2f.Count(), query2a.Count());

            comboBoxBalls.SelectedValue = mon.Ball;

            query1 = PkmDB.Where(pk => pk.Ball == mon.Ball || pk.Ball == 0 && mon.Ball == 4);
            query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query1f = query1.Where(pk => family.Contains(pk.Species));
            query2 = query1.Where(pk => pk.Game == game);
            query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query2f = query2.Where(pk => family.Contains(pk.Species));
            query0 = query1.Where(pk => pk.Origin == origin);
            query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
            query0f = query0.Where(pk => family.Contains(pk.Species));

            labelBallVal.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                            , query1.Count(), query1f.Count(), query1a.Count(),
                                              query0.Count(), query0f.Count(), query0a.Count(),
                                              query2.Count(), query2f.Count(), query2a.Count());

            comboBoxAbility.SelectedValue = mon.Ability;

            if (mon.Ability > 0)
            {
                query1 = PkmDB.Where(pk => pk.Ability == mon.Ability);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelAbilityVal.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
            }

            comboBoxNature.SelectedValue = mon.Nature;

            if (mon.Nature > 0)
            {
                query1 = PkmDB.Where(pk => pk.Nature == mon.Nature);
                query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query1f = query1.Where(pk => family.Contains(pk.Species));
                query2 = query1.Where(pk => pk.Game == game);
                query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query2f = query2.Where(pk => family.Contains(pk.Species));
                query0 = query1.Where(pk => pk.Origin == origin);
                query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                query0f = query0.Where(pk => family.Contains(pk.Species));

                labelNatureVal.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                , query1.Count(), query1f.Count(), query1a.Count(),
                                                  query0.Count(), query0f.Count(), query0a.Count(),
                                                  query2.Count(), query2f.Count(), query2a.Count());
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
                case 8:
                    mon = new PK8();
                    break;
                case 9:
                    mon = new PK9();
                    break;
                default:
                    mon = new PK7();
                    break;
            }
            mon.Species = data.Species;
            mon.Form = data.AltForm;
            if (mon.Species == 869) //Alcremie
                mon.Form = (byte)(mon.Form / 7);
            mon.CurrentLevel = (byte)data.Level;
            if (gameDict.TryGetValue(data.Game, out SaveInfo val))
                mon.Version = val.version;
            return mon;
        }

        private void ComboBoxMoveNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PkmDB != null) {
                IEnumerable<MonData> query1;
                IEnumerable<MonData> query2;
                IEnumerable<MonData> query0;
                IEnumerable<MonData> query1f;
                IEnumerable<MonData> query2f;
                IEnumerable<MonData> query0f;
                IEnumerable<MonData> query1a;
                IEnumerable<MonData> query2a;
                IEnumerable<MonData> query0a;

                MonData mon = PkmDB[ind];

                bool gd = true;
                bool nd = false;
                if (majorGenderDiff.Contains(mon.Species))
                    gd = false;
                if (noDiff.Contains(mon.Species))
                    nd = true;

                ushort[] family = monFamily.GetFamily(mon.Species);

                if ((int)comboBoxMoveNew.SelectedValue > 0)
                {
                    query1 = PkmDB.Where(pk => pk.Moves[0] == (int)comboBoxMoveNew.SelectedValue || pk.Moves[1] == (int)comboBoxMoveNew.SelectedValue
                                            || pk.Moves[2] == (int)comboBoxMoveNew.SelectedValue || pk.Moves[3] == (int)comboBoxMoveNew.SelectedValue);
                    query1a = query1.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                    query1f = query1.Where(pk => family.Contains(pk.Species));
                    query2 = query1.Where(pk => pk.Game == game);
                    query2a = query2.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                    query2f = query2.Where(pk => family.Contains(pk.Species));
                    query0 = query1.Where(pk => pk.Origin == origin);
                    query0a = query0.Where(pk => pk.Species == mon.Species && (pk.AltForm == mon.AltForm || nd) && (pk.Gender == mon.Gender || gd));
                    query0f = query0.Where(pk => family.Contains(pk.Species));

                    labelNewMove.Text = String.Format("({0} {1} {2}) ({3} {4} {5}) ({6} {7} {8})"
                                                    , query1.Count()+1, query1f.Count()+1, query1a.Count()+1,
                                                      query0.Count()+1, query0f.Count()+1, query0a.Count()+1,
                                                      query2.Count()+1, query2f.Count()+1, query2a.Count()+1);
                }
                else
                {
                    labelNewMove.Text = "0";
                }
            }
        }

        private int nationalToSv(int index)
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
