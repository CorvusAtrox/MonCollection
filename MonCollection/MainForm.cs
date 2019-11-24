using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonCollection.Properties;
using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.WinForms;
using TrueRandomGenerator;

namespace MonCollection
{
    public partial class MainForm : Form
    {
        private SaveFile ver;
        private readonly string[] genders = { "M", "F", "N" };
        private readonly LegalMoveSource LegalMoveSource = new LegalMoveSource();
        private ComboBox[] moveBoxes;
        private PictureBox[] PKXBOXES;
        private PictureBox[] ribbonBoxes;
        private int slotSelected = -1; // = null;
        private LegalityAnalysis legal;
        private List<MonData> PkmData;
        private List<MonData> FullPkmData;
        private List<int> PKMIndices;

        private List<string> filterGames;
        private List<string> gameList;

        private List<ComboItem> PkmListAny;
        private Dictionary<Tuple<GameVersion, int>, bool> monInGame; 

        private readonly string Counter = "Mon Count: {0}";

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;
        private int maxIndex = 0;

        private int[] majorGenderDiff;
        private int[] minorGenderDiff;
        private int[] noDiff;
        private string[] languages;


        public static DrawConfig Draw = new DrawConfig();

        public Dictionary<string,SaveInfo> gameDict;

        public MainForm()
        {
            InitializeComponent();
            InitializeGameDict();
            InitializeMonLists();
            InitializeStrings("en",GameVersion.SH,"blank");
            InitializeBinding();
            InitializePkxBoxes();
            PopulateFilteredDataSources();
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

            filterGames = new List<string>();

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
                filterGames.Add(split[0]);
                gameList = new List<string>(filterGames);
            }

