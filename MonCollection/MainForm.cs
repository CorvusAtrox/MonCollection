using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonCollection.Properties;
using PKHeX.Core;
using PKHeX.WinForms;

namespace MonCollection
{
    public partial class MainForm : Form
    {
        private SaveFile ver;
        private string[] genders = { "♂", "♀️", "-" };
        private readonly LegalMoveSource LegalMoveSource = new LegalMoveSource();
        private ComboBox[] moveBoxes;
        private PictureBox[] PKXBOXES;
        private int slotSelected = -1; // = null;
        private LegalityAnalysis legal;
        private List<PKM> PkmData;

        private List<ComboItem> PkmListAny;
        private Dictionary<Tuple<GameVersion, int>, bool> monInGame; 

        private string Counter = "Num Mon: {0}";
        private string HP = "HP: {0}";
        private string Attack = "Attack: {0}";
        private string Defense = "Defense: {0}";
        private string SpAtk = "Sp. Atk: {0}";
        private string SpDef = "Sp. Def: {0}";
        private string Speed = "Speed: {0}";

        private string OT = "OT: {0} ({1})";
        private string Game = "Game: {0}";
        private string Nickname = "Name: {0}";

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;

        private int[] majorGenderDiff;
        private int[] minorGenderDiff;
        private int[] noDiff;

        public static DrawConfig Draw = new DrawConfig();

        public Dictionary<string,SaveInfo> gameDict;

        public class SaveInfo
        {
            public string language;
            public GameVersion version;
            public int index;

            public SaveInfo(string l, string v, int i)
            {
                language = l;
                Enum.TryParse(v, out GameVersion ver);
                version = ver;
                index = i;
            }

        }


        public MainForm()
        {
            InitializeComponent();
            InitializeGameDict();
            InitializeMonLists();
            InitializeStrings("en",GameVersion.US,"blank");
            InitializeBinding();
            InitializePkxBoxes();
            PopulateFilteredDataSources(ver);
            L_Count.Text = "Loading...";
            new Task(LoadDatabase).Start();
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
                if(split.Count() == 3)
                    gameDict.Add(split[0], new SaveInfo(split[2],split[1],ind));
                ind++;
            }
        }

