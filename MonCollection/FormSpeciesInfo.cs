using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MonCollection
{
    public partial class FormSpeciesInfo : Form
    {
        public Image spImage;
        public string spFormName;
        public List<Image> ballList;
        public List<string> nameList;
        public List<string> abilityList;
        public List<string> languageList;
        public List<string> moveList;
        public List<int> levelList;
        public bool hasShiny;
        private PictureBox[] ballArray;

        public FormSpeciesInfo()
        {
            InitializeComponent();
            ballList = new List<Image>();
            abilityList = new List<string>();
            nameList = new List<string>();
            languageList = new List<string>();
            moveList = new List<string>();
            levelList = new List<int>();
            ballArray = new PictureBox[]{pictureBall0, pictureBall1, pictureBall2, pictureBall3, pictureBall4,
                                         pictureBall5, pictureBall6, pictureBall7, pictureBall8, pictureBall9,
                                         pictureBall10, pictureBall11, pictureBall12, pictureBall13, pictureBall14,
                                         pictureBall15, pictureBall16, pictureBall17, pictureBall18, pictureBall19};
        }

        public void LoadData()
        {
            pictureBoxIcon.Image = spImage;
            if (pictureBoxIcon.Image != null)
            {
                if (pictureBoxIcon.Image.Height > 56)
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            labelSpeciesForm.Text = spFormName;
            int pb = 0;
            foreach (var i in ballList)
            {
                ballArray[pb].Image = i;
                pb++;
            }
            foreach(string n in nameList)
                listBoxNames.Items.Add(n);
            foreach(string a in abilityList)
                listBoxAbilities.Items.Add(a);
            foreach(string l in languageList)
                listBoxLanguages.Items.Add(l);
            foreach(string m in moveList)
                listBoxMoves.Items.Add(m);
            foreach(int v in levelList)
                listBoxLevels.Items.Add(v);

            labelShiny.Visible = hasShiny;

        }
    }
}