            dict.Close();
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
                                      GameVersion.GO,
                                      GameVersion.SN, GameVersion.MN, GameVersion.US, GameVersion.UM,
                                      GameVersion.GP, GameVersion.GE,
                                      GameVersion.SW, GameVersion.SH};
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
                                         457, 459, 460, 461, 464, 465, 473, 133};
            noDiff = new int[] { 414, 493, 664, 665, 744, 773 };
            languages = new string[]{ "", "ja", "en", "fr", "it", "de", "", "es", "ko", "zh", "zh2" };
        }

        private void InitializeStrings(string spr, GameVersion gv, string trainer)
        {
            if (gv == GameVersion.Unknown)
                gv = GameVersion.SH;
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
                comboBoxBalls, comboBoxSpecies, comboBoxLanguage, comboBoxAbility, comboBoxNature
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

            ribbonBoxes = new[]{
                ribbonPic0, ribbonPic1, ribbonPic2, ribbonPic3, ribbonPic4
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

        private void PopulateFilteredDataSources()
        {
            var source = GameInfo.FilteredSources;

            comboBoxLanguage.DataSource = source.Languages;

            comboBoxBalls.DataSource = new BindingSource(source.Balls, null);
            comboBoxSpecies.DataSource = new BindingSource(source.Species, null);
            comboBoxAbility.DataSource = new BindingSource(source.Abilities, null);
            comboBoxNature.DataSource = new BindingSource(source.Natures, null);

            // Set the Move ComboBoxes too..
            LegalMoveSource.ReloadMoves(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);

            comboBoxGame.Items.Clear();
            foreach (var entry in gameDict)
                comboBoxGame.Items.Add(entry.Key.ToString());
        }

        private bool OpenPKM(MonData pk)
        {
            if (pk == null)
                return false;
            SetVersion(pk.Game);
            PopulateFields(pk);
            return true;
        }

        private void SetVersion(string identifier)
        {
            if (identifier == null)
                identifier = "";

            if (!gameDict.TryGetValue(identifier, out SaveInfo info))
                InitializeStrings("en", GameVersion.SH, "blank");
            else
                InitializeStrings(info.language, info.version, GetTrainer(identifier));

            PopulateFilteredDataSources();

        }

        private int GameIndex(string identifier)
        {
            if (identifier == null)
                identifier = "";

            if (!gameDict.TryGetValue(identifier, out SaveInfo info))
                return 0;
            else
                return info.index;
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
                default:
                    mon = new PK8();
                    break;
            }
            mon.Species = data.Species;
            mon.AltForm = data.AltForm;
            mon.CurrentLevel = data.Level;
            if(gameDict.TryGetValue(data.Game, out SaveInfo val))
                mon.Version = (int)val.version;
            return mon;
        }

        private void PopulateFields(MonData pk)
        {
            legal = new LegalityAnalysis(MonDataToPKM(pk),ver.Personal);
            LegalMoveSource.ReloadMoves(legal.AllSuggestedMovesAndRelearn());
            foreach(ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.DataSource, null);
            }

            switch (ver.Generation)
            {
                case 1:
                    labelGender.Visible = false;
                    labelBall.Visible = false;
                    comboBoxBalls.Visible = false;
                    labelAbility.Visible = false;
                    comboBoxAbility.Visible = false;
                    labelRibbons.Visible = false;
                    labelNature.Visible = false;
                    comboBoxNature.Visible = false;
                    comboBoxPlus.Visible = false;
                    comboBoxMinus.Visible = false;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    comboBoxPkrs.Visible = false;
                    buttonEggs.Visible = false;
                    labelDynamax.Visible = false;
                    textBoxDynaLv.Visible = false;
                    break;
                case 2:
                    labelGender.Visible = true;
                    labelBall.Visible = false;
                    comboBoxBalls.Visible = false;
                    labelAbility.Visible = false;
                    comboBoxAbility.Visible = false;
                    labelRibbons.Visible = false;
                    labelNature.Visible = false;
                    comboBoxPlus.Visible = false;
                    comboBoxMinus.Visible = false;
                    comboBoxNature.Visible = false;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    comboBoxPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    labelDynamax.Visible = false;
                    textBoxDynaLv.Visible = false;
                    break;
                case 3:
                case 4:
                case 5:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    labelAbility.Visible = true;
                    comboBoxAbility.Visible = true;
                    labelRibbons.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    comboBoxPlus.Visible = false;
                    comboBoxMinus.Visible = false;
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    comboBoxPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    labelDynamax.Visible = false;
                    textBoxDynaLv.Visible = false;
                    break;
                case 6:
                case 7:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    labelAbility.Visible = true;
                    comboBoxAbility.Visible = true;
                    labelRibbons.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    comboBoxPlus.Visible = false;
                    comboBoxMinus.Visible = false;
                    labelLanguage.Visible = true;
                    comboBoxLanguage.Visible = true;
                    comboBoxPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    labelDynamax.Visible = false;
                    textBoxDynaLv.Visible = false;
                    break;
                case 8:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    labelAbility.Visible = true;
                    comboBoxAbility.Visible = true;
                    labelRibbons.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    comboBoxPlus.Visible = true;
                    comboBoxMinus.Visible = true;
                    labelLanguage.Visible = true;
                    comboBoxLanguage.Visible = true;
                    comboBoxPkrs.Visible = true;
                    buttonEggs.Visible = true;
                    labelDynamax.Visible = true;
                    textBoxDynaLv.Visible = true;
                    break;
            }

            textBoxNickname.Text = pk.Nickname;
            comboBoxBalls.SelectedValue = pk.Ball;
            comboBoxSpecies.SelectedValue = pk.Species;
            comboBoxLanguage.SelectedValue = pk.Language;
            comboBoxAbility.SelectedValue = pk.Ability;
            comboBoxNature.SelectedValue = pk.Nature;

            if(pk.Moves == null)
                pk.Moves = new List<int>{0,0,0,0};

            while(pk.Moves.Count < 4)
            {
                pk.Moves.Add(0);
            }

            comboBoxMove1.SelectedValue = pk.Moves[0];
            comboBoxMove2.SelectedValue = pk.Moves[1];
            comboBoxMove3.SelectedValue = pk.Moves[2];
            comboBoxMove4.SelectedValue = pk.Moves[3];

            BTN_Shinytize.Tag = pk.Shiny;
            SetShiny((bool)BTN_Shinytize.Tag);

            textBoxHP.Text = pk.HP.ToString();
            textBoxAttack.Text = pk.ATK.ToString();
            textBoxDefense.Text = pk.DEF.ToString();
            textBoxSpAtk.Text = pk.SPA.ToString();
            textBoxSpDef.Text = pk.SPD.ToString();
            textBoxSpeed.Text = pk.SPE.ToString();
            if(pk.Gen >= 3)
            {
                if(pk.Boon == 0 || pk.Bane == 0)
                {
                    pk.Boon = (pk.Nature / 5) + 1;
                    pk.Bane = (pk.Nature % 5) + 1;
                }

            }
            else
            {
                pk.Boon = 0;
                pk.Bane = 0;
            }
            comboBoxPlus.SelectedIndex = pk.Boon;
            comboBoxMinus.SelectedIndex = pk.Bane;
                
            SetStatText(pk.Boon,pk.Bane);

            foreach (PictureBox pb in ribbonBoxes)
                pb.Image = null;

            if(pk.Ribbons != null)
            {
                int r = 0;
                foreach(string rib in pk.Ribbons)
                {
                    ribbonBoxes[r].Image = RetrieveImage("Resources/img/ribbon/" + rib + ".png");
                    r++;
                }
            }

            textBoxOT.Text = pk.OT;
            textBoxID.Text = pk.ID.ToString();
            comboBoxGame.Text = pk.Game;

            textBoxLevel.Text = pk.Level.ToString();

            pictureBoxBall.Image = RetrieveImage("Resources/img/ball/" + pk.Ball + ".png");
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
            if (pk.Species == 25 && pk.Gen == 6)
                spForm += "c";
            pictureBoxIcon.Image = RetrieveImage("Resources/img/icons/" + spForm + ".png");
            if(pictureBoxIcon.Image != null)
            {
                if (pictureBoxIcon.Image.Height > 56)
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            if (minorGenderDiff.Contains(pk.Species))
            {
                if(pk.AltForm == 0 && pk.Gen >= 4 && !(pk.Species == 133 && pk.Gen <= 7))
                {
                    if (pk.Gender == 0)
                        spForm += "m";
                    else if (pk.Gender == 1)
                        spForm += "f";
                }
            }

            if (pk.Game == null)
                pk.Game = "";

            gameDict.TryGetValue(pk.Game, out SaveInfo si);
            pictureBoxGameSprite.SizeMode = PictureBoxSizeMode.AutoSize;
            if(si != null)
                pictureBoxGameSprite.Image = GetSprite(spForm, si.version,pk.Shiny);
            else
                pictureBoxGameSprite.Image = GetSprite(spForm, GameVersion.SH, pk.Shiny);
            if (pictureBoxGameSprite.Height > 180 || pictureBoxGameSprite.Width > 180)
            {
                double ratio = pictureBoxGameSprite.Height / pictureBoxGameSprite.Width;
                if (ratio >= 1)
                {
                    pictureBoxGameSprite.Height = 180;
                    pictureBoxGameSprite.Width = (int)(180 / ratio);
                }
                else
                {
                    pictureBoxGameSprite.Width = 180;
                    pictureBoxGameSprite.Height = (int)(180 * ratio);
                }
                pictureBoxGameSprite.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            pictureBoxGameSprite.Refresh();
            labelGender.Text = genders[pk.Gender];
            labelGender.Tag = pk.Gender;
            SetShiny(pk.Shiny);
            if (pk.PKRS_Infected)
            {
                pictureBoxPkrs.Visible = true;
                if (pk.PKRS_Cured)
                {
                    comboBoxPkrs.SelectedIndex = 2;
                    pictureBoxPkrs.Image = RetrieveImage("Resources/img/pkrsCured.png");
                }
                else
                {
                    comboBoxPkrs.SelectedIndex = 1;
                    pictureBoxPkrs.Image = RetrieveImage("Resources/img/pkrsInfected.png");
                }
            }
            else
            {
                pictureBoxPkrs.Visible = false;
                comboBoxPkrs.SelectedIndex = 0;
            }

            if (pk.gMax)
                pictureBoxGMax.Image = RetrieveImage("Resources/img/gMax.png");
            else
                pictureBoxGMax.Image = null;
            textBoxDynaLv.Text = pk.dynaLevel.ToString();
            if(pk.Species != 869)
            {
                var ds = FormConverter.GetFormList(pk.Species, GameInfo.Strings.types, GameInfo.Strings.forms, genders, pk.Gen);
                comboBoxForm.DataSource = ds;
            } else
            {
                string[] ds = { "Strawberry Vanilla Cream", "Berry Vanilla Cream", "Love Vanilla Cream", "Star Vanilla Cream",
                                "Clover Vanilla Cream", "Flower Vanilla Cream", "Ribbon Vanilla Cream", "Strawberry Ruby Cream",
                                "Berry Ruby Cream", "Lover Ruby Cream", "Star Ruby Cream", "Clover Ruby Cream", "Flower Ruby Cream",
                                "Ribbon Ruby Cream", "Strawberry Matcha Cream", "Berry Matcha Cream", "Love Matcha Cream",
                                "Star Matcha Cream", "Clover Matcha Cream", "Flower Matcha Cream", "Ribbon Matcha Cream",
                                "Strawberry Mint Cream", "Berry Mint Cream", "Love Mint Cream", "Star Mint Cream", "Clover Mint Cream",
                                "Flower Mint Cream", "Ribbon Mint Cream", "Strawberry Lemon Cream", "Berry Lemon Cream",
                                "Love Lemon Cream", "Star Lemon Cream", "Clover Lemon Cream", "Flower Lemon Cream", "Ribbon Lemon Cream",
                                "Strawberry Salted Cream", "Berry Salted Cream", "Love Salted Cream", "Star Salted Cream",
                                "Clover Salted Cream", "Flower Salted Cream", "Ribbon Salted Cream", "Strawberry Ruby Swirl",
                                "Berry Ruby Swirl", "Love Ruby Swirl", "Star Ruby Swirl", "Clover Ruby Swirl", "Flower Ruby Swirl",
                                "Ribbon Ruby Swirl", "Strawberry Caramel Swirl", "Berry Caramel Swirl", "Love Caramel Swirl",
                                "Star Caramel Swirl", "Clover Caramel Swirl", "Flower Caramel Swirl", "Ribbon Caramel Swirl",
                                "Strawberry Rainbow Swirl", "Berry Rainbow Swirl", "Love Rainbow Swirl", "Star Rainbow Swirl",
                                "Clover Rainbow Swirl", "Flower Rainbow Swirl", "Ribbon Rainbow Swirl"};
                comboBoxForm.DataSource = ds;
            }
            
            comboBoxForm.SelectedIndex = pk.AltForm;
            if (comboBoxForm.Items.Count != 1)
                comboBoxForm.Visible = true;
            else
                comboBoxForm.Visible = false;

            PlayCry(pk.Species, pk.AltForm);
        }

        private void PlayCry(int sp, int form)
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
                sound.Dispose();
            }
        }

        private Image GetSprite(string species, GameVersion version, bool shiny)
        {
            string game = "";
            string ext = "";
            switch (version)
            {
                case GameVersion.Unknown:
                    game = "icons";
                    ext = ".png";
                    shiny = false; //Shiny not visible
                    break;
                case GameVersion.RD:
                case GameVersion.GN:
                    game = "rb";
                    ext = ".png";
                    shiny = false; //Shiny not visible
                    break;
                case GameVersion.YW:
                    game = "yw";
                    ext = ".png";
                    shiny = false; //Shiny not visible
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
                case GameVersion.GO:
                    game = "go";
                    ext = ".png";
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
                    Regex genDiff = new Regex("[0-9]+(f|m)");
                    game = "lgpe";
                    if (genDiff.IsMatch(species))
                        species = species.Substring(0, species.Length - 1);
                    ext = ".png";
                    shiny = false;
                    break;
                case GameVersion.SW:
                case GameVersion.SH:
                    game = "swsh";
                    ext = ".png";
                    break;
            }
            if(!shiny)
                return RetrieveImage("Resources/img/"+game+"/"+species+ext);
            else
                return RetrieveImage("Resources/img/" + game + "/rare/" + species + ext);
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
            FullPkmData = LoadPKMSaves();

            if (PkmData != null)
                UpdateFullData();

            PkmData = ApplyFilters(FullPkmData);

            BeginInvoke(new MethodInvoker(() => SetResults(PkmData)));
        }

        private static List<MonData> LoadPKMSaves()
        {
            if (!File.Exists(Settings.Default.mons + "/mons.json"))
                File.Create(Settings.Default.mons + "/mons.json").Dispose();

            using (StreamReader r = new StreamReader(Settings.Default.mons + "/mons.json"))
            {
                string json = r.ReadToEnd();
                List<MonData> mons = JsonConvert.DeserializeObject<List<MonData>>(json);
                return mons;
            }
        }

        private Image RetrieveImage(string path)
        {
            if (File.Exists(path))
                return Image.FromFile(path);
            else
                return null;
        }

        private void SetResults(List<MonData> res)
        {
            SCR_Box.Maximum = (int)Math.Ceiling((decimal)res.Count / RES_MIN);
            if (SCR_Box.Maximum > 0) SCR_Box.Maximum--;

            slotSelected = 0; // reset the slot last viewed
            SCR_Box.Value = 0;
            GameSpeciesSort(0);

            L_Count.Text = string.Format(Counter, res.Count());
            maxIndex = res.Count - 1;
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
                MonData mon = PkmData[i + begin];
                string spForm = mon.Species.ToString();
                if (mon.AltForm > 0 && !noDiff.Contains(mon.Species))
                    spForm += "-" + mon.AltForm.ToString();
                else if (majorGenderDiff.Contains(mon.Species))
                {
                    if (mon.Gender == 0)
                        spForm += "m";
                    else if (mon.Gender == 1)
                        spForm += "f";
                }
                if (mon.Species == 25 && mon.Gen == 6)
                    spForm += "c";
                PKXBOXES[i].Image = RetrieveImage("Resources/img/icons/" + spForm + ".png");
                if(PKXBOXES[i].Image != null)
                {
                    if (PKXBOXES[i].Image.Height > 56)
                        PKXBOXES[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    else
                        PKXBOXES[i].SizeMode = PictureBoxSizeMode.CenterImage;
                }
                PKXBOXES[i].Tag = i + begin;
            }
            for (int i = end; i < RES_MAX; i++)
            {
                PKXBOXES[i].Image = null;
                PKXBOXES[i].Tag = -1;
            }

            for (int i = 0; i < RES_MAX; i++)
                PKXBOXES[i].BackgroundImage = GetSlotImg((int)PKXBOXES[i].Tag,false);
            if (slotSelected != -1 && slotSelected >= begin && slotSelected < begin + RES_MAX)
                PKXBOXES[slotSelected - begin].BackgroundImage = GetSlotImg((int)PKXBOXES[slotSelected - begin].Tag, true);

        }

        private Image GetSlotImg(int slot, bool selected)
        {
            Image img = null;

            if (slot == -1)
                return img;

            if (PkmData[slot].Game == null)
                PkmData[slot].Game = "";

            gameDict.TryGetValue(PkmData[slot].Game, out SaveInfo si);

            int verId;

            if (si != null)
                verId = (int)si.version;
            else
                verId = 0;

            if (selected)
            {
                img = RetrieveImage("Resources/img/slots/selected/" + verId.ToString() + ".png");

                if (img == null)
                    img = RetrieveImage("Resources/img/slotView.png");
            }
            else
                img = RetrieveImage("Resources/img/slots/" + verId.ToString() + ".png");

            return img;
        }

        private void SCR_Box_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                FillPKXBoxes(e.NewValue);
        }

        public void SpeciesGameSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => mon.Species)
                             .ThenBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void OTSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => mon.ID)
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void GenSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => mon.Gen)
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }


        public void GameLevelSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void GameSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Species)
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        private void ButtonGameLevelSort_Click(object sender, EventArgs e)
        {
            GameLevelSort((int)bpkx1.Tag/RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGameSpeciesSort_Click(object sender, EventArgs e)
        {
            GameSpeciesSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGenSpeciesSort_Click(object sender, EventArgs e)
        {
            GenSpeciesSort((int)bpkx1.Tag / RES_MIN);
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
                mon => mon.Game,
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
            SpeciesGameSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonOTSort_Click(object sender, EventArgs e)
        {
            OTSpeciesSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonGameMonTally_Click(object sender, EventArgs e)
        {
            ButtonSpeciesSort_Click(sender, e);
            var SpeciesList = new List<ComboItem>(GameInfo.SpeciesDataSource);
            var query = PkmData.GroupBy(
                mon => mon.Species,
                mon => mon.Game,
                (species, game) => new
                {
                    Name = PkmListAny.Find(p => p.Value == species).Text,
                    Counts = GetGameCounts(species, game)
                }) ;
            var results = new FormGameTally();
            results.Show();
            foreach (var q in query)
                results.addEntry(String.Format("{0}; {1}", q.Name, q.Counts));
        }


        private void ButtonMoveMonTally_Click(object sender, EventArgs e)
        {
            ButtonSpeciesSort_Click(sender, e);

            var SpeciesList = new List<ComboItem>(GameInfo.SpeciesDataSource);
            var query = PkmData.GroupBy(
                mon => mon.Species,
                mon => mon.Moves,
                (species, moves) => new
                {
                    Name = PkmListAny.Find(p => p.Value == species).Text,
                    Counts = GetMoveCounts(moves)
                });
            var results = new FormGameTally();
            results.Show();
            foreach (var q in query)
                results.addEntry(String.Format("{0}; {1}", q.Name, q.Counts));
        }

        private void ButtonMonBallTally_Click(object sender, EventArgs e)
        {
            ButtonSpeciesSort_Click(sender, e);
            var SpeciesList = new List<ComboItem>(GameInfo.SpeciesDataSource);
            var query = PkmData.GroupBy(
                mon => mon.Species,
                mon => mon.Ball,
                (species, ball) => new
                {
                    Name = PkmListAny.Find(p => p.Value == species).Text,
                    Counts = GetBallCounts(ball)
                });
            var results = new FormGameTally();
            results.Show();
            foreach (var q in query)
                results.addEntry(String.Format("{0}; {1}", q.Name, q.Counts));
        }

        private void ButtonRanMon_Click(object sender, EventArgs e)
        {
            int rnd = RandomNumberGenerator.GetRandomInt(0, maxIndex);
            if(rnd != -1)
            {
                int val = (int)(rnd / RES_MIN) - 2;
                if (val < 0)
                    val = 0;

                SCR_Box.Value = val;
                slotSelected = rnd;
                FillPKXBoxes(val);
                OpenPKM(PkmData[rnd]);
            }
            else
            {
                MessageBox.Show("Could not find random number");
            }
            
        }

        private string GetGameCounts(int index, IEnumerable<string> game)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (var entry in gameDict)
            {
                if(GetGameMons(entry.Value.version,index))
                    d.Add(entry.Key, 0);
                else
                    d.Add(entry.Key, -1);
            }

            foreach (string s in game)
            {
                d.TryGetValue(s, out int count);

                if (count < 0)
                    count = 0;

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

        private string GetMoveCounts(IEnumerable<List<int>> moves)
        {
            List<int> allMoves = new List<int>();
            foreach (var m in moves)
                foreach (int i in m)
                    if(i != 0)
                        allMoves.Add(i);

            allMoves.Sort();

            string result = "";

            var moveGroups = allMoves.GroupBy(i => i);

            var moveNames = new List<ComboItem>(GameInfo.MoveDataSource);

            foreach (var mv in moveGroups)
                result += moveNames.Find(p => p.Value == mv.Key).Text + ": " + mv.Count() + "; ";

            return result;
        }

        private string GetBallCounts(IEnumerable<int> balls)
        {
            List<int> allBalls = new List<int>();
            foreach (var m in balls)
                if (m != 0)
                    allBalls.Add(m);

            allBalls.Sort();

            string result = "";

            var ballGroups = allBalls.GroupBy(i => i);

            var ballNames = new List<ComboItem>(GameInfo.BallDataSource);

            foreach (var bl in ballGroups)
                result += ballNames.Find(p => p.Value == bl.Key).Text + ": " + bl.Count() + "; ";

            return result;
        }

        private bool GetGameMons(GameVersion version, int species)
        {
            if(version == GameVersion.Unknown)
                return monInGame[new Tuple<GameVersion, int>(GameVersion.UM, species)];
            else
                return monInGame[new Tuple<GameVersion, int>(version, species)];
        }

        private string GetTrainer(string identifier)
        {
            int start = identifier.IndexOf("[");
            int end = identifier.IndexOf("]");
            if (start > -1 && end > 0)
                return identifier.Substring(start, end - start - 1);
            else
                return null;
        }

        private void ButtonNiqCalc_Click(object sender, EventArgs e)
        {
            var results = new FormNiqCalc();

            results.LoadDB(PkmData, slotSelected);
            results.ShowValues();
            results.Show();
        }

        private void ButtonEggs_Click(object sender, EventArgs e)
        {
            var results = new FormEggCalc();

            results.loadDB(PkmData, slotSelected, ver.Version);
            results.showValues();
            results.Show();
        }

        private void SetStatText(int boon, int bane)
        {

            labelAttack.ForeColor = Color.Black;
            labelDefense.ForeColor = Color.Black;
            labelSpAtk.ForeColor = Color.Black;
            labelSpDef.ForeColor = Color.Black;
            labelSpeed.ForeColor = Color.Black;
            textBoxAttack.ForeColor = Color.Black;
            textBoxDefense.ForeColor = Color.Black;
            textBoxSpAtk.ForeColor = Color.Black;
            textBoxSpDef.ForeColor = Color.Black;
            textBoxSpeed.ForeColor = Color.Black;

            if (boon !=0 && bane != 0)
            {
                if (boon != bane)
                {
                    switch (boon)
                    {
                        case 1:
                            labelAttack.ForeColor = Color.Red;
                            textBoxAttack.ForeColor = Color.Red;
                            break;
                        case 2:
                            labelDefense.ForeColor = Color.Red;
                            textBoxDefense.ForeColor = Color.Red;
                            break;
                        case 3:
                            labelSpeed.ForeColor = Color.Red;
                            textBoxSpeed.ForeColor = Color.Red;
                            break;
                        case 4:
                            labelSpAtk.ForeColor = Color.Red;
                            textBoxSpAtk.ForeColor = Color.Red;
                            break;
                        case 5:
                            labelSpDef.ForeColor = Color.Red;
                            textBoxSpDef.ForeColor = Color.Red;
                            break;
                    }

                    switch (bane)
                    {
                        case 1:
                            labelAttack.ForeColor = Color.Blue;
                            textBoxAttack.ForeColor = Color.Blue;
                            break;
                        case 2:
                            labelDefense.ForeColor = Color.Blue;
                            textBoxDefense.ForeColor = Color.Blue;
                            break;
                        case 3:
                            labelSpeed.ForeColor = Color.Blue;
                            textBoxSpeed.ForeColor = Color.Blue;
                            break;
                        case 4:
                            labelSpAtk.ForeColor = Color.Blue;
                            textBoxSpAtk.ForeColor = Color.Blue;
                            break;
                        case 5:
                            labelSpDef.ForeColor = Color.Blue;
                            textBoxSpDef.ForeColor = Color.Blue;
                            break;
                    }
                }
                else
                {
                    switch (boon)
                    {
                        case 1:
                            labelAttack.ForeColor = Color.Purple;
                            textBoxAttack.ForeColor = Color.Purple;
                            break;
                        case 2:
                            labelDefense.ForeColor = Color.Purple;
                            textBoxDefense.ForeColor = Color.Purple;
                            break;
                        case 3:
                            labelSpeed.ForeColor = Color.Purple;
                            textBoxSpeed.ForeColor = Color.Purple;
                            break;
                        case 4:
                            labelSpAtk.ForeColor = Color.Purple;
                            textBoxSpAtk.ForeColor = Color.Purple;
                            break;
                        case 5:
                            labelSpDef.ForeColor = Color.Purple;
                            textBoxSpDef.ForeColor = Color.Purple;
                            break;
                    }
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateFullData();

            string json = JsonConvert.SerializeObject(FullPkmData,Formatting.Indented);

            //write string to file
            File.WriteAllText(Settings.Default.mons + "/mons.json", json);
        }

        private void UpdateFullData()
        {
            for(int i = 0; i < PkmData.Count(); i++)
            {
                if (i < PKMIndices.Count())
                    FullPkmData[PKMIndices[i]] = PkmData[i];
                else
                    FullPkmData.Add(PkmData[i]);
            }
        }

        private void ButtonRevertMon_Click(object sender, EventArgs e)
        {
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonSaveMon_Click(object sender, EventArgs e)
        {
            SaveMon();
            OpenPKM(PkmData[slotSelected]);
        }

        private void SaveMon()
        {
            MonData mon = PkmData[slotSelected];

            mon.Nickname = textBoxNickname.Text;
            mon.Level = int.Parse(textBoxLevel.Text);
            mon.Gender = (int)labelGender.Tag;
            mon.Species = (int?) comboBoxSpecies.SelectedValue ?? 0;
            if(comboBoxForm.Visible == true)
                mon.AltForm = comboBoxForm.SelectedIndex;
            if(comboBoxAbility.SelectedValue != null)
                mon.Ability = (int)comboBoxAbility.SelectedValue;
            mon.Nature = (int)comboBoxNature.SelectedValue;
            mon.Boon = comboBoxPlus.SelectedIndex;
            mon.Bane = comboBoxMinus.SelectedIndex;
            mon.Moves = new List<int> { (int?)comboBoxMove1.SelectedValue ?? 0, (int?)comboBoxMove2.SelectedValue ?? 0,
                                        (int?)comboBoxMove3.SelectedValue ?? 0, (int?)comboBoxMove4.SelectedValue ?? 0 };
            mon.Game = comboBoxGame.Text;
            mon.OT = textBoxOT.Text;
            mon.ID = int.Parse(textBoxID.Text);
            mon.Shiny = (bool)BTN_Shinytize.Tag;
            mon.HP = int.Parse(textBoxHP.Text);
            mon.ATK = int.Parse(textBoxAttack.Text);
            mon.DEF = int.Parse(textBoxDefense.Text);
            mon.SPA = int.Parse(textBoxSpAtk.Text);
            mon.SPD = int.Parse(textBoxSpDef.Text);
            mon.SPE = int.Parse(textBoxSpeed.Text);
            mon.dynaLevel = int.Parse(textBoxDynaLv.Text);
            if(comboBoxBalls.SelectedValue != null)
                mon.Ball = (int)comboBoxBalls.SelectedValue;
            if (comboBoxLanguage.SelectedValue != null)
                mon.Language = (int)comboBoxLanguage.SelectedValue;

            switch (comboBoxPkrs.SelectedIndex)
            {
                case 0:
                    mon.PKRS_Cured = false;
                    mon.PKRS_Infected = false;
                    break;
                case 1:
                    mon.PKRS_Cured = false;
                    mon.PKRS_Infected = true;
                    break;
                case 2:
                    mon.PKRS_Cured = true;
                    mon.PKRS_Infected = true;
                    break;
            }

            PkmData[slotSelected] = mon;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(PkmData, Formatting.Indented);

            //write string to file
            File.WriteAllText(Settings.Default.mons + "/mons.json", json);
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            slotSelected = PkmData.Count();
            int val = ((slotSelected) / RES_MIN);
            if (val > SCR_Box.Maximum)
                SCR_Box.Maximum = val;
            PkmData.Add(new MonData());
            SCR_Box.Value = val;
            FillPKXBoxes(val);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    var files = Directory.EnumerateFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories);
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
                        pk.SetStats(pk.GetStats(pk.PersonalInfo));

                        MonData pd = new MonData
                        {
                            Nickname = pk.Nickname,
                            Species = pk.Species,
                            Level = pk.CurrentLevel,
                            Gender = pk.Gender,
                            Moves = new List<int> { pk.Move1, pk.Move2, pk.Move3, pk.Move4 },
                            Game = GetGame(pk.Identifier),
                            AltForm = pk.AltForm,
                            Shiny = pk.IsShiny,
                            Ability = pk.Ability,
                            Nature = pk.Nature,
                            Boon = 0,
                            Bane = 0,
                            HP = pk.Stat_HPMax,
                            ATK = pk.Stat_ATK,
                            DEF = pk.Stat_DEF,
                            SPA = pk.Stat_SPA,
                            SPD = pk.Stat_SPD,
                            SPE = pk.Stat_SPE,
                            Gen = GetGen(pk.Identifier),
                            ID = pk.DisplayTID,
                            OT = pk.OT_Name,
                            Ball = pk.Ball,
                            Language = pk.Language,
                            PKRS_Infected = pk.PKRS_Infected,
                            PKRS_Cured = pk.PKRS_Cured,
                            Ribbons = { }
                        };

                        PkmData.Add(pd);
                    });

                    MessageBox.Show("Pokemon Data Imported!");

                    string json = JsonConvert.SerializeObject(PkmData, Formatting.Indented,
                                  new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                    //write string to file
                    File.WriteAllText(Settings.Default.mons + "/mons.json", json);

                    LoadDatabase();

                    FillPKXBoxes((int)(slotSelected/RES_MIN));
                    OpenPKM(PkmData[slotSelected]);

                }
            }
        }

        private string GetGame(string identifier)
        {
            string[] strings = identifier.Split('\\');
            int count = strings.Count();
            return strings[count - 2];
        }

        private int GetGen(string identifier)
        {
            string sub = Regex.Match(identifier, @"\.[pcx][kb][0-9]*$").Value;
            return int.Parse(sub.Substring(3));
        }

        private void TextBoxLevel_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(textBoxLevel.Text,out int newLev))
            {
                MonData mon = PkmData[slotSelected];
                if(mon.Level != 0)
                {
                    if (mon.HP >= 11)
                        textBoxHP.Text = (((mon.HP - 10 - mon.Level) * newLev / mon.Level) + 10 + newLev).ToString();
                    if (mon.ATK >= 5)
                        textBoxAttack.Text = (((mon.ATK - 5) * newLev / mon.Level) + 5).ToString();
                    if (mon.DEF >= 5)
                        textBoxDefense.Text = (((mon.DEF - 5) * newLev / mon.Level) + 5).ToString();
                    if (mon.SPA >= 5)
                        textBoxSpAtk.Text = (((mon.SPA - 5) * newLev / mon.Level) + 5).ToString();
                    if (mon.SPD >= 5)
                        textBoxSpDef.Text = (((mon.SPD - 5) * newLev / mon.Level) + 5).ToString();
                    if (mon.SPE >= 5)
                        textBoxSpeed.Text = (((mon.SPE - 5) * newLev / mon.Level) + 5).ToString();
                }
            }
        }

        private void LabelGender_Click(object sender, EventArgs e)
        {
            labelGender.Tag = ((int)labelGender.Tag + 1) % 3;
            labelGender.Text = genders[(int)labelGender.Tag];
        }

        private void GamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TO DO: Make popup for editing this
            FormGames games = new FormGames();
            games.FormClosed += new FormClosedEventHandler(delegate (object send, FormClosedEventArgs a) {
                games.updateGameIni();
                InitializeGameDict();
            });
            games.Show();
        }

        private void BTN_Shinytize_Click(object sender, EventArgs e)
        {
            BTN_Shinytize.Tag = !(bool)BTN_Shinytize.Tag;
            SetShiny((bool)BTN_Shinytize.Tag);
        }

        private void SetShiny(bool shiny)
        {
            if (shiny)
            {
                BTN_Shinytize.Text = "★";
                BTN_Shinytize.ForeColor = Color.Red;
            }
            else
            {
                BTN_Shinytize.Text = "☆";
                BTN_Shinytize.ForeColor = Color.Black;
            }
        }

        private void ComboBoxGame_SelectedValueChanged(object sender, EventArgs e)
        {
            SaveInfo si = gameDict[comboBoxGame.Text];
            PkmData[slotSelected].Gen = si.getGen();
            if (PkmData[slotSelected].Language == 0)
                PkmData[slotSelected].Language = Array.IndexOf(languages,si.language);
            if (PkmData[slotSelected].ID == 0 && (PkmData[slotSelected].OT == null || PkmData[slotSelected].OT == string.Empty))
            {
                textBoxOT.Text = si.ot;
                textBoxID.Text = si.id.ToString();
            }
        }

        private List<MonData> ApplyFilters(List<MonData> full)
        {
            List<MonData> mons = new List<MonData>();
            PKMIndices = new List<int>();

            foreach (MonData data in full)
            {
                if (filterGames.Contains(data.Game))
                {
                    mons.Add(data);
                    PKMIndices.Add(full.IndexOf(data));
                }
            }

            return mons;
        }

        private void GameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFilters form = new FormFilters
            {
                filters = filterGames
            };
            form.loadFilterList(gameList);
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a) {
                    form.updateFilters();
                    filterGames = form.filters;
                    LoadDatabase();
                });
            form.Show();
        }

        private void ButtonLevelTally_Click(object sender, EventArgs e)
        {
            var query = PkmData.GroupBy(
                mon => mon.Level,
                mon => mon.Nickname,
                (level, name) => new
                {
                    Key = level,
                    Count = name.Count()
                }).OrderBy(entry => entry.Key);
            var results = new FormGameTally();
            foreach (var q in query)
                results.addEntry(String.Format("{0}: {1}", q.Key, q.Count));
            results.Show();
        }
    }
}
