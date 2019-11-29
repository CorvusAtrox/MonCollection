using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PKHeX.Core;

namespace MonCollection
{
    public partial class FormEggCalc : Form
    {
        private List<MonData> PkmData;
        private int ind = 0;
        private int mate = 0;
        private int gen;
        private MonData mon;
        private GameVersion version;

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;

        private PictureBox[] PKXBOXES;

        private int[] majorGenderDiff;
        private int[] noDiff;

        private int[] eg;

        private List<ComboItem> moveNames;

        private Assembly pkAssembly;

        public FormEggCalc()
        {
            InitializeComponent();

            pkAssembly = Assembly.LoadFile(Path.GetFullPath("PKHeX.Core.dll"));

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

                    if ((int)slot.Tag != -1)
                    {
                        mate = (int)slot.Tag;
                        SetMate(PkmData[(int)slot.Tag]);
                        FillPKXBoxes((int)(bpkx1.Tag) / RES_MIN);
                    }

                };
            }

            majorGenderDiff = new int[] { 521, 592, 593, 668, 678 };
            noDiff = new int[] { 414, 493, 664, 665, 744, 773 };
        }

        internal void loadDB(List<MonData> data, int index, GameVersion vers)
        {
            PkmData = data;
            ind = index;
            mon = PkmData[ind];
            gen = mon.Gen;
            version = vers;
        }

        public void showValues()
        {
            moveNames = new List<ComboItem>(GameInfo.FilteredSources.Moves);

            foreach (int move in mon.Moves)
                listMovesMon.Items.Add(moveName(move));

            eg = MonDataToPKM(mon).PersonalInfo.EggGroups;
            labelName.Text = String.Format("Name: {0}",mon.Nickname);
            if (MonDataToPKM(mon).PersonalInfo.IsEggGroup(15))
            {
                PkmData = PkmData.Where(pk => false).ToList();
            }
            else
            {
                PkmData = PkmData.Where(pk => ((pk.Gender + mon.Gender == 1) &&
                               (MonDataToPKM(pk).PersonalInfo.IsEggGroup(eg[0]) || MonDataToPKM(pk).PersonalInfo.IsEggGroup(eg[1])))
                               || ((pk.Species == 132 ^ mon.Species == 132) && (!MonDataToPKM(pk).PersonalInfo.IsEggGroup(15))))
                               .ToList();
            }

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
            monIcon.Image = retrieveImage("Resources/img/icons/" + spForm + ".png");
            if (monIcon.Image != null)
            {
                if (monIcon.Image.Height > 56)
                    monIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    monIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            FillPKXBoxes(0);
            SetResults(PkmData);
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
                PKXBOXES[i].Image = retrieveImage("Resources/img/icons/" + spForm + ".png");
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
                PKXBOXES[i].BackgroundImage = null;
            if (mate != -1 && mate >= begin && mate < begin + RES_MAX)
                PKXBOXES[mate - begin].BackgroundImage = Image.FromFile("Resources/img/slotView.png");
        }

        private Image retrieveImage(string path)
        {
            if (File.Exists(path))
                return Image.FromFile(path);
            else
                return null;
        }

        private void SCR_Box_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                FillPKXBoxes(e.NewValue);
        }

        private void SetResults(List<MonData> res)
        {

            SCR_Box.Maximum = (int)Math.Ceiling((decimal)res.Count / RES_MIN);
            if (SCR_Box.Maximum > 0) SCR_Box.Maximum--;

            mate = 0; // reset the slot last viewed
            SCR_Box.Value = 0;

            L_Count.Text = string.Format("Mates {0}", res.Count);
            if(PkmData.Count != 0)
                SetMate(PkmData[0]);
        }

        private void SetMate(MonData mate)
        {
            labelMateName.Text = String.Format("Name: {0}", mate.Nickname);
            listMovesMate.Items.Clear();
            foreach (int move in mate.Moves)
                listMovesMate.Items.Add(moveName(move));
            if (mon.Gender == 1 || mate.Species == 132)
                makeEgg(mon, mate);
            else
                makeEgg(mate, mon);
                

        }

        private void makeEgg(MonData mon1, MonData mon2)
        {
            PKM pkmn = MonDataToPKM(mon1);
            var eggs = EncounterEggGenerator.GenerateEggs(pkmn);
            int sp = eggs.ToArray()[0].Species;
            int[] moves = calcEggMoves(sp, mon1.AltForm);
            string spForm = sp.ToString();
            if (mon1.AltForm > 0 && !noDiff.Contains(mon1.Species))
                spForm += "-" + mon1.AltForm.ToString();
            labelShiny.Visible = (mon1.Language != mon2.Language);
            eggIcon.Image = retrieveImage("Resources/img/icons/" + spForm + ".png");
            if (eggIcon.Image != null)
            {
                if (eggIcon.Image.Height > 56)
                    eggIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    eggIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            listMoves.Items.Clear();
            if (gen >= 6)
            {
                foreach(int move in moves.Intersect(mon1.Moves))
                    listMoves.Items.Add(moveName(move));
            }
            foreach (int move in moves.Intersect(mon2.Moves))
            {
                if(!listMoves.Items.Contains(move))
                    listMoves.Items.Add(moveName(move));
            }
                
            if(gen <= 5)
            {
                moves = calcTMMoves(pkmn,sp,mon1.AltForm).ToArray();
                foreach (int move in moves.Intersect(mon2.Moves))
                {
                    if (!listMoves.Items.Contains(move))
                        listMoves.Items.Add(moveName(move));
                }
            }
            if(version == GameVersion.C)
            {
                moves = MoveTutor.GetTutorMoves(pkmn, sp, 0, false, gen).ToArray();
                foreach (int move in moves.Intersect(mon2.Moves))
                {
                    if (!listMoves.Items.Contains(move))
                        listMoves.Items.Add(moveName(move));
                }
            }
            foreach (int move in mon1.Moves.Intersect(mon2.Moves))
            {
                if (!listMoves.Items.Contains(move) && move != 0 && calcLevelUpMoves(pkmn, sp, mon1.AltForm,move).Level != -1)
                    listMoves.Items.Add(moveName(move));
            }
            int ball = 4;
            int ball2 = 0;
            if(gen >= 7)
            {
                if(mon1.Species == 132)
                    ball = mon2.Ball;
                else
                {
                    ball = mon1.Ball;
                    if (mon2.Species == mon1.Species)
                        ball2 = mon2.Ball;
                }
                
            }
            else if(gen == 6)
            {
                if (mon1.Gender == 1)
                    ball = mon1.Ball;
                else if (mon2.Gender == 1)
                    ball = mon2.Ball;
            }
            pictureBoxBall.Image = getBreedBall(ball);
            pictureBoxBall2.Image = getBreedBall(ball2);

            if (eggs.Count() == 2)
                sp = eggs.ToArray()[1].Species;
            else if (sp == 29)
                sp = 32;
            else if (sp == 32 && gen >= 5)
                sp = 29;
            else if (sp == 313 && gen >= 5)
                sp = 314;
            else if (sp == 314)
                sp = 313;
            else
                sp = 0;

            if (sp > 0)
            {
                moves = calcEggMoves(sp, mon1.AltForm);
                eggIcon2.Image = retrieveImage("Resources/img/icons/" + sp.ToString() + ".png");
                if (eggIcon2.Image != null)
                {
                    if (eggIcon2.Image.Height > 56)
                        eggIcon2.SizeMode = PictureBoxSizeMode.StretchImage;
                    else
                        eggIcon2.SizeMode = PictureBoxSizeMode.CenterImage;
                }
                listMoves2.Items.Clear();
                if (gen >= 6)
                {
                    foreach (int move in moves.Intersect(mon1.Moves))
                        listMoves2.Items.Add(moveName(move));
                }
                foreach (int move in moves.Intersect(mon2.Moves))
                {
                    if (!listMoves2.Items.Contains(move))
                        listMoves2.Items.Add(moveName(move));
                }

                if (gen <= 5)
                {
                    moves = calcTMMoves(pkmn, sp, mon1.AltForm).ToArray();
                    foreach (int move in moves.Intersect(mon2.Moves))
                    {
                        if (!listMoves2.Items.Contains(move))
                            listMoves2.Items.Add(moveName(move));
                    }
                }
                if (version == GameVersion.C)
                {
                    moves = MoveTutor.GetTutorMoves(pkmn, sp, 0, false, gen).ToArray();
                    foreach (int move in moves.Intersect(mon2.Moves))
                    {
                        if (!listMoves2.Items.Contains(move))
                            listMoves2.Items.Add(moveName(move));
                    }
                }
                foreach (int move in mon1.Moves.Intersect(mon2.Moves))
                {
                    if (!listMoves2.Items.Contains(move) && move != 0 && calcLevelUpMoves(pkmn, sp, mon1.AltForm, move).Level != -1)
                        listMoves2.Items.Add(moveName(move));
                }
                ball = 4;
                ball2 = 0;
                if (gen >= 7)
                {

                    if (mon2.Species == mon1.Species)
                        ball2 = mon2.Ball;
                }
                else if (gen == 6)
                {
                    if (mon1.Gender == 1)
                        ball = mon1.Ball;
                    else if (mon2.Gender == 1)
                        ball = mon2.Ball;
                }
                pictureBoxBalla.Image = getBreedBall(ball);
                pictureBoxBall2a.Image = getBreedBall(ball2);

                eggIcon2.Visible = true;
                pictureBoxBalla.Visible = true;
                pictureBoxBall2a.Visible = true;
                listMoves2.Visible = true;
            }
            else
            {
                eggIcon2.Visible = false;
                pictureBoxBalla.Visible = false;
                pictureBoxBall2a.Visible = false;
                listMoves2.Visible = false;
            }
        }

        private int[] calcEggMoves(int species, int forme)
        {
            Type eggType = pkAssembly.GetType("PKHeX.Core.MoveEgg");
            MethodInfo em = eggType.GetMethod("GetEggMoves", BindingFlags.NonPublic | BindingFlags.Static, null,
                                              new Type[] { typeof(int), typeof(int), typeof(int), typeof(GameVersion) }, null);
            object[] paramArray = new object[] { gen, species, forme, version };
            object val = em.Invoke(eggType, paramArray);
            return (int[]) val;
        }

        private IEnumerable<int> calcTMMoves(PKM mon, int species, int forme)
        {
            Type tmType = pkAssembly.GetType("PKHeX.Core.MoveTechnicalMachine");
            MethodInfo em = tmType.GetMethod("GetTMHM", BindingFlags.NonPublic | BindingFlags.Static);
            object[] paramArray = new object[] { mon, species, forme, gen, version, true};
            object val = em.Invoke(tmType, paramArray);
            return (IEnumerable<int>)val;
        }

        private LearnVersion calcLevelUpMoves(PKM mon, int species, int forme, int move)
        {
            Type lUpType = pkAssembly.GetType("PKHeX.Core.MoveLevelUp");
            MethodInfo em = lUpType.GetMethod("GetIsLevelUpMove", BindingFlags.Public | BindingFlags.Static);
            object[] paramArray = new object[] { mon, species, forme, 100, gen, move, 5, 5, version };
            object val = em.Invoke(lUpType, paramArray);
            return (LearnVersion)val;
        }

        private string moveName(int index)
        {
            var result = moveNames.Where(move => move.Value == index).ToArray();
            if (result.Count() > 0)
                return result[0].Text;
            else
                return "(None)";
        }

        private Image getBreedBall(int ball)
        {
            if (ball == 1 || ball == 16)
                ball = 4;

            if(ball == 0)
                return null;
            else
                return retrieveImage("Resources/img/ball/" + ball.ToString() + ".png");
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
                    mon = new PK7();
                    break;
            }
            mon.Species = data.Species;
            mon.AltForm = data.AltForm;
            mon.CurrentLevel = data.Level;
            mon.Version = (int)version;
            return mon;
        }

    }
}
