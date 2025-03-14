﻿using System;
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
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PKHeX.Core;
using PKHeX.WinForms;
using TrueRandomGenerator;

namespace MonCollection
{
    public partial class MainForm : Form
    {
        private SaveFile ver;
        private readonly string[] genders = { "M", "F", "N" };
        private readonly LegalMoveSource<ComboItem> LegalMoveSource = new(new LegalMoveComboSource());
        private ComboBox[] moveBoxes;
        private PictureBox[] PKXBOXES;
        private PictureBox[] ribbonBoxes;
        private int slotSelected = -1; // = null;
        private LegalityAnalysis legal;
        private List<MonData> PkmData;
        private List<MonData> FullPkmData;
        private List<int> PKMIndices;

        private List<string> filterGames;
        private List<string> filterOrigins;
        private List<int> filterSpecies;
        private (int, int) filterLevels;

        private List<string> gameList;

        private List<ComboItem> PkmListAny;
        private Dictionary<Tuple<GameVersion, int>, bool> monInGame;

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;
        private int maxIndex = 0;

        private const int numPokemon = 1025;

        private const int SV_PRIORITY = 1;
        private const int PLA_PRIORITY = 2;
        private const int SWSH_PRIORITY = 3;
        private const int LGPE_PRIORITY = 4;
        private const int BDSP_PRIORITY = 5;

        private enum Dexes
        {
            HomeDex,
            ScarletVioletDex,
            SwordShieldDex,
            BrilliantDiamondShiningPearlDex,
            LegendsArceusDex,
            NewSnapDex,
            SunMoonDex,
            UltraSunMoonDex,
            LetsGoDex,
            PinkMeteorDex
        }

        private enum VisibleSpecies
        {
            All,
            Available,
            Native
        }


        private ushort[] majorGenderDiff;
        private ushort[] genderDiff;
        private ushort[] noDiff;

        private Dictionary<ushort, byte> alolan;
        private Dictionary<ushort, byte> galarian;
        private Dictionary<ushort, byte> hisuian;
        private Dictionary<ushort, byte> paldean;

        private Dictionary<(ushort, byte), byte> galarianEvo;
        private Dictionary<(ushort, byte), byte> hisuianEvo;
        private Dictionary<(ushort, byte), byte> paldeanEvo;

        private Dictionary<int, int> monOrder;

        private int lastDex;
        private int lastVis;

        private List<MonSortOrder> dexes;

        private string[] languages;

        private MonFamily monFamily;

        public static DrawConfig Draw = new DrawConfig();

        public Dictionary<string, SaveInfo> gameDict;
        public Dictionary<string, int> regionDict;

        public List<string> monRibbons;
        public Dictionary<string, Movepools> monMovepools;

        public MainForm()
        {
            InitializeComponent();
            InitializeGameDict();
            InitializeMonLists();
            InitializeStrings("en", GameVersion.SV, "blank");
            InitializeBinding();
            InitializePkxBoxes();
            PopulateFilteredDataSources();
            L_Count.Text = "Loading...";
            new Task(LoadDatabase).Start();
        }

        private void InitializeGameDict()
        {
            gameDict = new Dictionary<string, SaveInfo>();
            regionDict = new Dictionary<string, int>();

            filterGames = new List<string>();
            filterOrigins = new List<string>();
            filterSpecies = new List<int>();
            filterLevels = (0, 100);

            for (int i = 1; i <= numPokemon; i++)
                filterSpecies.Add(i);

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
                filterGames.Add(split[0]);
            }

            dict.Close();

            gameList = new List<string>(filterGames);

            dict = new StreamReader(Settings.Default.mons + "/regions.ini");
            ind = 1;

            while (!dict.EndOfStream)
            {
                l = dict.ReadLine();
                split = l.Split(',');
                regionDict.Add(split[0], ind);
                filterOrigins.Add(split[0]);
                ind++;
            }

            dict.Close();
        }

