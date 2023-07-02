using Microsoft.VisualBasic.Devices;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MonCollection
{
    public partial class FormMovepools : Form
    {

        public Dictionary<string, Movepools> movepools;
        public MonData mon;

        private readonly LegalMoveSource<ComboItem> LegalMoveSource = new(new LegalMoveComboSource());
        private LegalityAnalysis legal;
        private ComboBox[] moveBoxes;
        private List<ushort> relearn;

        public FormMovepools()
        {
            InitializeComponent();
        }

        public void SetupValues()
        {
            comboBoxCategory.Items.Clear();
            if (movepools == null)
            {
                movepools = new Dictionary<string, Movepools>();
            }
            foreach (var entry in movepools)
            {
                comboBoxCategory.Items.Add(entry.Key);
            }

            moveBoxes = new[] { comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4 };

            foreach (var cb in moveBoxes)
            {
                cb.DisplayMember = nameof(ComboItem.Text);
                cb.ValueMember = nameof(ComboItem.Value);
            }

            var source = GameInfo.FilteredSources;
            LegalMoveSource.ChangeMoveSource(source.Moves);
            foreach (var cb in moveBoxes)
                cb.DataSource = new BindingSource(source.Moves, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            movepools[comboBoxCategory.Text].moves = new List<int> { (int?)comboBoxMove1.SelectedValue ?? 0, (int?)comboBoxMove2.SelectedValue ?? 0,
                                                                     (int?)comboBoxMove3.SelectedValue ?? 0, (int?)comboBoxMove4.SelectedValue ?? 0 };
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.Text != string.Empty && !movepools.ContainsKey(comboBoxCategory.Text))
            {
                movepools[comboBoxCategory.Text] = new Movepools
                {
                    moves = new List<int> { 0, 0, 0, 0 }
                };
            }
            if (comboBoxCategory.Text != string.Empty)
            {
                GameVersion gv = GameVersion.SH;
                switch (comboBoxCategory.Text)
                {
                    case "LGPE":
                        gv = GameVersion.GE;
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
                pkmn.CurrentLevel = mon.Level;
                pkmn.Version = (int)gv;

                legal = new LegalityAnalysis(pkmn, sf.Personal);
                LegalMoveSource.ReloadMoves(legal);

                var ls = GameData.GetLearnSource(gv);
                var learn = ls.GetLearnset(mon.Species, mon.AltForm);
                var mv = learn.GetAllMoves();

                relearn = new List<ushort>();
                for (int i = 0; i < 4; i++)
                {
                    ushort m = (ushort)movepools[comboBoxCategory.Text].moves[i];
                    if (m != 0 && !relearn.Contains(m) && LegalMoveSource.Info.CanLearn(m))
                    {
                        relearn.Add(m);
                    }
                }

                foreach (var m in mv)
                {
                    if (learn.GetLevelLearnMove(m) <= mon.Level && !relearn.Contains(m))
                    {
                        relearn.Add(m);
                    }
                }

                listBoxMoves.Items.Clear();
                var moveNames = new List<ComboItem>(GameInfo.MoveDataSource);
                foreach (var r in relearn)
                {
                    if (r != 0)
                        listBoxMoves.Items.Add(moveNames.Find(p => p.Value == r).Text);
                }
            }
            comboBoxMove1.SelectedValue = movepools[comboBoxCategory.Text].moves[0];
            comboBoxMove2.SelectedValue = movepools[comboBoxCategory.Text].moves[1];
            comboBoxMove3.SelectedValue = movepools[comboBoxCategory.Text].moves[2];
            comboBoxMove4.SelectedValue = movepools[comboBoxCategory.Text].moves[3];
        }

        private void buttonSetCategory_Click(object sender, EventArgs e)
        {
            if (!comboBoxCategory.Items.Contains(comboBoxCategory.Text))
            {
                comboBoxCategory.Items.Add(comboBoxCategory.Text);
                movepools[comboBoxCategory.Text] = new Movepools
                {
                    moves = new List<int> { 0, 0, 0, 0 }
                };
                comboBoxMove1.SelectedValue = movepools[comboBoxCategory.Text].moves[0];
                comboBoxMove2.SelectedValue = movepools[comboBoxCategory.Text].moves[1];
                comboBoxMove3.SelectedValue = movepools[comboBoxCategory.Text].moves[2];
                comboBoxMove4.SelectedValue = movepools[comboBoxCategory.Text].moves[3];
            }
        }
    }
}
