using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;

namespace MonCollection
{
    public partial class FormEggCalc : Form
    {
        private List<PKM> PkmData;
        private int ind = 0;
        private int mate = 0;
        private string game;

        private const int RES_MAX = 30;
        private const int RES_MIN = 6;

        private PictureBox[] PKXBOXES;

        private int[] majorGenderDiff;
        private int[] minorGenderDiff;


        public FormEggCalc()
        {
            InitializeComponent();

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
                        //OpenPKM(PkmData[(int)slot.Tag]);
                        FillPKXBoxes((int)(bpkx1.Tag) / RES_MIN);
                    }

                };
            }

            majorGenderDiff = new int[] { 521, 592, 593, 668, 678 };
            minorGenderDiff = new int[] { 3, 12, 19, 20, 25, 26, 41, 42, 44, 45, 64, 65, 84, 85, 97,
                                         111, 112, 118, 119, 123, 129, 130, 154, 165, 166, 178, 185,
                                         186, 190, 194, 195, 198, 202, 203, 207, 208, 212, 214, 215,
                                         217, 221, 224, 229, 232, 255, 256, 257, 267, 269, 272, 274,
                                         275, 307, 308, 315, 316, 317, 322, 323, 332, 350, 369, 396,
                                         397, 398, 399, 400, 401, 402, 403, 404, 405, 407, 415, 417,
                                         417, 418, 419, 424, 443, 444, 445, 449, 450, 453, 454, 456,
                                         457, 459, 460, 461, 464, 465, 473};
        }

        public void loadDB(List<PKM> data, int index)
        {
            PkmData = data;
            ind = index;
            game = data[index].Identifier.Split('\\')[1];
        }

        public void showValues()
        {
            PKM mon = PkmData[ind];
            labelName.Text = String.Format("Name: {0}",mon.Nickname);

            PkmData = PkmData.Where(pk=> pk.Identifier.Split('\\')[1] == game)
                               .Where(pk => (pk.Gender + mon.Gender == 1) 
                               || (pk.Species == 132 ^ mon.Species == 132)).ToList();
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
                if (mon.AltForm > 0)
                    spForm += "-" + mon.AltForm.ToString();
                else if (majorGenderDiff.Contains(mon.Species))
                {
                    if (mon.Gender == 0)
                        spForm += "m";
                    else if (mon.Gender == 1)
                        spForm += "f";
                }
                PKXBOXES[i].Image = retrieveImage("img/icons/" + spForm + ".png");
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
                PKXBOXES[mate - begin].BackgroundImage = Image.FromFile("img/slotView.png");
        }

        private Image retrieveImage(string path)
        {
            if (File.Exists(path))
                return Image.FromFile(path);
            else
                return null;
        }
    }
}