        private void InitializeMonLists()
        {
            majorGenderDiff = new ushort[] { 521, 592, 593, 668 };
            genderDiff = new ushort[] { 3, 12, 19, 20, 25, 26, 41, 42, 44, 45, 64, 65, 84, 85, 97,
                                        111, 112, 118, 119, 123, 129, 130, 133, 154, 165, 166, 178,
                                         185, 186, 190, 194, 195, 198, 202, 203, 207, 208, 212, 214,
                                         215, 217, 221, 224, 229, 232, 255, 256, 257, 267, 269, 272,
                                         274, 275, 307, 308, 315, 316, 317, 322, 323, 332, 350, 369,
                                         396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 407, 415,
                                         417, 418, 419, 424, 443, 444, 445, 449, 450, 453, 454, 456,
                                         457, 459, 460, 461, 464, 465, 473, 521, 592, 593, 668 };
            noDiff = new ushort[] { 414, 658, 664, 665, 744 };
            alolan = new Dictionary<ushort, byte>()
            {
                {19, 1}, {20, 1}, {26, 1}, {27, 1}, {28, 1}, {37, 1}, {38, 1}, {50, 1}, {51, 1},
                {52, 1}, {53, 1}, {74, 1}, {75, 1}, {76, 1}, {88, 1}, {89, 1}, {103, 1}, {105, 1}
            };
            galarian = new Dictionary<ushort, byte>()
            {
                {52, 2}, {77, 1}, {78, 1}, {79, 1}, {80, 2}, {83, 1}, {110, 1}, {122, 1 }, {144, 1 }, {145, 1}, {146, 1},
                {199, 1}, {222, 1}, {263, 1}, {264, 1}, {554, 1}, {555, 2}, {562, 1}, {618, 1}
            };
            galarianEvo = new Dictionary<(ushort, byte), byte>()
            {
                {(80, 1), 2}, {(555, 1), 2}, {(862, 1), 0}, {(863, 2), 0},
                {(864, 1), 0}, {(865, 1), 0}, {(866, 1), 0}, {(867, 1), 0}
            };
            hisuian = new Dictionary<ushort, byte>()
            {
                {58, 1}, {59, 1}, {100, 1}, {101, 1}, {157, 1}, {211, 1 }, {215, 1}, {503, 1},
                {549, 1}, {550, 2},  {570, 1}, {571, 1}, {628, 1}, {705, 1}, {706, 1}, {713, 1},
                {724, 1}
            };
            hisuianEvo = new Dictionary<(ushort, byte), byte>()
            {
                {(902, 0), 2}, {(903, 0), 1}, {(904, 0), 1}
            };
            paldean = new Dictionary<ushort, byte>()
            {
                {128, 1}, {194, 1}
            };
            paldeanEvo = new Dictionary<(ushort, byte), byte>()
            {
                {(980, 0), 1}
            };
            languages = new string[] { "", "ja", "en", "fr", "it", "de", "", "es", "ko", "zh", "zh2" };

            dexes = LoadSortOrders();

            List<bool> checks = new List<bool>();
            checks.Add(true);
            setDexOrder((int)Dexes.HomeDex, VisibleSpecies.All, checks);

            GameInfo.Strings = GameInfo.GetStrings("en");
            PkmListAny = new List<ComboItem>(GameInfo.SpeciesDataSource);

            monInGame = new Dictionary<Tuple<GameVersion, int>, bool>();
            GameVersion[] versions = {GameVersion.RD,GameVersion.GN, GameVersion.YW,
                                      GameVersion.GD, GameVersion.SI, GameVersion.C,
                                      GameVersion.R, GameVersion.S, GameVersion.FR,  GameVersion.LG, GameVersion.E,
                                      GameVersion.CXD,
                                      GameVersion.D, GameVersion.P, GameVersion.Pt, GameVersion.HG, GameVersion.SS,
                                      GameVersion.B, GameVersion.W, GameVersion.B2, GameVersion.W2,
                                      GameVersion.X, GameVersion.Y, GameVersion.OR, GameVersion.AS,
                                      GameVersion.GO,
                                      GameVersion.SN, GameVersion.MN, GameVersion.US, GameVersion.UM,
                                      GameVersion.GP, GameVersion.GE,
                                      GameVersion.SW, GameVersion.SH,
                                      GameVersion.BD, GameVersion.SP,
                                      GameVersion.PLA,
                                      GameVersion.SL, GameVersion.VL};
            foreach (GameVersion v in versions)
            {
                SaveFile sf = SaveUtil.GetBlankSAV(v, "blank");
                var f = new FilteredGameDataSource(sf, GameInfo.Sources).Species;
                List<ComboItem> sp = new List<ComboItem>(f);

                if (v == GameVersion.SW || v == GameVersion.SH)
                {
                    List<int> gameSpecies = new List<int>();

                    gameSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Galar"]);
                    gameSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Isle of Armor"]);
                    gameSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Crown Tundra"]);
                    gameSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Foreign);


                    foreach (ComboItem ci in PkmListAny)
                    {
                        if (gameSpecies.Contains(ci.Value))
                            monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value), true);
                        else
                            monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value), false);
                    }
                }
                else
                {
                    foreach (ComboItem ci in PkmListAny)
                    {
                        if (sp.Contains(ci))
                            monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value), true);
                        else
                            monInGame.Add(new Tuple<GameVersion, int>(v, ci.Value), false);
                    }
                }
            }

            for (int i = 1; i <= numPokemon; i++)
                monInGame.Add(new Tuple<GameVersion, int>(GameVersion.HOME, i), true);

        }

        private void InitializeStrings(string spr, GameVersion gv, string trainer)
        {
            if (gv == GameVersion.HOME)
                gv = GameVersion.VL;
            GameInfo.Strings = GameInfo.GetStrings(spr);
            ver = SaveUtil.GetBlankSAV(gv, trainer);
            //PKMConverter.SetPrimaryTrainer(ver);
            GameInfo.FilteredSources = new FilteredGameDataSource(ver, GameInfo.Sources, true);

            // Update Legality Strings
            Task.Run(() =>
                {
                    var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Substring(0, 2);
                    LocalizationUtil.SetLocalization(typeof(LegalityCheckStrings), lang);
                    LocalizationUtil.SetLocalization(typeof(MessageStrings), lang);
                    RibbonStrings.ResetDictionary(GameInfo.Strings.ribbons);
                });

            // Update Legality Analysis strings
            /*LegalityAnalysis.MoveStrings = GameInfo.Strings.movelist;
            LegalityAnalysis.SpeciesStrings = GameInfo.Strings.specieslist;*/
        }

        private void InitializeBinding()
        {
            ComboBox[] cbs =
            {
                comboBoxBalls, comboBoxSpecies, comboBoxLanguage, comboBoxAbility, comboBoxNature
            };

            moveBoxes = new[] { comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4 };

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
                ribbonPic0, ribbonPic1, ribbonPic2, ribbonPic3, ribbonPic4,
                ribbonPic5, ribbonPic6, ribbonPic7, ribbonPic8, ribbonPic9,
                ribbonPic10, ribbonPic11, ribbonPic12, ribbonPic13, ribbonPic14,
                ribbonPic15, ribbonPic16, ribbonPic17, ribbonPic18, ribbonPic19
            };

            foreach (var slot in PKXBOXES)
            {
                slot.DoubleClick += (sender, e) =>
                {

                    if ((int)slot.Tag != -1)
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
            LegalMoveSource.ChangeMoveSource(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);

            comboBoxGame.Items.Clear();
            foreach (var entry in gameDict)
                comboBoxGame.Items.Add(entry.Key.ToString());

            comboBoxOrigin.Items.Clear();
            foreach (var entry in regionDict)
                comboBoxOrigin.Items.Add(entry.Key.ToString());

            comboBoxTeraType.InitializeBinding();
            var types = GameInfo.Strings.types;
            var tera = Util.GetCBList(types);
            comboBoxTeraType.DataSource = new BindingSource(tera, null);
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
                InitializeStrings("en", GameVersion.VL, "blank");
            else if (info.version == GameVersion.HOME)
                InitializeStrings(info.language, GameVersion.SV, GetTrainer(identifier));
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

        private int OriginIndex(string identifier)
        {
            if (identifier == null)
                identifier = "";

            if (!regionDict.TryGetValue(identifier, out int value))
                return 0;
            else
                return value;
        }


        private PKM MonDataToPKM(MonData data)
        {
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
                    mon = new PK8();
                    break;
            }
            mon.Species = data.Species;
            mon.Form = data.AltForm;
            if (mon.Species == 869) //Alcremie
                mon.Form = (byte)(mon.Form / 7);
            mon.CurrentLevel = (byte)data.Level;
            if (gameDict.TryGetValue(data.Game, out SaveInfo val))
                mon.Version = val.version;
            if (mon.Version == GameVersion.HOME)
            {
                switch (data.Gen)
                {
                    case 7:
                        mon.Version = GameVersion.GE;
                        break;
                    case 8:
                        mon.Version = GameVersion.SH;
                        break;
                    case 9:
                        mon.Version = GameVersion.VL;
                        break;
                    default:
                        mon.Version = GameVersion.SH;
                        break;
                }
            }
            return mon;
        }

        private void PopulateFields(MonData pk)
        {
            legal = new LegalityAnalysis(MonDataToPKM(pk), ver.Personal);
            LegalMoveSource.ReloadMoves(legal);
            foreach (ComboBox mb in moveBoxes)
            {
                mb.DataSource = new BindingSource(LegalMoveSource.Display.DataSource, null);
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
                    labelGimmick.Visible = false;
                    textBoxDynaLv.Visible = false;
                    comboBoxTeraType.Visible = false;
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
                    labelGimmick.Visible = false;
                    textBoxDynaLv.Visible = false;
                    comboBoxTeraType.Visible = false;
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
                    labelGimmick.Visible = false;
                    textBoxDynaLv.Visible = false;
                    comboBoxTeraType.Visible = false;
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
                    labelGimmick.Visible = false;
                    textBoxDynaLv.Visible = false;
                    comboBoxTeraType.Visible = false;
                    break;
                case 8:
                    labelGender.Visible = true;
                    labelBall.Visible = true;
                    comboBoxBalls.Visible = true;
                    if (pk.Game.Contains("Legends"))
                    {
                        labelAbility.Visible = false;
                        comboBoxAbility.Visible = false;
                    }
                    else
                    {
                        labelAbility.Visible = true;
                        comboBoxAbility.Visible = true;
                    }
                    labelRibbons.Visible = true;
                    labelNature.Visible = true;
                    comboBoxNature.Visible = true;
                    comboBoxPlus.Visible = true;
                    comboBoxMinus.Visible = true;
                    labelLanguage.Visible = true;
                    comboBoxLanguage.Visible = true;
                    comboBoxPkrs.Visible = true;
                    if (pk.Game.Contains("Sword") ||
                        pk.Game.Contains("Shield"))
                    {
                        labelGimmick.Visible = true;
                        labelGimmick.Text = "Dynamax:";
                        textBoxDynaLv.Visible = true;
                    }
                    else
                    {
                        labelGimmick.Visible = false;
                        textBoxDynaLv.Visible = false;
                    }
                    comboBoxTeraType.Visible = false;
                    break;
                case 9:
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
                    if (pk.Game.Contains("Scarlet") ||
                        pk.Game.Contains("Violet"))
                    {
                        comboBoxPkrs.Visible = false;
                        labelGimmick.Visible = true;
                        labelGimmick.Text = "Tera Type:";
                        comboBoxTeraType.Visible = true;
                    }
                    else
                    {
                        comboBoxPkrs.Visible = true;
                        labelGimmick.Visible = false;
                        comboBoxTeraType.Visible = false;
                    }
                    textBoxDynaLv.Visible = false;
                    break;
            }

            if (pk.Game.Contains("HOME"))
                buttonIdealTransfer.Visible = true;
            else
                buttonIdealTransfer.Visible = false;

            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();

            textBoxNickname.Text = pk.Nickname;
            comboBoxBalls.SelectedValue = pk.Ball;
            comboBoxSpecies.Text = PkmListSorted[pk.Species].Text;
            comboBoxLanguage.SelectedValue = pk.Language;
            comboBoxAbility.SelectedValue = pk.Ability;
            comboBoxNature.SelectedValue = pk.Nature;

            if (pk.Moves == null)
                pk.Moves = new List<int> { 0, 0, 0, 0 };

            while (pk.Moves.Count < 4)
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
            if (pk.Gen >= 3)
            {
                if (pk.Boon == 0 || pk.Bane == 0)
                {
                    pk.Boon = (pk.Nature / 5) + 1;
                    pk.Bane = (pk.Nature % 5) + 1;
                }

                comboBoxPlus.SelectedIndex = pk.Boon;
                comboBoxMinus.SelectedIndex = pk.Bane;

                SetStatText(comboBoxPlus.SelectedIndex, comboBoxMinus.SelectedIndex);
            }
            else
            {
                pk.Boon = 0;
                pk.Bane = 0;
            }

            monRibbons = new List<string>();

            if (pk.Ribbons != null)
            {
                foreach (string rib in pk.Ribbons)
                    monRibbons.Add(rib);
            }

            UpdateRibbons();

            if (pk.movepools != null)
            {
                monMovepools = pk.movepools;
            }
            else
            {
                monMovepools = new Dictionary<string, Movepools>();
            }

            if (pk.availableVersions == null)
            {
                pk.availableVersions = new List<string>();
            }
            foreach (string av in pk.availableVersions)
            {
                if (!monMovepools.ContainsKey(av))
                {
                    monMovepools[av] = new Movepools();
                    monMovepools[av].moves = new List<int> { 0, 0, 0, 0 };
                }
            }

            textBoxOT.Text = pk.OT;
            textBoxID.Text = pk.ID.ToString();
            comboBoxGame.Text = pk.Game;
            comboBoxOrigin.Text = pk.Origin;

            textBoxLevel.Text = pk.Level.ToString();

            pictureBoxBall.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxBall.Image = RetrieveImage("Resources/img/ball/" + pk.Ball + ".png");
            pictureBoxBall.Height = 32;
            pictureBoxBall.Width = 32;
            pictureBoxBall.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBall.Refresh();

            string spForm = pk.Species.ToString();
            if (pk.AltForm > 0 && !noDiff.Contains(pk.Species) && pk.Species != 869)
                spForm += "-" + pk.AltForm.ToString();
            else if (pk.Species == 869 && pk.AltForm >= 7)
                spForm += "-" + (pk.AltForm / 7).ToString();
            else if (majorGenderDiff.Contains(pk.Species))
            {
                if (pk.Gender == 0)
                    spForm += "m";
                else if (pk.Gender == 1)
                    spForm += "f";
            }
            if (pk.Species == 25 && pk.Gen == 6)
                spForm += "c";
            pictureBoxIcon.Image = RetrieveImage("Resources/img/icons/" + spForm + ".png");
            if (pictureBoxIcon.Image != null)
            {
                if (pictureBoxIcon.Image.Height > 56)
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            if (pk.Species == 869 && pk.AltForm < 7)
                spForm += "-0";
            if (pk.Species == 869)
                spForm += "-" + (pk.AltForm % 7).ToString();
            if (genderDiff.Contains(pk.Species) && !majorGenderDiff.Contains(pk.Species))
            {
                if ((pk.AltForm == 0 || (pk.AltForm == 1 && pk.Species == 215)) && pk.Gen >= 4 && !(pk.Species == 133 && gameDict[pk.Game].version < GameVersion.GP))
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
            if (si != null)
                pictureBoxGameSprite.Image = GetSprite(spForm, si.version, pk.Shiny);
            else
                pictureBoxGameSprite.Image = GetSprite(spForm, GameVersion.VL, pk.Shiny);
            if (pictureBoxGameSprite.Height > 180 || pictureBoxGameSprite.Width > 180)
            {
                double ratio = (double)pictureBoxGameSprite.Height / (double)pictureBoxGameSprite.Width;
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
                pictureBoxPkrs.Visible = comboBoxPkrs.Visible;
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

            if (pk.gMax && (pk.Game.Contains("HOME") || pk.Game.Contains("Sword") || pk.Game.Contains("Shield")))
                pictureBoxGMax.Image = RetrieveImage("Resources/img/gMax.png");
            else
                pictureBoxGMax.Image = null;
            textBoxDynaLv.Text = pk.dynaLevel.ToString();
            if (pk.alpha && (pk.Game.Contains("HOME") || pk.Game.Contains("Arceus")))
                pictureBoxAlpha.Image = RetrieveImage("Resources/img/alpha.png");
            else
                pictureBoxAlpha.Image = null;
            var types = GameInfo.Strings.types;
            comboBoxTeraType.Text = types[pk.teraType];
            if (pk.Species != 869)
            {
                var ds = FormConverter.GetFormList((ushort)pk.Species, GameInfo.Strings.types, GameInfo.Strings.forms, genders, (EntityContext)pk.Gen);
                comboBoxForm.DataSource = ds;
            }
            else
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

            if (pk.AltForm < comboBoxForm.Items.Count)
                comboBoxForm.SelectedIndex = pk.AltForm;
            else if (galarianEvo.ContainsKey((pk.Species, pk.AltForm)))
            {
                pk.AltForm = galarianEvo[(pk.Species, pk.AltForm)];
                comboBoxForm.SelectedIndex = pk.AltForm;
            }
            else if (hisuianEvo.ContainsKey((pk.Species, pk.AltForm)))
            {
                pk.AltForm = hisuianEvo[(pk.Species, pk.AltForm)];
                comboBoxForm.SelectedIndex = pk.AltForm;
            }
            else if (paldeanEvo.ContainsKey((pk.Species, pk.AltForm)))
            {
                pk.AltForm = paldeanEvo[(pk.Species, pk.AltForm)];
                comboBoxForm.SelectedIndex = pk.AltForm;
            }
            else
                comboBoxForm.SelectedIndex = 0;



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
                if (!File.Exists(spCry))
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
                case GameVersion.SI:
                    game = "si";
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
                case GameVersion.BD:
                case GameVersion.SP:
                    game = "swsh";
                    ext = ".gif";
                    break;
                case GameVersion.PLA:
                    game = "pla";
                    ext = ".png";
                    break;
                case GameVersion.SL:
                case GameVersion.VL:
                    game = "sv";
                    ext = ".png";
                    break;
                case GameVersion.HOME:
                    game = "home";
                    ext = ".png";
                    break;

            }
            if (!shiny)
                return RetrieveImage("Resources/img/" + game + "/" + species + ext);
            else
                return RetrieveImage("Resources/img/" + game + "/rare/" + species + ext);
        }

        private void UpdateRibbons()
        {
            foreach (PictureBox pb in ribbonBoxes)
                pb.Image = null;

            if (monRibbons.Count > 0)
            {
                int r = 0;
                foreach (string rib in monRibbons)
                {
                    ribbonBoxes[r].Image = RetrieveImage("Resources/img/ribbons/" + rib + ".png");
                    r++;
                }
            }
        }

        private void ValidateMovePaint(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var item = (ComboItem)((ComboBox)sender).Items[e.Index];
            var valid = LegalMoveSource.Info.CanLearn((ushort)item.Value);

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

        private static List<MonSortOrder> LoadSortOrders()
        {
            if (!File.Exists(Settings.Default.mons + "/dexes.json"))
                File.Create(Settings.Default.mons + "/dexes.json").Dispose();

            using (StreamReader r = new StreamReader(Settings.Default.mons + "/dexes.json"))
            {
                string json = r.ReadToEnd();
                List<MonSortOrder> orders = JsonConvert.DeserializeObject<List<MonSortOrder>>(json);
                return orders;
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

            L_Count.Text = string.Format("Mon Count: {0}", res.Count());
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
                if (mon.AltForm > 0 && !noDiff.Contains(mon.Species) && mon.Species != 869)
                    spForm += "-" + mon.AltForm.ToString();
                else if (mon.Species == 869 && mon.AltForm >= 7)
                    spForm += "-" + (mon.AltForm / 7).ToString();
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
                if (PKXBOXES[i].Image != null)
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
                PKXBOXES[i].BackgroundImage = GetSlotImg((int)PKXBOXES[i].Tag, false);
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
            PkmData = PkmData.OrderBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => OriginIndex(mon.Origin))
                             .ThenBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void GameOriginSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => OriginIndex(mon.Origin))
                             .ThenBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void OriginSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => OriginIndex(mon.Origin))
                             .ThenBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void GenSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => mon.Gen)
                             .ThenBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }


        public void GameLevelSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        public void GameSpeciesSort(int index)
        {
            PkmData = PkmData.OrderBy(mon => GameIndex(mon.Game))
                             .ThenBy(mon => monOrder[mon.Species])
                             .ThenBy(mon => visibleAltForm(mon))
                             .ThenBy(mon => mon.Shiny)
                             .ThenBy(mon => mon.Level)
                             .ThenBy(mon => mon.Nickname)
                             .ToList<MonData>();

            FillPKXBoxes(index);
        }

        private void ButtonGameLevelSort_Click(object sender, EventArgs e)
        {
            GameLevelSort((int)bpkx1.Tag / RES_MIN);
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
            ButtonGameLevelSort_Click(sender, e);
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
                results.addEntry(String.Format("{0}: {1}", q.Key, q.Count));
            results.Show();
        }

        private void ButtonOriginTally_Click(object sender, EventArgs e)
        {
            var query = PkmData.GroupBy(
                mon => mon.Origin,
                mon => mon.Nickname,
                (origin, name) => new
                {
                    Key = origin,
                    Count = name.Count()
                });
            var results = new FormGameTally();
            foreach (var q in query)
                results.addEntry(String.Format("{0}: {1}", q.Key, q.Count));
            results.Show();
        }

        private void ButtonSpeciesSort_Click(object sender, EventArgs e)
        {
            SpeciesGameSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void ButtonOriginSort_Click(object sender, EventArgs e)
        {
            OriginSpeciesSort((int)bpkx1.Tag / RES_MIN);
            OpenPKM(PkmData[slotSelected]);
        }

        private void buttonGameOriginSort_Click(object sender, EventArgs e)
        {
            GameOriginSort((int)bpkx1.Tag / RES_MIN);
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
                });
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
            if (rnd != -1)
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
                if (GetGameMons(entry.Value.version, index))
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
            foreach (var entry in d)
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
                    if (i != 0)
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
            if (version == GameVersion.HOME)
                return monInGame[new Tuple<GameVersion, int>(GameVersion.HOME, species)];
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

            if (boon != 0 && bane != 0)
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

            string json = JsonConvert.SerializeObject(FullPkmData, Formatting.Indented);

            //write string to file
            File.WriteAllText(Settings.Default.mons + "/mons.json", json);

            json = JsonConvert.SerializeObject(dexes, Formatting.Indented);
            File.WriteAllText(Settings.Default.mons + "/dexes.json", json);
        }

        private void UpdateFullData()
        {
            for (int i = 0; i < PkmData.Count(); i++)
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
            if (comboBoxSpecies.SelectedValue != null)
                mon.Species = (ushort)((int)comboBoxSpecies.SelectedValue);
            if (comboBoxForm.Visible == true)
                mon.AltForm = (byte)comboBoxForm.SelectedIndex;
            if (comboBoxAbility.SelectedValue != null)
                mon.Ability = (int)comboBoxAbility.SelectedValue;
            mon.Nature = (int)comboBoxNature.SelectedValue;
            mon.Boon = comboBoxPlus.SelectedIndex;
            mon.Bane = comboBoxMinus.SelectedIndex;
            mon.Moves = new List<int> { (int?)comboBoxMove1.SelectedValue ?? 0, (int?)comboBoxMove2.SelectedValue ?? 0,
                                        (int?)comboBoxMove3.SelectedValue ?? 0, (int?)comboBoxMove4.SelectedValue ?? 0 };
            mon.Game = comboBoxGame.Text;
            mon.OT = textBoxOT.Text;
            mon.ID = int.Parse(textBoxID.Text);
            mon.Origin = comboBoxOrigin.Text;
            mon.Shiny = (bool)BTN_Shinytize.Tag;
            mon.HP = int.Parse(textBoxHP.Text);
            mon.ATK = int.Parse(textBoxAttack.Text);
            mon.DEF = int.Parse(textBoxDefense.Text);
            mon.SPA = int.Parse(textBoxSpAtk.Text);
            mon.SPD = int.Parse(textBoxSpDef.Text);
            mon.SPE = int.Parse(textBoxSpeed.Text);
            mon.dynaLevel = int.Parse(textBoxDynaLv.Text);
            mon.gMax = (pictureBoxGMax.Image != null);
            mon.alpha = (pictureBoxAlpha.Image != null);
            if (comboBoxTeraType.SelectedValue != null)
                mon.teraType = (byte)((int)comboBoxTeraType.SelectedValue);
            if (comboBoxBalls.SelectedValue != null)
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

            mon.Ribbons = monRibbons.ToArray();
            if (mon.availableVersions == null)
            {
                mon.availableVersions = new List<string>();
            }
            SaveInfo si = gameDict[mon.Game];
            GameVersion gv = si.version;

            if (gv != GameVersion.HOME)
            {
                mon.Gen = si.getGen();
            }
            else
            {
                byte temp = 0;
                foreach (string s in mon.availableVersions)
                {
                    switch (s)
                    {
                        case "LGPE":
                            if (temp < 7)
                                temp = 7;
                            break;
                        case "SWSH":
                        case "BDSP":
                        case "PLA":
                            if (temp < 8)
                                temp = 8;
                            break;
                        case "SV":
                            if (temp < 9)
                                temp = 9;
                            break;
                    }
                }
                if (temp == 0)
                    temp = 8;
                mon.Gen = temp;
            }
            switch (mon.Gen)
            {
                case 9:
                case 8:
                    mon.availableVersions.Remove("LGPE");
                    goto case 7;
                case 7:
                default:
                    break;
            }

            mon.movepools = monMovepools;
            if (si.dataGroup != string.Empty)
            {
                if (!mon.availableVersions.Contains(si.dataGroup))
                {
                    mon.availableVersions.Add(si.dataGroup);
                }
                if (mon.movepools == null)
                {
                    mon.movepools = new Dictionary<string, Movepools>();
                }
                if (!mon.movepools.ContainsKey(si.dataGroup))
                {
                    mon.movepools[si.dataGroup] = new Movepools();
                }
                mon.movepools[si.dataGroup].moves = mon.Moves;
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
            /*using (var fbd = new FolderBrowserDialog())
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
                        pk.SetStats(pk.GetStats(pk.PersonalInfo));

                        MonData pd = new MonData
                        {
                            Nickname = pk.Nickname,
                            Species = pk.Species,
                            Level = pk.CurrentLevel,
                            Gender = pk.Gender,
                            Moves = new List<int> { pk.Move1, pk.Move2, pk.Move3, pk.Move4 },
                            Game = GetGame(file),
                            AltForm = pk.Form,
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
                            Gen = GetGen(file),
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
            }*/
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
            if (int.TryParse(textBoxLevel.Text, out int newLev))
            {
                MonData mon = PkmData[slotSelected];
                if (mon.Level != 0)
                {
                    if (mon.Game.Contains("Legends"))
                    {
                        textBoxHP.Text = ((int)(((mon.HP - mon.Level) * (1 + .01 * newLev) / (1 + .01 * mon.Level)) + newLev)).ToString();
                        textBoxAttack.Text = ((int)(mon.ATK * (1 + .02 * newLev) / (1 + .02 * mon.Level))).ToString();
                        textBoxDefense.Text = ((int)(mon.DEF * (1 + .02 * newLev) / (1 + .02 * mon.Level))).ToString();
                        textBoxSpAtk.Text = ((int)(mon.SPA * (1 + .02 * newLev) / (1 + .02 * mon.Level))).ToString();
                        textBoxSpDef.Text = ((int)(mon.SPD * (1 + .02 * newLev) / (1 + .02 * mon.Level))).ToString();
                        textBoxSpeed.Text = ((int)(mon.SPE * (1 + .02 * newLev) / (1 + .02 * mon.Level))).ToString();
                    }
                    else
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
        }

        private void LabelGender_Click(object sender, EventArgs e)
        {
            labelGender.Tag = ((int)labelGender.Tag + 1) % 3;
            labelGender.Text = genders[(int)labelGender.Tag];
        }

        private void GamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGames games = new FormGames();
            games.FormClosed += new FormClosedEventHandler(delegate (object send, FormClosedEventArgs a)
            {
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
            if (PkmData[slotSelected].Language == 0)
                PkmData[slotSelected].Language = Array.IndexOf(languages, si.language);
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
                if (filterGames.Contains(data.Game) &&
                    filterOrigins.Contains(data.Origin) &&
                    filterSpecies.Contains(data.Species) &&
                    data.Level >= filterLevels.Item1 &&
                    data.Level <= filterLevels.Item2)
                {
                    mons.Add(data);
                    PKMIndices.Add(full.IndexOf(data));
                }
            }

            return mons;
        }

        private List<int> getMovepools(MonData mon)
        {
            List<int> moves = new List<int>();

            moves.AddRange(mon.Moves);

            SaveInfo si = gameDict[mon.Game];

            if (si.version == GameVersion.HOME)
            {
                if (mon.movepools == null)
                {
                    mon.movepools = new Dictionary<string, Movepools>();
                }
                foreach (string av in mon.availableVersions)
                {
                    if (!mon.movepools.ContainsKey(av))
                    {
                        mon.movepools[av] = new Movepools();
                        monMovepools[av].moves = new List<int> { 0, 0, 0, 0 };
                    }
                    GameVersion gv = GameVersion.Any;
                    switch (av)
                    {
                        case "LGPE":
                            gv = GameVersion.GE;
                            break;
                        case "SWSH":
                            gv = GameVersion.SH;
                            break;
                        case "BDSP":
                            gv = GameVersion.BD;
                            break;
                        case "PLA":
                            gv = GameVersion.PLA;
                            break;
                        case "SV":
                            gv = GameVersion.VL;
                            break;
                    }

                    if (gv == GameVersion.Any)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            ushort m = (ushort)mon.movepools[av].moves[i];
                            if (m != 0 && !moves.Contains(m))
                            {
                                moves.Add(m);
                            }
                        }
                    }
                    else
                    {
                        SaveFile sf = SaveUtil.GetBlankSAV(gv, "blank");

                        PKM pkmn = new PK8();

                        switch (gv)
                        {
                            case GameVersion.GE:
                                pkmn = new PB7();
                                break;
                            case GameVersion.SH:
                                pkmn = new PK8();
                                break;
                            case GameVersion.BD:
                                pkmn = new PB8();
                                break;
                            case GameVersion.PLA:
                                pkmn = new PA8();
                                break;
                            case GameVersion.VL:
                                pkmn = new PK9();
                                break;
                        }

                        pkmn.Species = mon.Species;
                        pkmn.Form = mon.AltForm;
                        pkmn.CurrentLevel = (byte)mon.Level;
                        pkmn.Version = gv;

                        legal = new LegalityAnalysis(pkmn, sf.Personal);
                        LegalMoveSource.ReloadMoves(legal);

                        var ls = GameData.GetLearnSource(gv);
                        var learn = ls.GetLearnset(mon.Species, mon.AltForm);
                        var mv = learn.GetAllMoves();

                        for (int i = 0; i < 4; i++)
                        {
                            ushort m = (ushort)mon.movepools[av].moves[i];
                            if (m != 0 && !moves.Contains(m))
                            {
                                moves.Add(m);
                            }
                        }

                        foreach (var m in mv)
                        {
                            if (learn.GetLevelLearnMove(m) <= mon.Level && !moves.Contains(m))
                            {
                                moves.Add(m);
                            }
                        }

                        if (gv == GameVersion.VL)
                        {
                            LearnSource9SV lsv = new LearnSource9SV();
                            var reminder = lsv.GetReminderMoves(mon.Species, mon.AltForm);
                            foreach (var r in reminder)
                            {
                                if (!moves.Contains(r))
                                {
                                    moves.Add(r);
                                }
                            }
                        }
                    }
                }
            }
            else if (si.dataGroup != string.Empty)
            {
                GameVersion gv = GameVersion.Any;
                switch (si.dataGroup)
                {
                    case "LGPE":
                        gv = GameVersion.GE;
                        break;
                    case "SWSH":
                        gv = GameVersion.SH;
                        break;
                    case "BDSP":
                        gv = GameVersion.BD;
                        break;
                    case "PLA":
                        gv = GameVersion.PLA;
                        break;
                    case "SV":
                        gv = GameVersion.VL;
                        break;
                }
                SaveFile sf = SaveUtil.GetBlankSAV(gv, "blank");

                PKM pkmn = new PK8();

                switch (gv)
                {
                    case GameVersion.GE:
                        pkmn = new PB7();
                        break;
                    case GameVersion.SH:
                        pkmn = new PK8();
                        break;
                    case GameVersion.BD:
                        pkmn = new PB8();
                        break;
                    case GameVersion.PLA:
                        pkmn = new PA8();
                        break;
                    case GameVersion.VL:
                        pkmn = new PK9();
                        break;
                }

                pkmn.Species = mon.Species;
                pkmn.Form = mon.AltForm;
                pkmn.CurrentLevel = (byte)mon.Level;
                pkmn.Version = gv;

                legal = new LegalityAnalysis(pkmn, sf.Personal);
                LegalMoveSource.ReloadMoves(legal);

                var ls = GameData.GetLearnSource(gv);
                var learn = ls.GetLearnset(mon.Species, mon.AltForm);
                var mv = learn.GetAllMoves();

                for (int i = 0; i < 4; i++)
                {
                    ushort m = (ushort)mon.movepools[si.dataGroup].moves[i];
                    if (m != 0 && !moves.Contains(m))
                    {
                        moves.Add(m);
                    }
                }

                if (mon.movepools[si.dataGroup].special != null)
                {
                    foreach (ushort s in mon.movepools[si.dataGroup].special)
                    {
                        if (s != 0 && !moves.Contains(s) && LegalMoveSource.Info.CanLearn(s))
                        {
                            moves.Add(s);
                        }
                    }
                }

                foreach (var m in mv)
                {
                    if (learn.GetLevelLearnMove(m) <= mon.Level && !moves.Contains(m))
                    {
                        moves.Add(m);
                    }
                }

                if (gv == GameVersion.VL)
                {
                    LearnSource9SV lsv = new LearnSource9SV();
                    var reminder = lsv.GetReminderMoves(mon.Species, mon.AltForm);
                    foreach (var r in reminder)
                    {
                        if (!moves.Contains(r))
                        {
                            moves.Add(r);
                        }
                    }
                }
            }

            return moves;
        }

        private void GameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFilters form = new FormFilters
            {
                filters = filterGames
            };
            form.LoadFilterList(gameList);
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    form.UpdateFilters();
                    filterGames = form.filters;
                    LoadDatabase();
                });
            form.Show();
        }

        private void originToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> originList = new List<string>();
            FormFilters form = new FormFilters
            {
                filters = filterOrigins
            };
            foreach (var item in comboBoxOrigin.Items)
            {
                if (item != null)
                    originList.Add(item.ToString());
            }
            form.LoadFilterList(originList);
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    form.UpdateFilters();
                    filterOrigins = form.filters;
                    LoadDatabase();
                });
            form.Show();
        }

        private void SpeciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted.RemoveAt(0);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();

            List<string> filterSpeciesNames = new List<string>();
            List<string> allSpeciesNames = new List<string>();

            foreach (ComboItem ci in PkmListSorted)
            {
                allSpeciesNames.Add(ci.Text);
                if (filterSpecies.Contains(ci.Value))
                    filterSpeciesNames.Add(ci.Text);
            }

            FormFilters form = new FormFilters
            {
                filters = filterSpeciesNames
            };
            form.LoadFilterList(allSpeciesNames);
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    form.UpdateFilters();
                    filterSpeciesNames = form.filters;
                    filterSpecies = new List<int>();
                    foreach (ComboItem ci in PkmListAny)
                    {
                        if (filterSpeciesNames.Contains(ci.Text))
                            filterSpecies.Add(ci.Value);
                    }
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
            int i = 1;
            foreach (var q in query)
            {
                while (i < q.Key)
                {
                    results.addEntry(String.Format("{0}: 0", i));
                    i++;
                }

                results.addEntry(String.Format("{0}: {1}", q.Key, q.Count));
                i++;
            }

            results.Show();
        }

        private int visibleAltForm(MonData mon)
        {
            int formVal = mon.AltForm;

            if (genderDiff.Contains(mon.Species))
            {
                if (mon.Species == 215 && mon.AltForm == 1)
                {
                    formVal = mon.AltForm * 2 + mon.Gender;
                }
                else
                {
                    formVal = mon.AltForm + (mon.AltForm > 0 ? 1 : mon.Gender);
                }
            }
            else if (noDiff.Contains(mon.Species))
                formVal = -1;

            return formVal;
        }

        private void ButtonMonInfo_Click(object sender, EventArgs e)
        {
            FormSpeciesInfo form = new FormSpeciesInfo();

            ushort sp = (ushort)((int)comboBoxSpecies.SelectedValue);
            int af = comboBoxForm.SelectedIndex;
            int gd = -1;
            if (genderDiff.Contains(sp) && (af == 0 || (af == 1 && sp == 215)))
                gd = (int)labelGender.Tag;
            if (noDiff.Contains(sp))
                af = -1;
            form.spImage = pictureBoxIcon.Image;
            form.spFormName = comboBoxSpecies.Text;
            if (comboBoxForm.Visible == true && af != -1)
            {
                form.spFormName += " - " + comboBoxForm.Text;
            }
            switch (gd)
            {
                case 0:
                    form.spFormName += " - M";
                    break;
                case 1:
                    form.spFormName += " - F";
                    break;
            }

            List<int> balls = new List<int>();
            List<int> abilities = new List<int>();
            List<int> languages = new List<int>();
            List<string> origins = new List<string>();
            List<int> moves = new List<int>();
            List<string> names = new List<string>();
            List<int> levels = new List<int>();

            foreach (var mon in PkmData)
            {
                if (mon.Species == sp && (mon.AltForm == af || af == -1) && (mon.Gender == gd || gd == -1))
                {
                    abilities.Add(mon.Ability);
                    balls.Add(mon.Ball);
                    languages.Add(mon.Language);
                    origins.Add(mon.Origin);
                    moves.AddRange(getMovepools(mon));
                    names.Add(mon.Nickname);
                    levels.Add(mon.Level);

                    if (mon.Shiny)
                        form.hasShiny = true;
                }
            }

            abilities = abilities.Distinct().ToList();
            balls = balls.Distinct().ToList();
            languages = languages.Distinct().ToList();
            origins = origins.Distinct().ToList();
            moves = moves.Distinct().ToList();

            balls.Sort();
            names.Sort();
            languages.Sort();
            origins = origins.OrderBy(o => regionDict[o]).ToList();
            levels.Sort();

            foreach (int b in balls)
            {
                if (b != 0)
                    form.ballList.Add(RetrieveImage("Resources/img/ball/" + b + ".png"));
            }

            foreach (string n in names)
                form.nameList.Add(n);

            foreach (int v in levels)
                form.levelList.Add(v);

            foreach (string o in origins)
                form.originList.Add(o);

            var abilityNames = new List<ComboItem>(GameInfo.AbilityDataSource);

            foreach (int a in abilities)
            {
                if (a != 0)
                    form.abilityList.Add(abilityNames.Find(p => p.Value == a).Text);
            }

            var languageNames = new List<ComboItem>(GameInfo.LanguageDataSource(8));

            foreach (int l in languages)
            {
                if (l != 0)
                    form.languageList.Add(languageNames.Find(p => p.Value == l).Text);
            }

            var moveNames = new List<ComboItem>(GameInfo.MoveDataSource);

            foreach (int m in moves)
            {
                if (m != 0)
                    form.moveList.Add(moveNames.Find(p => p.Value == m).Text);
            }

            form.abilityList.Sort();
            form.moveList.Sort();

            form.LoadData();

            form.Show();
        }

        private void buttonLineInfo_Click(object sender, EventArgs e)
        {
            FormSpeciesInfo form = new FormSpeciesInfo();

            monFamily = new MonFamily();
            ushort[] family = monFamily.GetFamily((ushort)(int)comboBoxSpecies.SelectedValue);

            if (family[0] == 592)
                form.spImage = RetrieveImage("Resources/img/icons/592f.png");
            else
                form.spImage = RetrieveImage("Resources/img/icons/" + family[0] + ".png");
            form.spFormName = "";

            List<int> balls = new List<int>();
            List<int> abilities = new List<int>();
            List<int> languages = new List<int>();
            List<string> origins = new List<string>();
            List<int> moves = new List<int>();
            List<string> names = new List<string>();
            List<int> levels = new List<int>();

            foreach (var mon in PkmData)
            {
                if (family.Contains(mon.Species))
                {
                    abilities.Add(mon.Ability);
                    balls.Add(mon.Ball);
                    languages.Add(mon.Language);
                    origins.Add(mon.Origin);
                    foreach (int m in getMovepools(mon))
                        moves.Add(m);
                    names.Add(mon.Nickname);
                    levels.Add(mon.Level);

                    if (mon.Shiny)
                        form.hasShiny = true;
                }
            }

            abilities = abilities.Distinct().ToList();
            balls = balls.Distinct().ToList();
            languages = languages.Distinct().ToList();
            origins = origins.Distinct().ToList();
            moves = moves.Distinct().ToList();

            balls.Sort();
            names.Sort();
            languages.Sort();
            origins = origins.OrderBy(o => regionDict[o]).ToList();
            levels.Sort();

            foreach (int b in balls)
            {
                if (b != 0)
                    form.ballList.Add(RetrieveImage("Resources/img/ball/" + b + ".png"));
            }

            foreach (string n in names)
                form.nameList.Add(n);

            foreach (string o in origins)
                form.originList.Add(o);

            foreach (int v in levels)
                form.levelList.Add(v);

            var abilityNames = new List<ComboItem>(GameInfo.AbilityDataSource);

            foreach (int a in abilities)
            {
                if (a != 0)
                    form.abilityList.Add(abilityNames.Find(p => p.Value == a).Text);
            }

            var languageNames = new List<ComboItem>(GameInfo.LanguageDataSource(8));

            foreach (int l in languages)
            {
                if (l != 0)
                    form.languageList.Add(languageNames.Find(p => p.Value == l).Text);
            }

            var moveNames = new List<ComboItem>(GameInfo.MoveDataSource);

            foreach (int m in moves)
            {
                if (m != 0)
                    form.moveList.Add(moveNames.Find(p => p.Value == m).Text);
            }

            form.abilityList.Sort();
            form.moveList.Sort();

            form.LoadData();

            form.Show();
        }

        private void comboBoxNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNature.Visible)
            {
                int nat = (int)comboBoxNature.SelectedValue;
                comboBoxPlus.SelectedIndex = (nat / 5) + 1;
                comboBoxMinus.SelectedIndex = (nat % 5) + 1;

                SetStatText(comboBoxPlus.SelectedIndex, comboBoxMinus.SelectedIndex);
            }
            else
            {
                SetStatText(0, 0);
            }
        }

        private void buttonDupCheck_Click(object sender, EventArgs e)
        {
            OriginSpeciesSort((int)bpkx1.Tag / RES_MIN);
            MonData previous = null;
            foreach (MonData md in PkmData)
            {
                if (previous == null)
                {
                    previous = md;
                    continue;
                }

                if (md.Nickname == previous.Nickname &&
                   md.Level == previous.Level &&
                   md.Species == previous.Species &&
                   md.AltForm == previous.AltForm &&
                   md.HP == previous.HP &&
                   md.SPE == previous.SPE &&
                   md.OT == previous.OT)
                {
                    OpenPKM(md);
                    return;
                }
                else
                {
                    previous = md;
                }
            }

            OpenPKM(PkmData[slotSelected]);

        }

        private void buttonPkrsCount_Click(object sender, EventArgs e)
        {
            var results = new FormGameTally();
            List<MonData> total = PkmData;
            List<MonData> infected = total.Where(pk => pk.PKRS_Infected == true).ToList();
            List<MonData> cured = total.Where(pk => pk.PKRS_Cured == true).ToList();
            results.addEntry(String.Format("Total: {0}% Infected {1}% Cured",
                    Math.Round((double)infected.Count / total.Count * 100, 2),
                    Math.Round((double)cured.Count / total.Count * 100, 2)));
            foreach (var entry in gameDict)
            {
                if (entry.Value.getGen() > 1 && entry.Value.version != GameVersion.GO &&
                   entry.Value.version != GameVersion.GP && entry.Value.version != GameVersion.GE)
                {
                    total = PkmData.Where(pk => pk.Game == entry.Key).ToList();
                    infected = total.Where(pk => pk.PKRS_Infected == true).ToList();
                    cured = total.Where(pk => pk.PKRS_Cured == true).ToList();
                    results.addEntry(String.Format("{0}: {1}% Infected {2}% Cured", entry.Key,
                            Math.Round((double)infected.Count / total.Count * 100, 2),
                            Math.Round((double)cured.Count / total.Count * 100, 2)));
                }
            }
            results.Show();
        }

        private void labelDynamax_Click(object sender, EventArgs e)
        {
            if (pictureBoxGMax.Image == null)
                pictureBoxGMax.Image = RetrieveImage("Resources/img/gMax.png");
            else
                pictureBoxGMax.Image = null;
        }

        private void labelRibbons_Click(object sender, EventArgs e)
        {
            FormRibbons form = new FormRibbons
            {
                ribbons = monRibbons
            };
            form.LoadRibbons();
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    monRibbons = form.ribbons;
                    UpdateRibbons();
                });
            form.Show();
        }

        private void labelMoves_Click(object sender, EventArgs e)
        {
            FormMovepools form = new FormMovepools
            {
                movepools = monMovepools,
                mon = PkmData[slotSelected]
            };
            form.SetupValues();
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    monMovepools = form.movepools;
                });
            form.Show();
        }

        private void buttonAssignOrigin_Click(object sender, EventArgs e)
        {
            /*FormAssignOrigin form = new FormAssignOrigin
            {
                regionVals = regionDict
            };
            form.LoadOrigins();
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    if (form.set)
                    {
                        foreach (MonData mon in PkmData)
                        {
                            if (mon.ID == form.setID && mon.OT == form.setOT)
                            {
                                mon.Origin = form.setOrigin;
                            }
                        }
                        OpenPKM(PkmData[slotSelected]);
                    }
                });

            form.Show();*/
            List<string> ord = new List<string> { "Bank", "LGPE", "SWSH", "BDSP", "PLA", "SV" };
            for (int i = 0; i < PkmData.Count; i++)
            {
                if (PkmData[i].movepools != null)
                {
                    PkmData[i].movepools = PkmData[i].movepools.OrderByDescending(i => ord.IndexOf(i.Key)).ToDictionary(d => d.Key, d => d.Value);
                    if (PkmData[i].movepools.ContainsKey("SWSH") && !PkmData[i].availableVersions.Contains("SWSH"))
                        PkmData[i].availableVersions.Add("SWSH");
                    if (PkmData[i].movepools.ContainsKey("SWSH") && PkmData[i].movepools.ContainsKey("Bank"))
                        PkmData[i].movepools["Bank"] = PkmData[i].movepools["SWSH"];
                    foreach (KeyValuePair<string, Movepools> m in PkmData[i].movepools)
                    {
                        if (m.Value.special != null)
                            m.Value.special.Sort();
                    }
                }
                if (PkmData[i].availableVersions != null)
                {
                    PkmData[i].availableVersions = PkmData[i].availableVersions.OrderBy(i => ord.IndexOf(i)).Distinct().ToList();
                }
            }
        }

        private void homeSwShToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterGames = new List<string>();
            foreach (var item in comboBoxGame.Items)
            {
                if (item.ToString().Contains("HOME"))
                    filterGames.Add(item.ToString());
            }

            filterSpecies = new List<int>();

            filterSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Galar"]);
            filterSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Isle of Armor"]);
            filterSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Dexes["Crown Tundra"]);
            filterSpecies.AddRange(dexes[(int)Dexes.SwordShieldDex].Foreign);
            LoadDatabase();
        }

        private void buttonSortOrder_Click(object sender, EventArgs e)
        {
            FormDexOrder dexOrder = new FormDexOrder(dexes, lastDex, lastVis);
            dexOrder.FormClosed += new FormClosedEventHandler(delegate (object send, FormClosedEventArgs a)
            {
                setDexOrder(dexOrder.dex, (VisibleSpecies)dexOrder.vis, dexOrder.getChecks());
                LoadDatabase();
            });
            dexOrder.Show();
        }

        private void setDexOrder(int dex, VisibleSpecies vis, List<bool> checks)
        {
            monOrder = new Dictionary<int, int>();
            filterSpecies = new List<int>();
            lastDex = dex;
            lastVis = (int)vis;

            int j = 1;

            List<int> foreign = new List<int>();
            int d = 0;

            foreach (var entry in dexes[dex].Dexes)
            {
                if (checks[d])
                {
                    foreach (int val in entry.Value)
                    {
                        if (!monOrder.ContainsKey(val))
                        {
                            monOrder.Add(val, j);
                            filterSpecies.Add(val);
                            j++;
                        }
                    }
                }
                else
                {
                    foreign.AddRange(entry.Value);
                }
                d++;
            }

            foreign.Sort();
            foreign.AddRange(dexes[dex].Foreign);

            foreach (int val in foreign)
            {
                if (!monOrder.ContainsKey(val))
                {
                    monOrder.Add(val, j);
                    if (vis != VisibleSpecies.Native)
                    {
                        filterSpecies.Add(val);
                    }
                    j++;
                }
            }

            for (int p = 1; p <= numPokemon; p++)
            {
                if (!monOrder.ContainsKey(p))
                {
                    monOrder.Add(p, j);
                    if (vis == VisibleSpecies.All)
                    {
                        filterSpecies.Add(p);
                    }
                    j++;
                }
            }
        }

        private void buttonEditDexes_Click(object sender, EventArgs e)
        {
            FormEditDexes editDexes = new FormEditDexes(dexes);
            editDexes.FormClosed += new FormClosedEventHandler(delegate (object send, FormClosedEventArgs a)
            {
                dexes = editDexes.UpdateDexes();
            });
            editDexes.Show();
        }

        private void levelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLevelRange form = new FormLevelRange
            {
                setLevels = filterLevels
            };
            form.loadLevels();
            form.FormClosing += new FormClosingEventHandler(
                delegate (object send, FormClosingEventArgs a)
                {
                    filterLevels = form.setLevels;
                    LoadDatabase();
                });
            form.Show();
        }

        private void buttonPickTransfers_Click(object sender, EventArgs e)
        {
            List<MonData> transferMons = new List<MonData>();
            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();

            for (int i = 0; i < PkmData.Count; i++)
            {
                if (canTransfer(PkmData, i))
                {
                    transferMons.Add(PkmData[i]);
                }
            }

            int entries = int.Parse(Interaction.InputBox(String.Format("How many Transfers?\n({0})", homeTransferCounts()), "Transfers"));
            int[] rnd = RandomNumberGenerator.GetRandomInts(0, transferMons.Count - 1, entries);

            Array.Sort(rnd);

            if (rnd[0] != -1)
            {
                var results = new FormGameTally();

                foreach (int r in rnd)
                {
                    string spForm = PkmListSorted[transferMons[r].Species].Text;
                    if (transferMons[r].AltForm > 0 && !noDiff.Contains(transferMons[r].Species) && transferMons[r].Species != 869)
                        spForm += "-" + transferMons[r].AltForm.ToString();
                    else if (transferMons[r].Species == 869 && transferMons[r].AltForm >= 7)
                        spForm += "-" + (transferMons[r].AltForm / 7).ToString();

                    if (transferMons[r].Game.Contains("HOME"))
                    {
                        results.addEntry(String.Format("{0} ({1}, {2}) -> [{3}]", transferMons[r].Nickname, spForm, transferMons[r].OT, idealTransfer(transferMons, r)));
                    }
                    else
                    {
                        results.addEntry(String.Format("{0} ({1}, {2}) [{3}]", transferMons[r].Nickname, spForm, transferMons[r].OT, transferMons[r].Game));
                    }
                }

                results.Show();
            }
            else
            {
                MessageBox.Show("Could not find random numbers");
            }

        }

        public bool isHatPikachu(MonData mon)
        {
            if (mon.Species == 25 && mon.AltForm > 0)
                return true;
            return false;
        }

        public bool isAlolanForm(MonData mon)
        {
            if (alolan.ContainsKey(mon.Species))
            {
                if (alolan[mon.Species] == mon.AltForm)
                    return true;
            }
            return false;
        }

        public bool isGalarianForm(MonData mon)
        {
            if (galarian.ContainsKey(mon.Species))
            {
                if (galarian[mon.Species] == mon.AltForm)
                    return true;
            }
            return false;
        }

        public bool hasHisuianForm(MonData mon)
        {
            if (hisuian.ContainsKey(mon.Species))
                return true;
            return false;
        }

        public bool isHisuianForm(MonData mon)
        {
            if (hisuian.ContainsKey(mon.Species))
            {
                if (hisuian[mon.Species] == mon.AltForm)
                    return true;
            }
            return false;
        }

        public bool isPaldeanForm(MonData mon)
        {
            if (paldean.ContainsKey(mon.Species))
            {
                if (paldean[mon.Species] == mon.AltForm || (mon.Species == 128 && (mon.AltForm >= 1 && mon.AltForm <= 3)))
                    return true;
            }
            return false;
        }

        public bool canTransfer(List<MonData> monData, int index)
        {
            bool t = true;

            string[] noTransfer = { };

            foreach (string nt in noTransfer)
            {
                if (monData[index].Game.Contains(nt))
                {
                    t = false;
                }
            }

            if (monData[index].Game.Contains("HOME"))
            {
                bool outHome = false;

                foreach (var gameInfo in gameDict)
                {
                    GameVersion vers = gameInfo.Value.version;
                    if ((vers == GameVersion.GP || vers == GameVersion.GE) &&
                        (monData[index].Gen < 8))
                    {
                        if (dexes[(int)Dexes.LetsGoDex].Dexes["Kanto"].Contains(monData[index].Species) &&
                            !isHatPikachu(monData[index]) && !isGalarianForm(monData[index]) && !isHisuianForm(monData[index]))
                        {
                            bool legalForm = true;
                            if (galarian.ContainsKey(monData[index].Species))
                            {
                                if (galarian[monData[index].Species] == monData[index].AltForm)
                                    legalForm = false;
                            }
                            if (hisuian.ContainsKey(monData[index].Species))
                            {
                                if (hisuian[monData[index].Species] == monData[index].AltForm)
                                    legalForm = false;
                            }
                            outHome = legalForm;
                        }
                    }
                    else if (vers == GameVersion.SW || vers == GameVersion.SH)
                    {
                        if ((dexes[(int)Dexes.SwordShieldDex].Dexes["Galar"].Contains(monData[index].Species) ||
                            dexes[(int)Dexes.SwordShieldDex].Dexes["Isle of Armor"].Contains(monData[index].Species) ||
                            dexes[(int)Dexes.SwordShieldDex].Dexes["Crown Tundra"].Contains(monData[index].Species) ||
                            dexes[(int)Dexes.SwordShieldDex].Foreign.Contains(monData[index].Species)) &&
                            !(monData[index].Species == 290 && monData[index].Origin == "Sinnoh (BDSP)") &&
                            !isHisuianForm(monData[index]) && !isPaldeanForm(monData[index]))
                        {
                            outHome = true;
                        }
                    }
                    else if (vers == GameVersion.BD || vers == GameVersion.SP)
                    {
                        if ((dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Dexes["Sinnoh"].Contains(monData[index].Species) ||
                             dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Foreign.Contains(monData[index].Species)) &&
                             monData[index].Species != 355 &&
                             !(monData[index].Species == 290 && monData[index].Origin != "Sinnoh (BDSP)") &&
                             !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 52 || monData[index].Species == 133)) &&
                            !isHatPikachu(monData[index]) && !isAlolanForm(monData[index]) && !isGalarianForm(monData[index]) && !isHisuianForm(monData[index]) && !isPaldeanForm(monData[index]))
                        {
                            outHome = true;
                        }
                    }
                    else if (vers == GameVersion.PLA)
                    {
                        if ((dexes[(int)Dexes.LegendsArceusDex].Dexes["Hisui"].Contains(monData[index].Species) ||
                             dexes[(int)Dexes.LegendsArceusDex].Foreign.Contains(monData[index].Species)) &&
                            !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 133)) &&
                            !isHatPikachu(monData[index]) &&
                            (!isAlolanForm(monData[index]) || monData[index].Species == 37 || monData[index].Species == 38) &&
                            !isGalarianForm(monData[index]) &&
                            (isHisuianForm(monData[index]) || !hasHisuianForm(monData[index]) || monData[index].Species == 215))
                        {
                            outHome = true;
                        }
                    }
                    else if (vers == GameVersion.SL || vers == GameVersion.VL)
                    {
                        if ((dexes[(int)Dexes.ScarletVioletDex].Dexes["Paldea"].Contains(monData[index].Species) ||
                             dexes[(int)Dexes.ScarletVioletDex].Dexes["Kitakami"].Contains(monData[index].Species) ||
                             dexes[(int)Dexes.ScarletVioletDex].Dexes["Blueberry"].Contains(monData[index].Species) ||
                             dexes[(int)Dexes.ScarletVioletDex].Foreign.Contains(monData[index].Species)) &&
                             !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 52 || monData[index].Species == 133)))
                        {
                            outHome = true;
                        }
                    }
                }

                t = (t && outHome);
            }

            return t;
        }

        public string idealTransfer(List<MonData> monData, int index)
        {
            string dest;

            List<Tuple<string, float, int, int>> comp = new List<Tuple<string, float, int, int>>();

            foreach (var save in gameDict)
            {
                GameVersion vers = save.Value.version;
                if ((vers == GameVersion.GP || vers == GameVersion.GE) &&
                    (monData[index].Gen < 8))
                {
                    if (dexes[(int)Dexes.LetsGoDex].Dexes["Kanto"].Contains(monData[index].Species) &&
                        !isGalarianForm(monData[index]) && !isHisuianForm(monData[index]))
                    {
                        int num = 0;
                        float spec = 0;
                        float spform = 0;
                        foreach (MonData md in monData)
                        {
                            if (md.Game == save.Key)
                            {
                                num++;
                                if (md.Species == monData[index].Species)
                                {
                                    spec++;
                                }
                                if (md.Species == monData[index].Species &&
                                    (md.AltForm == monData[index].AltForm || noDiff.Contains(md.Species)) &&
                                    (md.Gender == monData[index].Gender || !genderDiff.Contains(md.Species)) &&
                                    md.Shiny == monData[index].Shiny)
                                {
                                    spform++;
                                }
                            }
                        }
                        comp.Add(Tuple.Create(save.Key, (spec == 0) ? 0 : spec * (1 + (spform / spec)) / 2, LGPE_PRIORITY, num));
                    }
                }
                else if ((vers == GameVersion.SW || vers == GameVersion.SH))
                {
                    if ((dexes[(int)Dexes.SwordShieldDex].Dexes["Galar"].Contains(monData[index].Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Dexes["Isle of Armor"].Contains(monData[index].Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Dexes["Crown Tundra"].Contains(monData[index].Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Foreign.Contains(monData[index].Species)) &&
                        !isHisuianForm(monData[index]) &&
                        !isPaldeanForm(monData[index]))
                    {
                        int num = 0;
                        float spec = 0;
                        float spform = 0;
                        foreach (MonData md in monData)
                        {
                            if (md.Game == save.Key)
                            {
                                num++;
                                if (md.Species == monData[index].Species)
                                {
                                    spec++;
                                }
                                if (md.Species == monData[index].Species &&
                                    (md.AltForm == monData[index].AltForm || noDiff.Contains(md.Species)) &&
                                    (md.Gender == monData[index].Gender || !genderDiff.Contains(md.Species)) &&
                                    md.Shiny == monData[index].Shiny)
                                {
                                    spform++;
                                }
                            }
                        }
                        comp.Add(Tuple.Create(save.Key, (spec == 0) ? 0 : spec * (1 + (spform / spec)) / 2, SWSH_PRIORITY, num));
                    }
                }
                else if ((vers == GameVersion.BD || vers == GameVersion.SP))
                {
                    if ((dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Dexes["Sinnoh"].Contains(monData[index].Species) ||
                         dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Foreign.Contains(monData[index].Species)) &&
                         (monData[index].Species != 290 && monData[index].Species != 355) &&
                         !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 52 || monData[index].Species == 133)) &&
                        !isHatPikachu(monData[index]) &&
                        !isAlolanForm(monData[index]) &&
                        !isGalarianForm(monData[index]) &&
                        !isHisuianForm(monData[index]) &&
                        !isPaldeanForm(monData[index]))
                    {
                        int num = 0;
                        float spec = 0;
                        float spform = 0;
                        foreach (MonData md in monData)
                        {
                            if (md.Game == save.Key)
                            {
                                num++;
                                if (md.Species == monData[index].Species)
                                {
                                    spec++;
                                }
                                if (md.Species == monData[index].Species &&
                                    (md.AltForm == monData[index].AltForm || noDiff.Contains(md.Species)) &&
                                    (md.Gender == monData[index].Gender || !genderDiff.Contains(md.Species)) &&
                                    md.Shiny == monData[index].Shiny)
                                {
                                    spform++;
                                }
                            }
                        }
                        comp.Add(Tuple.Create(save.Key, (spec == 0) ? 0 : spec * (1 + (spform / spec)) / 2, BDSP_PRIORITY, num));
                    }
                }
                else if ((vers == GameVersion.PLA))
                {
                    if ((dexes[(int)Dexes.LegendsArceusDex].Dexes["Hisui"].Contains(monData[index].Species) ||
                         dexes[(int)Dexes.LegendsArceusDex].Foreign.Contains(monData[index].Species)) &&
                         !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 133)) &&
                        !isHatPikachu(monData[index]) &&
                        (!isAlolanForm(monData[index]) || monData[index].Species == 37 || monData[index].Species == 38) &&
                        !isGalarianForm(monData[index]) &&
                        (isHisuianForm(monData[index]) || !hasHisuianForm(monData[index]) || monData[index].Species == 215) &&
                        !isPaldeanForm(monData[index]))
                    {
                        int num = 0;
                        float spec = 0;
                        float spform = 0;
                        foreach (MonData md in monData)
                        {
                            if (md.Game == save.Key)
                            {
                                num++;
                                if (md.Species == monData[index].Species)
                                {
                                    spec++;
                                }
                                if (md.Species == monData[index].Species &&
                                    (md.AltForm == monData[index].AltForm || noDiff.Contains(md.Species)) &&
                                    (md.Gender == monData[index].Gender || !genderDiff.Contains(md.Species)) &&
                                    md.Shiny == monData[index].Shiny)
                                {
                                    spform++;
                                }
                            }
                        }
                        comp.Add(Tuple.Create(save.Key, (spec == 0) ? 0 : spec * (1 + (spform / spec)) / 2, PLA_PRIORITY, num));
                    }
                }
                else if ((vers == GameVersion.SL || vers == GameVersion.VL))
                {
                    if ((dexes[(int)Dexes.ScarletVioletDex].Dexes["Paldea"].Contains(monData[index].Species) ||
                         dexes[(int)Dexes.ScarletVioletDex].Dexes["Kitakami"].Contains(monData[index].Species) ||
                         dexes[(int)Dexes.ScarletVioletDex].Dexes["Blueberry"].Contains(monData[index].Species) ||
                         dexes[(int)Dexes.ScarletVioletDex].Foreign.Contains(monData[index].Species)) &&
                         !(monData[index].gMax && (monData[index].Species == 25 || monData[index].Species == 52 || monData[index].Species == 133)))
                    {
                        int num = 0;
                        float spec = 0;
                        float spform = 0;
                        foreach (MonData md in monData)
                        {
                            if (md.Game == save.Key)
                            {
                                num++;
                                if (md.Species == monData[index].Species)
                                {
                                    spec++;
                                }
                                if (md.Species == monData[index].Species &&
                                    (md.AltForm == monData[index].AltForm || noDiff.Contains(md.Species)) &&
                                    (md.Gender == monData[index].Gender || !genderDiff.Contains(md.Species)) &&
                                    md.Shiny == monData[index].Shiny)
                                {
                                    spform++;
                                }
                            }
                        }
                        comp.Add(Tuple.Create(save.Key, (spec == 0) ? 0 : spec * (1 + (spform / spec)) / 2, SV_PRIORITY, num));
                    }
                }
            }

            if (comp.Count == 0)
            {
                dest = "???";
            }
            else
            {
                dest = comp[0].Item1;
                double spec = comp[0].Item2;
                int priority = comp[0].Item3;
                int size = comp[0].Item4;

                if (comp.Count > 1)
                {
                    for (int i = 1; i < comp.Count; i++)
                    {
                        if (spec > comp[i].Item2)
                        {
                            dest = comp[i].Item1;
                            spec = comp[i].Item2;
                            priority = comp[i].Item3;
                            size = comp[i].Item4;
                        }
                        else if (spec == comp[i].Item2)
                        {
                            if (priority > comp[i].Item3)
                            {
                                dest = comp[i].Item1;
                                spec = comp[i].Item2;
                                priority = comp[i].Item3;
                                size = comp[i].Item4;
                            }
                            else if (priority == comp[i].Item3)
                            {
                                if (size >= comp[i].Item4)
                                {
                                    dest = comp[i].Item1;
                                    spec = comp[i].Item2;
                                    priority = comp[i].Item3;
                                    size = comp[i].Item4;
                                }
                            }
                        }
                    }
                }
            }
            return dest;
        }

        public string homeTransferCounts()
        {
            List<MonData> homeMons = new List<MonData>();

            foreach (MonData md in PkmData)
            {
                if (md.Game.Contains("HOME"))
                    homeMons.Add(md);
            }

            int lgpe = 0;
            int swsh = 0;
            int bdsp = 0;
            int pla = 0;
            int sv = 0;

            foreach (MonData hm in homeMons)
            {

                if ((dexes[(int)Dexes.SwordShieldDex].Dexes["Galar"].Contains(hm.Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Dexes["Isle of Armor"].Contains(hm.Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Dexes["Crown Tundra"].Contains(hm.Species) ||
                        dexes[(int)Dexes.SwordShieldDex].Foreign.Contains(hm.Species)) &&
                        !isHisuianForm(hm) &&
                        !isPaldeanForm(hm))
                {
                    swsh++;
                }

                if ((dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Dexes["Sinnoh"].Contains(hm.Species) ||
                        dexes[(int)Dexes.BrilliantDiamondShiningPearlDex].Foreign.Contains(hm.Species)) &&
                    !isHatPikachu(hm) &&
                    !isAlolanForm(hm) &&
                    !isGalarianForm(hm) &&
                    !isHisuianForm(hm) &&
                    !isPaldeanForm(hm) &&
                    !(hm.gMax && (hm.Species == 25 || hm.Species == 52 || hm.Species == 133)))
                {
                    bdsp++;
                }

                if ((dexes[(int)Dexes.LegendsArceusDex].Dexes["Hisui"].Contains(hm.Species) ||
                        dexes[(int)Dexes.LegendsArceusDex].Foreign.Contains(hm.Species)) &&
                    !isHatPikachu(hm) &&
                    (!isAlolanForm(hm) || hm.Species == 37 || hm.Species == 38) &&
                    !isGalarianForm(hm) &&
                    (isHisuianForm(hm) || !hasHisuianForm(hm) || hm.Species == 215) &&
                    !isPaldeanForm(hm) &&
                    !(hm.gMax && (hm.Species == 25 || hm.Species == 133)))
                {
                    pla++;
                }

                if (dexes[(int)Dexes.ScarletVioletDex].Dexes["Paldea"].Contains(hm.Species) ||
                    dexes[(int)Dexes.ScarletVioletDex].Dexes["Kitakami"].Contains(hm.Species) ||
                    dexes[(int)Dexes.ScarletVioletDex].Dexes["Blueberry"].Contains(hm.Species) ||
                    dexes[(int)Dexes.ScarletVioletDex].Foreign.Contains(hm.Species) &&
                    !(hm.gMax && (hm.Species == 25 || hm.Species == 52 || hm.Species == 133)))
                {
                    sv++;
                }

                if (hm.Gen < 8)
                {
                    if (dexes[(int)Dexes.LetsGoDex].Dexes["Kanto"].Contains(hm.Species) &&
                        !isGalarianForm(hm) && !isHisuianForm(hm))
                    {
                        lgpe++;
                    }
                }
            }

            return String.Format("LGPE: {0}, SWSH: {1},  BDSP: {2}, PLA: {3}, SV: {4}", lgpe, swsh, bdsp, pla, sv);
        }

        private void buttonIdealTransfer_Click(object sender, EventArgs e)
        {
            List<ComboItem> PkmListSorted = new List<ComboItem>(GameInfo.SpeciesDataSource);
            PkmListSorted = PkmListSorted.OrderBy(i => i.Value).ToList();

            string spForm = PkmListSorted[PkmData[slotSelected].Species].Text;
            if (PkmData[slotSelected].AltForm > 0 && !noDiff.Contains(PkmData[slotSelected].Species) && PkmData[slotSelected].Species != 869)
                spForm += "-" + PkmData[slotSelected].AltForm.ToString();
            else if (PkmData[slotSelected].Species == 869 && PkmData[slotSelected].AltForm >= 7)
                spForm += "-" + (PkmData[slotSelected].AltForm / 7).ToString();
            MessageBox.Show(String.Format("{0} ({1}, {2}) -> [{3}]", PkmData[slotSelected].Nickname, spForm, PkmData[slotSelected].OT, idealTransfer(PkmData, slotSelected)));
        }
    }
}