        private void InitializeMonLists()
        {
            GameInfo.Strings = GameInfo.GetStrings("en");
            PkmListAny = new List<ComboItem>(GameInfo.SpeciesDataSource);

            monInGame = new Dictionary<Tuple<GameVersion, int>, bool>();
            GameVersion[] versions = {GameVersion.RD,GameVersion.GN, GameVersion.YW,
                                      GameVersion.GD, GameVersion.SV, GameVersion.C,
                                      GameVersion.R, GameVersion.S, GameVersion.FR,  GameVersion.LG, GameVersion.E,
                                      GameVersion.CXD,
                                      GameVersion.D, GameVersion.P, GameVersion.Pt, GameVersion.HG, GameVersion.SS,
                                      GameVersion.B, GameVersion.W, GameVersion.B2, GameVersion.W2,
                                      GameVersion.X, GameVersion.Y, GameVersion.OR, GameVersion.AS,
                                      GameVersion.SN, GameVersion.MN, GameVersion.US, GameVersion.UM,
                                      GameVersion.GP, GameVersion.GE};
            foreach (GameVersion v in versions)
            {
                SaveFile sf = SaveUtil.GetBlankSAV(v, "blank");
                var f = new FilteredGameDataSource(sf, GameInfo.Sources).Species;
                List<ComboItem> sp = new List<ComboItem>(f);

                foreach (ComboItem ci in PkmListAny)
                {
                    if (sp.Contains(ci))
                        monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value),true);
                    else
                        monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value), false);
                }
            }
            majorGenderDiff = new int[]{ 521, 592, 593, 668, 678 };
            minorGenderDiff = new int[] { 3, 12, 19, 20, 25, 26, 41, 42, 44, 45, 64, 65, 84, 85, 97,
                                         111, 112, 118, 119, 123, 129, 130, 154, 165, 166, 178, 185,
                                         186, 190, 194, 195, 198, 202, 203, 207, 208, 212, 214, 215,
                                         217, 221, 224, 229, 232, 255, 256, 257, 267, 269, 272, 274,
                                         275, 307, 308, 315, 316, 317, 322, 323, 332, 350, 369, 396,
                                         397, 398, 399, 400, 401, 402, 403, 404, 405, 407, 415, 417,
                                         417, 418, 419, 424, 443, 444, 445, 449, 450, 453, 454, 456,
                                         457, 459, 460, 461, 464, 465, 473};
            noDiff = new int[] { 414, 493, 664, 665, 744, 773 };
        }

        private void InitializeStrings(string spr, GameVersion gv, string trainer)
        {
            GameInfo.Strings = GameInfo.GetStrings(spr);
            ver = SaveUtil.GetBlankSAV(gv, trainer);
            PKMConverter.Trainer = ver;
            GameInfo.FilteredSources = new FilteredGameDataSource(ver, GameInfo.Sources);

            // Update Legality Strings
            Task.Run(() =>
            {
                var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Substring(0, 2);
                Util.SetLocalization(typeof(LegalityCheckStrings), lang);
                Util.SetLocalization(typeof(MessageStrings), lang);
                RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);
            });

            // Update Legality Analysis strings
            LegalityAnalysis.MoveStrings = GameInfo.Strings.movelist;
            LegalityAnalysis.SpeciesStrings = GameInfo.Strings.specieslist;
        }

        private void InitializeBinding()
        {
            ComboBox[] cbs =
            {
                comboBoxBalls, comboBoxSpecies, comboBoxLanguage, comboBoxAbility, comboBoxNature,
                comboBoxOrigin, comboBoxMet
            };

            moveBoxes = new []{comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4};

            foreach (var cb in cbs.Concat(moveBoxes))
            {
                cb.DisplayMember = nameof(ComboItem.Text);
                cb.ValueMember = nameof(ComboItem.Value);
            }
        }

        private void InitializePkxBoxes()
        {
            PKXBOXES = new[]
            {
                bpkx1, bpkx2, bpkx3, bpkx4, bpkx5, bpkx6,
                bpkx7, bpkx8, bpkx9, bpkx10,bpkx11,bpkx12,
                bpkx13,bpkx14,bpkx15,bpkx16,bpkx17,bpkx18,
                bpkx19,bpkx20,bpkx21,bpkx22,bpkx23,bpkx24,
                bpkx25,bpkx26,bpkx27,bpkx28,bpkx29,bpkx30
            };

            foreach (var slot in PKXBOXES)
            {
                slot.DoubleClick += (sender, e) =>
                {

                    if((int)slot.Tag != -1)
                    {
                        slotSelected = (int)slot.Tag;
                        OpenPKM(PkmData[(int)slot.Tag]);
                        FillPKXBoxes((int)(bpkx1.Tag) / RES_MIN);
                    }
                    
                };
            }
        }

        private void PopulateFilteredDataSources(ITrainerInfo sav)
        {
            var source = GameInfo.FilteredSources;

            //if (ver.Generation > 1)
            //    CB_HeldItem.DataSource = new BindingSource(source.Items, null);

            comboBoxLanguage.DataSource = source.Languages;

            comboBoxBalls.DataSource = new BindingSource(source.Balls, null);
            comboBoxSpecies.DataSource = new BindingSource(source.Species, null);
            comboBoxAbility.DataSource = new BindingSource(source.Abilities, null);
            comboBoxNature.DataSource = new BindingSource(source.Natures, null);
            comboBoxOrigin.DataSource = new BindingSource(source.Games, null);

            // Set the Move ComboBoxes too..
            LegalMoveSource.ReloadMoves(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);
        }

        private bool OpenPKM(PKM pk)
        {
            if (pk == null)
                return false;
            SetVersion(pk.Identifier);
            PopulateFields(pk);
            return true;
        }

        private void SetVersion(string identifier)
        {
            identifier = getGame(identifier);
            if (!gameDict.TryGetValue(identifier, out SaveInfo info))
                InitializeStrings("en", GameVersion.US, "blank");
            else
                InitializeStrings(info.language, info.version, getTrainer(identifier));

            PopulateFilteredDataSources(ver);

        }

        private int gameIndex(string identifier)
        {
            identifier = getGame(identifier);
            if (!gameDict.TryGetValue(identifier, out SaveInfo info))
                return 0;
            else
                return info.index;
        }

        private void PopulateFields(PKM pk)
        {
            legal = new LegalityAnalysis(pk,ver.Personal);
            LegalMoveSource.ReloadMoves(legal.AllSuggestedMovesAndRelearn);
            foreach(ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.DataSource, null);
            }

            comboBoxMet.DataSource = new BindingSource(GameInfo.GetLocationList(ver.Version,pk.Format), null);

            switch (ver.Generation)
            {
                case 1:
                    labelGender.Visible = false;
                    labelBall.Visible = false;
                    comboBoxBalls.Visible = false;
                    labelAbility.Visible = false;
                    comboBoxAbility.Visible = false;
                    labelNature.Visible = false;
                    comboBoxNature.Visible = false;
                    labelMet.Visible = false;
                    comboBoxMet.Visible = false;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    labelPkrs.Visible = false;
                    buttonEggs.Visible = false;
                    break;
                case 2:
                    labelGender.Visible = true;
                    labelBall.Visible = false;
                    comboBoxBalls.Visible = false;
                    labelAbility.Visible = false;
                    comboBoxAbility.Visible = false;
                    labelMet.Visible = false;
                    comboBoxMet.Visible = false;
                    labelNature.Visible = false;
                    comboBoxNature.Visible = false;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    labelPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    break;
                case 3:
                case 4:
                case 5:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    labelAbility.Visible = true;
                    comboBoxAbility.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    //labelMet.Visible = true;
                    //comboBoxMet.Visible = true;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    labelPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    break;
                case 6:
                case 7:
                case 8:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    labelAbility.Visible = true;
                    comboBoxAbility.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    //labelMet.Visible = true;
                    //comboBoxMet.Visible = true;
                    labelLanguage.Visible = true;
                    comboBoxLanguage.Visible = true;
                    labelPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    break;
            }

            labelNickname.Text = string.Format(Nickname, pk.Nickname);
            comboBoxBalls.SelectedValue = pk.Ball;
            comboBoxSpecies.SelectedValue = pk.Species;
            comboBoxLanguage.SelectedValue = pk.Language;
            comboBoxAbility.SelectedValue = pk.Ability;
            comboBoxNature.SelectedValue = pk.Nature;

            comboBoxOrigin.SelectedValue = pk.Version;
            comboBoxMet.SelectedValue = pk.Met_Location;

            comboBoxMove1.SelectedValue = pk.Move1;
            comboBoxMove2.SelectedValue = pk.Move2;
            comboBoxMove3.SelectedValue = pk.Move3;
            comboBoxMove4.SelectedValue = pk.Move4;

            labelHP.Text = string.Format(HP, pk.Stat_HPMax);
            labelAttack.Text = string.Format(Attack, pk.Stat_ATK);
            labelDefense.Text = string.Format(Defense, pk.Stat_DEF);
            labelSpAtk.Text = string.Format(SpAtk, pk.Stat_SPA);
            labelSpDef.Text = string.Format(SpDef, pk.Stat_SPD);
            labelSpeed.Text = string.Format(Speed, pk.Stat_SPE);
            setStatText(pk.Nature,getGen(pk.Identifier));

            labelOT.Text = string.Format(OT,pk.DisplayTID,pk.OT_Name);
            labelGame.Text = string.Format(Game, getGame(pk.Identifier));

            textBoxLevel.Text = pk.CurrentLevel.ToString();

            pictureBoxBall.Image = retrieveImage("Resources/img/ball/" + pk.Ball + ".png");
            string spForm = pk.Species.ToString();
            if (pk.AltForm > 0 && !noDiff.Contains(pk.Species))
                spForm += "-" + pk.AltForm.ToString();
            else if (majorGenderDiff.Contains(pk.Species))
            {
                if (pk.Gender == 0)
                    spForm += "m";
                else if(pk.Gender == 1)
                    spForm += "f";
            }
            pictureBoxIcon.Image = retrieveImage("Resources/img/icons/" + spForm + ".png");

            if (minorGenderDiff.Contains(pk.Species))
            {
                if(pk.AltForm == 0 && pk.Format >= 4)
                {
                    if (pk.Gender == 0)
                        spForm += "m";
                    else if (pk.Gender == 1)
                        spForm += "f";
                }
            }
            gameDict.TryGetValue(getGame(pk.Identifier), out SaveInfo si);
            pictureBoxGameSprite.Image = getSprite(spForm, si.version,pk.IsShiny);
            pictureBoxGameSprite.Refresh();
            labelGender.Text = genders[pk.Gender];
            Label_IsShiny.Visible = pk.IsShiny;
            if (pk.PKRS_Infected)
            {
                pictureBoxPkrs.Visible = true;
                if (pk.PKRS_Cured)
                {
                    labelPkrs.Text = "Cured";
                    pictureBoxPkrs.Image = retrieveImage("Resources/img/pkrsCured.png");
                }
                else
                {
                    labelPkrs.Text = "Infected";
                    pictureBoxPkrs.Image = retrieveImage("Resources/img/pkrsInfected.png");
                }
                labelPkrs.Text += " - " + pk.PKRS_Strain.ToString();
            }
            else
            {
                pictureBoxPkrs.Visible = false;
                labelPkrs.Text = "";
            }

            var ds = PKX.GetFormList(pk.Species, GameInfo.Strings.types, GameInfo.Strings.forms, genders, pk.Format);
            comboBoxForm.DataSource = ds;
            comboBoxForm.SelectedIndex = pk.AltForm;
            if (comboBoxForm.Items.Count != 1)
                comboBoxForm.Visible = true;
            else
                comboBoxForm.Visible = false;

            playCry(pk.Species, pk.AltForm);
        }

        private void playCry(int sp, int form)
        {
            string spCry;
            if (form > 0)
            {
                spCry = "resources/cries/" + sp.ToString() + "-" + form.ToString() + ".wav";
                if(!File.Exists(spCry))
                    spCry = "resources/cries/" + sp.ToString() + ".wav";
            }    
            else
                spCry = "resources/cries/" + sp.ToString() + ".wav";
            if (File.Exists(spCry))
            {
                SoundPlayer sound = new SoundPlayer(spCry);
                sound.Play();
            }
        }

        private Image getSprite(string species, GameVersion version, bool shiny)
        {
            string game = "";
            string ext = "";
            switch (version)
            {
                case GameVersion.RD:
                case GameVersion.GN:
                    game = "rb";
                    ext = ".png";
                    break;
                case GameVersion.YW:
                    game = "yw";
                    ext = ".png";
                    break;
                case GameVersion.GD:
                    game = "gd";
                    ext = ".png";
                    break;
                case GameVersion.SV:
                    game = "sv";
                    ext = ".png";
                    break;
                case GameVersion.C:
                    game = "c";
                    ext = ".gif";
                    break;
                case GameVersion.R:
                case GameVersion.S:
                    game = "rs";
                    ext = ".png";
                    break;
                case GameVersion.FR:
                case GameVersion.LG:
                    game = "frlg";
                    ext = ".png";
                    break;
                case GameVersion.E:
                    game = "e";
                    ext = ".gif";
                    break;
                case GameVersion.CXD:
                    game = "cxd";
                    ext = ".gif";
                    break;
                case GameVersion.D:
                case GameVersion.P:
                    game = "dp";
                    ext = ".png";
                    break;
                case GameVersion.Pt:
                    game = "pt";
                    ext = ".png";
                    break;
                case GameVersion.HG:
                case GameVersion.SS:
                    game = "hgss";
                    ext = ".png";
                    break;
                case GameVersion.B:
                case GameVersion.W:
                    game = "bw";
                    ext = ".gif";
                    break;
                case GameVersion.B2:
                case GameVersion.W2:
                    game = "b2w2";
                    ext = ".gif";
                    break;
                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.OR:
                case GameVersion.AS:
                    game = "xyoras";
                    ext = ".gif";
                    break;
                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.US:
                case GameVersion.UM:
                    game = "smusum";
                    ext = ".gif";
                    break;
                case GameVersion.GP:
                case GameVersion.GE:
                    game = "lgpe";
                    ext = ".png";
                    break;
            }
            if(!shiny)
                return retrieveImage("Resources/img/"+game+"/"+species+ext);
            else
                return retrieveImage("Resources/img/" + game + "/rare/" + species + ext);
        }

        private void ValidateMovePaint(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var item = (ComboItem)((ComboBox)sender).Items[e.Index];
            var valid = LegalMoveSource.CanLearn(item.Value);

            var current = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var brush = Draw.Brushes.GetBackground(valid, current);
            var textColor = Draw.GetText(current);

            DrawMoveRectangle(e, brush, item.Text, textColor);
        }


        private static void DrawMoveRectangle(DrawItemEventArgs e, Brush brush, string text, Color textColor)
        {
            var rec = new Rectangle(e.Bounds.X - 1, e.Bounds.Y, e.Bounds.Width + 1, e.Bounds.Height + 0); // 1px left
            e.Graphics.FillRectangle(brush, rec);

            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.ExpandTabs | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(e.Graphics, text, e.Font, rec, textColor, flags);
        }

        private void LoadDatabase()
        {
            PkmData = LoadPKMSaves(Settings.Default.mons);

            // Load stats for pkm who do not have any
            foreach (var pk in PkmData.Where(z => z.Stat_Level == 0))
            {
                pk.Stat_Level = pk.CurrentLevel;
                pk.SetStats(pk.GetStats(pk.PersonalInfo));
            }

            try
            {
                BeginInvoke(new MethodInvoker(() => SetResults(PkmData)));
            }
            catch { /* Window Closed? */ }
        }

        private static List<PKM> LoadPKMSaves(string pkmdb)
        {
            var dbTemp = new ConcurrentBag<PKM>();
            var files = Directory.EnumerateFiles(pkmdb, "*", SearchOption.AllDirectories);
            var extensions = new HashSet<string>(PKM.Extensions.Select(z => $".{z}"));
            Parallel.ForEach(files, file =>
            {
                var fi = new FileInfo(file);
                if (!extensions.Contains(fi.Extension) || !PKX.IsPKM(fi.Length)) return;
                var data = File.ReadAllBytes(file);
                var pk = PKMConverter.GetPKMfromBytes(data);
                if (!(pk?.Species > 0))
                    return;
                pk.Identifier = file;
                dbTemp.Add(pk);
            });

            var db = dbTemp.Where(pk => pk.Species != 0).OrderBy(pk => pk.Identifier);

            // Prepare Database
            return new List<PKM>(db);
        }

        private Image retrieveImage(string path)
        {
            if (File.Exists(path))
                return Image.FromFile(path);
            else
                return null;
        }

        private void SetResults(List<PKM> res)
        {

            SCR_Box.Maximum = (int)Math.Ceiling((decimal)res.Count / RES_MIN);
            if (SCR_Box.Maximum > 0) SCR_Box.Maximum--;

            slotSelected = 0; // reset the slot last viewed
            SCR_Box.Value = 0;
            gameSpeciesSort(0);

            L_Count.Text = string.Format(Counter, res.Count);
            OpenPKM(PkmData[0]);
        }

        private void FillPKXBoxes(int start)
        {
            if (PkmData == null)
            {
                for (int i = 0; i < RES_MAX; i++)
                    PKXBOXES[i].Image = null;
                return;
            }
            int begin = start * RES_MIN;
            int end = Math.Min(RES_MAX, PkmData.Count - begin);
            for (int i = 0; i < end; i++)
            {
                PKM mon = PkmData[i + begin];
                string spForm = mon.Species.ToString();
                if(mon.AltForm > 0 && !noDiff.Contains(mon.Species))
                    spForm += "-" + mon.AltForm.ToString();
                else if (majorGenderDiff.Contains(mon.Species))
                {
                    if (mon.Gender == 0)
                        spForm += "m";
                    else if (mon.Gender == 1)
                        spForm += "f";
                }
                PKXBOXES[i].Image = retrieveImage("Resources/img/icons/" + spForm + ".png");
                PKXBOXES[i].Tag = i + begin;
            }
            for (int i = end; i < RES_MAX; i++)
            {
                PKXBOXES[i].Image = null;
                PKXBOXES[i].Tag = -1;
            }

            for (int i = 0; i < RES_MAX; i++)
                PKXBOXES[i].BackgroundImage = null;
            if (slotSelected != -1 && slotSelected >= begin && slotSelected < begin + RES_MAX)
                PKXBOXES[slotSelected - begin].BackgroundImage = Image.FromFile("Resources/img/slotView.png");
        }

        private void SCR_Box_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                FillPKXBoxes(e.NewValue);
        }

        public void speciesGameSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => mon.Species)
                             .ThenBy(mon => gameIndex(mon.Identifier))
                             .ThenBy(mon => mon.CurrentLevel)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<PKM>();

            FillPKXBoxes(index);
        }

        public void genSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => getGen(mon.Identifier))
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.CurrentLevel)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<PKM>();

            FillPKXBoxes(index);
        }


        //        //Gen Level



        //        PkmData.OrderBy(mon=>mon.GenNumber).ThenBy(mon=>mon.CurrentLevel).ThenBy(mon=>mon.Species).ThenBy(mon=>mon.Nickname);



        public void gameLevelSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => gameIndex(mon.Identifier))
                             .ThenBy(mon => mon.CurrentLevel)
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<PKM>();

            FillPKXBoxes(index);
        }

        public void gameSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => gameIndex(mon.Identifier))
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.CurrentLevel)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<PKM>();

            FillPKXBoxes(index);
        }

        private void ButtonGameLevelSort_Click(object sender, EventArgs e)
        {
            gameLevelSort((int)bpkx1.Tag/RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGameSpeciesSort_Click(object sender, EventArgs e)
        {
            gameSpeciesSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGenSpeciesSort_Click(object sender, EventArgs e)
        {
            genSpeciesSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonReloadDB_Click(object sender, EventArgs e)
        {
            LoadDatabase();
            ButtonGameLevelSort_Click(sender,e);
        }

        private void ButtonGameTally_Click(object sender, EventArgs e)
        {
            var query = PkmData.GroupBy(
                mon => getGame(mon.Identifier),
                mon => mon.Nickname,
                (game, name) => new
                {
                    Key = game,
                    Count = name.Count()
                });
            var results = new FormGameTally();
            foreach (var q in query)
                results.addEntry(String.Format("{0}: {1}",q.Key,q.Count));
            results.Show();
        }

        private void ButtonSpeciesSort_Click(object sender, EventArgs e)
        {
            speciesGameSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGameMonTally_Click(object sender, EventArgs e)
        {
            ButtonSpeciesSort_Click(sender, e);
            var SpeciesList = new List<ComboItem>(GameInfo.SpeciesDataSource);
            var query = PkmData.GroupBy(
                mon => mon.Species,
                mon => getGame(mon.Identifier),
                (species, game) => new
                {
                    Name = PkmListAny.Find(p => p.Value == species).Text,
                    Counts = getGameCounts(species, game)
                }) ;
            var results = new FormGameTally();
            results.Show();
            foreach (var q in query)
                results.addEntry(String.Format("{0}; {1}", q.Name, q.Counts));
        }

        private string getGameCounts(int index, IEnumerable<string> game)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var entry in gameDict)
            {
                if(getGameMons(entry.Value.version,index))
                    d.Add(entry.Key, 0);
                else
                    d.Add(entry.Key, -1);
            }
            int count;
            foreach(string s in game)
            {
                d.TryGetValue(s, out count);

                d[s] = count + 1;
            }
                 
            string result = "";
            foreach(var entry in d)
            {
                if (entry.Value < 0)
                    result += "; ";
                else
                    result += entry.Key + ": " + entry.Value.ToString() + "; ";
            }
            return result;
        }

        private bool getGameMons(GameVersion version, int species)
        {
            return monInGame[new Tuple<GameVersion, int>(version, species)];
        }

        private string getGame(string identifier)
        {
            string[] strings = identifier.Split('\\');
            int count = strings.Count();
            return strings[count - 2];
        }

        private int getGen(string identifier)
        {
            string sub = identifier.Substring(identifier.IndexOf(".p"));
            return int.Parse(sub.Substring(3));
        }

        private string getTrainer(string identifier)
        {
            int start = identifier.IndexOf("[");
            int end = identifier.IndexOf("]");
            string sub = identifier.Substring(start, end - start - 1);
            return identifier;
        }

        private void ButtonNiqCalc_Click(object sender, EventArgs e)
        {
            var results = new FormNiqCalc();

            results.loadDB(PkmData, slotSelected);
            results.showValues();
            results.Show();
        }

        private void ButtonEggs_Click(object sender, EventArgs e)
        {
            var results = new FormEggCalc();

            results.loadDB(PkmData, slotSelected,ver.Version);
            results.showValues();
            results.Show();
        }

        private void setStatText(int nature, int gen)
        {

            labelAttack.ForeColor = Color.Black;
            labelDefense.ForeColor = Color.Black;
            labelSpAtk.ForeColor = Color.Black;
            labelSpDef.ForeColor = Color.Black;
            labelSpeed.ForeColor = Color.Black;

            if(gen >= 3)
            {
                int plus = (nature / 5);
                int minus = nature % 5;

                if (plus != minus)
                {
                    switch (plus)
                    {
                        case 0:
                            labelAttack.ForeColor = Color.Red;
                            break;
                        case 1:
                            labelDefense.ForeColor = Color.Red;
                            break;
                        case 2:
                            labelSpeed.ForeColor = Color.Red;
                            break;
                        case 3:
                            labelSpAtk.ForeColor = Color.Red;
                            break;
                        case 4:
                            labelSpDef.ForeColor = Color.Red;
                            break;
                    }

                    switch (minus)
                    {
                        case 0:
                            labelAttack.ForeColor = Color.Blue;
                            break;
                        case 1:
                            labelDefense.ForeColor = Color.Blue;
                            break;
                        case 2:
                            labelSpeed.ForeColor = Color.Blue;
                            break;
                        case 3:
                            labelSpAtk.ForeColor = Color.Blue;
                            break;
                        case 4:
                            labelSpDef.ForeColor = Color.Blue;
                            break;
                    }
                }
                else
                {
                    switch (plus)
                    {
                        case 0:
                            labelAttack.ForeColor = Color.Purple;
                            break;
                        case 1:
                            labelDefense.ForeColor = Color.Purple;
                            break;
                        case 2:
                            labelSpeed.ForeColor = Color.Purple;
                            break;
                        case 3:
                            labelSpAtk.ForeColor = Color.Purple;
                            break;
                        case 4:
                            labelSpDef.ForeColor = Color.Purple;
                            break;
                    }
                }
            }

        }
    }
}
