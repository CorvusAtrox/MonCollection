using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
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
        private SoundPlayer cryMaker;
        private string[] genders = { "♂", "♀️", "-" };
        //private string[] genders = { "M", "F", "N" };
        private readonly LegalMoveSource LegalMoveSource = new LegalMoveSource();
        private ComboBox[] moveBoxes;
        private PictureBox[] PKXBOXES;
        private int slotSelected = -1; // = null;
        private LegalityAnalysis legal;
        private List<PKM> PkmData;

        private string Counter = "Num Mon: {0}";

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;

        public static DrawConfig Draw = new DrawConfig();

        public Dictionary<string,SaveInfo> gameDict;

        public class SaveInfo
        {
            public string language;
            public GameVersion version;
            public int index;

            public SaveInfo(string l, GameVersion v, int i)
            {
                language = l;
                version = v;
                index = i;
            }

        }


        public MainForm()
        {
            InitializeComponent();
            InitializeGameDict();
            InitializeStrings("en",GameVersion.US);
            InitializeBinding();
            InitializePkxBoxes();
            PopulateFilteredDataSources(ver);
            L_Count.Text = "Loading...";
            new Task(LoadDatabase).Start();
        }

        private void InitializeGameDict()
        {
            gameDict = new Dictionary<string, SaveInfo>();
            gameDict.Add("Red [Dustin]", new SaveInfo("en", GameVersion.RD, 0));
            gameDict.Add("Blue [Yuuya]", new SaveInfo("fr", GameVersion.GN, 1));
            gameDict.Add("Yellow [Juan]", new SaveInfo("es", GameVersion.YW, 2));
        }

        private void InitializeStrings(string spr, GameVersion gv)
        {

            GameInfo.Strings = GameInfo.GetStrings(spr);
            ver = SaveUtil.GetBlankSAV(gv, "blank");
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

            foreach (var slot in PKXBOXES)
            {
                slot.DoubleClick += (sender, e) =>
                {

                    slotSelected = (int)slot.Tag;
                    OpenPKM(PkmData[(int)slot.Tag]);
                    FillPKXBoxes((int)(bpkx1.Tag)/RES_MIN);
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
            //CB_GameOrigin.DataSource = new BindingSource(source.Games, null);

            // Set the Move ComboBoxes too..
            LegalMoveSource.ReloadMoves(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string path = "C:/Users/CorvusAtrox/Music/PKHeX/pkmdb/004 - Embi - 96EDA47AE9C1.pk6";

            byte[] input;

            try {
                input = File.ReadAllBytes(path);
            } catch (Exception) {
                return;
            }

            PKM pk = PKMConverter.GetPKMfromBytes(input);

            OpenPKM(pk);
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
            identifier = identifier.Split('\\')[1];
            if (!gameDict.TryGetValue(identifier, out SaveInfo info))
            {
                ver = SaveUtil.GetBlankSAV(GameVersion.US, "setMe");
                ver.Language = GameLanguage.GetLanguageIndex("en");
                InitializeStrings("en", ver.Version);
            } 
            else
            {
                ver = SaveUtil.GetBlankSAV(info.version, "setMe");
                ver.Language = GameLanguage.GetLanguageIndex(info.language);
                InitializeStrings(info.language, ver.Version);
            }
            PopulateFilteredDataSources(ver);

        }

        private int gameIndex(string identifier)
        {
            identifier = identifier.Split('\\')[1];
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
                    labelLanguage.Visible = false;
                    comboBoxLanguage.Visible = false;
                    break;
            }

            statusItemNickname.Text = pk.Nickname;
            comboBoxBalls.SelectedValue = pk.Ball;
            comboBoxSpecies.SelectedValue = pk.Species;
            comboBoxLanguage.SelectedValue = pk.Language;
            comboBoxAbility.SelectedValue = pk.Ability;
            comboBoxNature.SelectedValue = pk.Nature;
            comboBoxMove1.SelectedValue = pk.Move1;
            comboBoxMove2.SelectedValue = pk.Move2;
            comboBoxMove3.SelectedValue = pk.Move3;
            comboBoxMove4.SelectedValue = pk.Move4;
            textBoxLevel.Text = pk.CurrentLevel.ToString();
            pictureBoxBall.Image = retrieveImage("img/ball/" + pk.Ball + ".png");
            pictureBoxIcon.Image = retrieveImage("img/icons/" + pk.Species + ".png");
            labelGender.Text = genders[pk.Gender];
            cryMaker = new SoundPlayer("cries/" + pk.Species + ".wav");
            cryMaker.Play();
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
            PkmData = LoadPKMSaves("mons");

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
                Console.WriteLine(pk.Identifier);
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
                PKXBOXES[i].Image = retrieveImage("img/icons/" + PkmData[i + begin].Species.ToString() + ".png");
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
                PKXBOXES[slotSelected - begin].BackgroundImage = Image.FromFile("img/slotView.png");
        }

        private void SCR_Box_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                FillPKXBoxes(e.NewValue);
        }

        //        //All Species



        //        PkmData.OrderBy(mon =>mon.Species).ThenBy(mon=>mon.CurrentLevel).ThenBy(mon=>mon.Nickname).ThenBy(mon=>mon.Generation);



        //        //All Levels



        //        PkmData.OrderBy(mon=>mon.CurrentLevel).ThenBy(mon=>mon.Species).ThenBy(mon=>mon.Nickname).ThenBy(mon=>mon.Generation);



        //        //Gen Species



        //        PkmData.OrderBy(mon=>mon.GenNumber).ThenBy(mon=>mon.Species).ThenBy(mon=>mon.CurrentLevel).ThenBy(mon=>mon.Nickname);



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
    }
}
